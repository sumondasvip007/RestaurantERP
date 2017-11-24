using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Repository;
using Microsoft.Ajax.Utilities;
using Microsoft.Reporting.WebForms;
using Restaurant.Models.ViewModel;
using Restaurant.Utility;

namespace Restaurant.Controllers
{
    public class ProductEntryHistoryInSellPointController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: ProductionHouseProductTransferStatus
        [SessionManger.CheckUserSession]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult ProductEntryHistoryInSellPoint()
        {
            return View();
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetSellsPointInformation()
        {
            try
            {
                IEnumerable<VM_StoreInformation> SellsPointList = (from a in unitOfWork.StoreRepository.Get().Where(a => a.IsSellsPointStore == true)
                                                                   select new VM_StoreInformation()
                                                                   {
                                                                       StoreId = a.store_id,
                                                                       StoreName = a.store_name,
                                                                   }).ToList();
                return Json(new { success = true, result = SellsPointList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult SearchProductTransactionList(int sellsPointStoreId, DateTime fromDate, DateTime toDate,int shiftId)
        {
            try
            {
                List<DAL.ViewModel.VM_Product> productList = unitOfWork.CustomRepository.spSearchProductEntryHistoryInSellsPoint(fromDate, toDate,
                sellsPointStoreId, shiftId);
                decimal totalAmount = 0;
                if (productList.Any())
                {
                    totalAmount = productList.Select(s => s.TotalPrice).Sum();
                    return Json(new { success = true, result = productList, TotalAmount = totalAmount }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, errorMessage = "No product transition found.", TotalAmount = totalAmount }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GenerateReportForSearchResult(int sellsPointStoreId, DateTime fromDate, DateTime toDate, int shiftId)
        {
            try
            {
                

                List<DAL.ViewModel.VM_Product> productList = unitOfWork.CustomRepository.spSearchProductEntryHistoryInSellsPoint(fromDate, toDate,
                sellsPointStoreId, shiftId);
                decimal totalAmount = 0;
                var newProductList = new List<VM_Product>();
                int serial = 0;
                foreach (var product in productList)
                {
                    VM_Product newProduct = new VM_Product();
                    newProduct.Serial = ++serial;
                    newProduct.ProductName = product.ProductName;
                    newProduct.ProductTypeName = product.ProductTypeName;
                    newProduct.UnitPrice = product.UnitPrice;
                    newProduct.Quantity = product.Quantity;
                    newProduct.Unit = product.Unit;
                    newProduct.TotalPrice = product.TotalPrice;
                    newProductList.Add(newProduct);
                }

                totalAmount = productList.Select(s => s.TotalPrice).Sum();

                int restaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString()); ;
                string restaurantName = unitOfWork.RestaurantRepository.GetByID(restaurantId).Name;
                string restaurantAddress = unitOfWork.RestaurantRepository.GetByID(restaurantId).Address;
                string sellsPointName = unitOfWork.StoreRepository.GetByID(sellsPointStoreId).store_name;
                string shiftName = unitOfWork.ShiftRepository.GetByID(shiftId).ShiftName;


                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reports/ProductEntryHistoryInSellPointReport.rdlc");
                localReport.SetParameters(new ReportParameter("FromDate", fromDate.ToString()));
                localReport.SetParameters(new ReportParameter("ToDate", toDate.ToString()));
                localReport.SetParameters(new ReportParameter("ShiftName", shiftName));
                localReport.SetParameters(new ReportParameter("SellsPointName", sellsPointName));
                localReport.SetParameters(new ReportParameter("RestaurantName", restaurantName));
                localReport.SetParameters(new ReportParameter("RestaurantAddress", restaurantAddress));
                ReportDataSource reportDataSource = new ReportDataSource("ProductEntryHistoryInSellPointDataSet", newProductList);

                localReport.DataSources.Add(reportDataSource);
                string reportType = "pdf";
                string mimeType;
                string encoding;
                string fileNameExtension;
                //The DeviceInfo settings should be changed based on the reportType
                //http://msdn.microsoft.com/en-us/library/ms155397.aspx
                string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>0in</MarginLeft>" +
                "  <MarginRight>0in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";
                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;
                //Render the report
                renderedBytes = localReport.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);
                var path = System.IO.Path.Combine(Server.MapPath("~/pdfReport"));
                var saveAs = string.Format("{0}.pdf", Path.Combine(path, "myfilename"));

                var idx = 0;
                while (System.IO.File.Exists(saveAs))
                {
                    idx++;
                    saveAs = string.Format("{0}.{1}.pdf", Path.Combine(path, "myfilename"), idx);
                }
                Session["report"] = saveAs;
                using (var stream = new FileStream(saveAs, FileMode.Create, FileAccess.Write))
                {
                    stream.Write(renderedBytes, 0, renderedBytes.Length);
                    stream.Close();
                }
                localReport.Dispose();
                return Json(new { success = true, successMessage = "Product Report generated.", result = productList, TotalAmount = totalAmount }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public ActionResult ViewPdfReportForSearchResult(int sellsPointStoreId, string fromDate, string toDate, int shiftId)
        {
            try
            {
                List<DAL.ViewModel.VM_Product> productList = unitOfWork.CustomRepository.spSearchProductEntryHistoryInSellsPoint(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate),
                    sellsPointStoreId, shiftId);
                var newProductList = new List<VM_Product>();
                int serial = 0;
                foreach (var product in productList)
                {
                    VM_Product newProduct = new VM_Product();
                    newProduct.Serial = ++serial;
                    newProduct.ProductName = product.ProductName;
                    newProduct.ProductTypeName = product.ProductTypeName;
                    newProduct.UnitPrice = product.UnitPrice;
                    newProduct.Quantity = product.Quantity;
                    newProduct.Unit = product.Unit;
                    newProduct.TotalPrice = product.TotalPrice;
                    newProductList.Add(newProduct);
                }

                int restaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString()); ;
                string restaurantName = unitOfWork.RestaurantRepository.GetByID(restaurantId).Name;
                string restaurantAddress = unitOfWork.RestaurantRepository.GetByID(restaurantId).Address;
                string sellsPointName = unitOfWork.StoreRepository.GetByID(sellsPointStoreId).store_name;
                string shiftName = unitOfWork.ShiftRepository.GetByID(shiftId).ShiftName;
                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reports/ProductEntryHistoryInSellPointReport.rdlc");
                localReport.SetParameters(new ReportParameter("FromDate", fromDate.ToString()));
                localReport.SetParameters(new ReportParameter("ToDate", toDate.ToString()));
                localReport.SetParameters(new ReportParameter("ShiftName", shiftName));
                localReport.SetParameters(new ReportParameter("SellsPointName", sellsPointName));
                localReport.SetParameters(new ReportParameter("RestaurantName", restaurantName));
                localReport.SetParameters(new ReportParameter("RestaurantAddress", restaurantAddress));
                ReportDataSource reportDataSource = new ReportDataSource("ProductEntryHistoryInSellPointDataSet", newProductList);

                localReport.DataSources.Add(reportDataSource);
                string reportType = "pdf";
                string mimeType;
                string encoding;
                string fileNameExtension;
                //The DeviceInfo settings should be changed based on the reportType
                //http://msdn.microsoft.com/en-us/library/ms155397.aspx
                string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>0in</MarginLeft>" +
                "  <MarginRight>0in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                //Render the report
                renderedBytes = localReport.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);
                return File(renderedBytes, mimeType);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}