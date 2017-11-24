using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Repository;
using Microsoft.Reporting.WebForms;
using Restaurant.Models.Enam;
using Restaurant.Models.ViewModel;
using Restaurant.Utility;

namespace Restaurant.Controllers
{
    public class SellsPointStatusController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: SellsPointStatus
        [SessionManger.CheckUserSession]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult SellsPointProductStatus()
        {
            return View();
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetAllStoreList()
        {
            try
            {
                IEnumerable<VM_StoreInformation> SellsPointStoreList = (from a in unitOfWork.StoreRepository.Get().Where(a => a.IsSellsPointStore == true)
                                                              select new VM_StoreInformation()
                                                              {
                                                                  StoreId = a.store_id,
                                                                  StoreName = a.store_name,
                                                              }).ToList();
                return Json(new { success = true, result = SellsPointStoreList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetAllSellsTypeProductList()
        {
            try
            {
                IEnumerable<VM_Product> sellsPointProductList = (from a in unitOfWork.ProductRepository.Get().Where(a => a.ProductTypeId == (int)ProductType.SellTypeProduct || a.ProductTypeId == (int)ProductType.PurchaseAndSellTypeProduct)
                                                                 select new VM_Product()
                                                              {
                                                                  ProductId = a.ProductId,
                                                                  ProductName = a.ProductName,
                                                                  Unit = a.Unit,
                                                                  UnitPrice = (decimal)a.UnitPrice
                                                              }).ToList();
                return Json(new { success = true, result = sellsPointProductList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        //public JsonResult GetSellsPointProductQuantityList(int? storeId, IEnumerable<VM_Product> productList)
        public JsonResult GetSellsPointProductQuantityList(int storeId)
        {
            try
            {
                decimal totalAmount = 0;

                //List<VM_Product> products = new List<VM_Product>();
                //foreach (VM_Product aProduct in productList)
                //{
                //    var product =
                //    unitOfWork.ProductTransferRepository.Get()
                //        .Where(x => x.ProductId == aProduct.ProductId && x.StoreId ==storeId)
                //        .ToList();

                //    decimal ProductIn = (decimal)product.Where(x => x.isIn == true).Select(x => x.Quantity).Sum();
                //    decimal ProductOut = (decimal)product.Where(x => x.isOut == true).Select(x => x.Quantity).Sum();

                //    decimal getProductAvilableQty = ProductIn - ProductOut;

                //    if (getProductAvilableQty>0)
                //    {
                //        aProduct.Quantity = getProductAvilableQty;
                //        aProduct.TotalPrice = aProduct.UnitPrice * getProductAvilableQty;
                //        totalAmount += aProduct.TotalPrice;

                //        products.Add(aProduct);
                //    }
                    
                //}


                //DateTime today = DateTime.Now;

                //var dateToday = DateTime.Parse(today.ToString());
                //var date = string.Format("{0:yyyy-MM-dd}", dateToday);


                //List<DAL.ViewModel.VM_Product> products = unitOfWork.CustomRepository.sp_SellPointProductStatus(storeId, Convert.ToDateTime(date)).ToList();
                List<DAL.ViewModel.VM_Product> products = unitOfWork.CustomRepository.sp_SellPointProductStatus(storeId).ToList();

                if (products.Any())
                {
                    totalAmount = products.Select(x => x.TotalPrice).Sum();
                }

                    

                return Json(new { success = true, result = products, TotalAmount = totalAmount }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [SessionManger.CheckUserSession]
        [Authorize]
        //public ActionResult ViewPdfReportForSearchResult(int? storeId)
        public ActionResult ViewPdfReportForSearchResult(int storeId)
        {
            //IEnumerable<VM_Product> sellsPointProductList = (from a in unitOfWork.ProductRepository.Get().Where(a => a.ProductTypeId == (int)ProductType.SellTypeProduct || a.ProductTypeId == (int)ProductType.PurchaseAndSellTypeProduct)
            //                                                 select new VM_Product()
            //                                                 {
            //                                                     ProductId = a.ProductId,
            //                                                     ProductName = a.ProductName,
            //                                                     Unit = a.Unit,
            //                                                     UnitPrice = (decimal)a.UnitPrice
            //                                                 }).ToList();
            try
            {
                decimal totalAmount = 0;
                //List<VM_Product> products = new List<VM_Product>();
                //foreach (VM_Product aProduct in sellsPointProductList)
                //{
                //    var product =
                //    unitOfWork.ProductTransferRepository.Get()
                //        .Where(x => x.ProductId == aProduct.ProductId && x.StoreId == storeId)
                //        .ToList();

                //    decimal ProductIn = (decimal)product.Where(x => x.isIn == true).Select(x => x.Quantity).Sum();
                //    decimal ProductOut = (decimal)product.Where(x => x.isOut == true).Select(x => x.Quantity).Sum();


                //    decimal getProductAvilableQty = ProductIn - ProductOut;
                //    if (getProductAvilableQty > 0)
                //    {
                //        aProduct.Quantity = getProductAvilableQty;
                //        aProduct.TotalPrice = aProduct.UnitPrice*getProductAvilableQty;
                //        totalAmount += aProduct.TotalPrice;
                //        products.Add(aProduct);
                //    }
                //}

                //DateTime today = DateTime.Now;

                //var dateToday = DateTime.Parse(today.ToString());
                //var date = string.Format("{0:yyyy-MM-dd}", dateToday);

                //List<DAL.ViewModel.VM_Product> products = unitOfWork.CustomRepository.sp_SellPointProductStatus(storeId, Convert.ToDateTime(date)).ToList();
                List<DAL.ViewModel.VM_Product> products = unitOfWork.CustomRepository.sp_SellPointProductStatus(storeId).ToList();

               

                string storeName = unitOfWork.StoreRepository.GetByID(storeId).store_name;
                int restaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString()); ;
                string restaurantName = unitOfWork.RestaurantRepository.GetByID(restaurantId).Name;
                string restaurantAddress = unitOfWork.RestaurantRepository.GetByID(restaurantId).Address;

                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reports/SellsPointStatusReport.rdlc");
                localReport.SetParameters(new ReportParameter("StoreName", storeName));
                localReport.SetParameters(new ReportParameter("RestaurantName", restaurantName));
                localReport.SetParameters(new ReportParameter("RestaurantAddress", restaurantAddress));
                ReportDataSource reportDataSource = new ReportDataSource("SellsPointStatusDataSet", products);

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