using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Repository;
using DAL.ViewModel;
using Microsoft.Reporting.WebForms;
using Restaurant.Models.Enam;
using Restaurant.Utility;
using VM_Product = Restaurant.Models.ViewModel.VM_Product;

namespace Restaurant.Controllers
{
    public class ProductSellHistoryWithOpeningProductController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: ProductSellHistoryWithOpeningProduct
       

        [SessionManger.CheckUserSession]
        [Authorize]
        public ActionResult ViewProductSellHistoryWithOpeningProduct()
        {
            return View();
        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllGroup()
        {
            try
            {

                var groupList = (from a in unitOfWork.GroupForShiftRepository.Get()
                                 select new VM_GroupForShift()
                                 {

                                     GroupId = a.GroupId,
                                     GroupName = a.GroupName

                                 }).ToList();
                return Json(new { success = true, result = groupList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllShift()
        {
            try
            {

                var shiftList = (from a in unitOfWork.ShiftRepository.Get()
                                 select new VM_Shift()
                                 {

                                     ShiftId = a.ShiftId,
                                     ShiftName = a.ShiftName

                                 }).ToList();
                return Json(new { success = true, result = shiftList }, JsonRequestBehavior.AllowGet);
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
                                                                     UnitPrice = (decimal)a.UnitPrice,
                                                                     ProductionCost = Convert.ToDecimal(a.ProductionCost)
                                                                     
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
        //public JsonResult GetSellsPointProductQuantityList(int? storeId,int shiftId, DateTime fromDate, IEnumerable<VM_Product> productList)
        public JsonResult GetSellsPointProductQuantityList(int storeId,int shiftId, DateTime fromDate)
        {
            try
            {

                List<DAL.ViewModel.VM_Product> products; 

                decimal totalAmount = 0;
                decimal totalProductionCostAmount=0;
                decimal totalOtherExpense = 0;
                decimal netCash = 0;


                var date = DateTime.Parse(fromDate.ToString());
                var printInfo = string.Format("{0:yyyy-MM-dd}", date);
                   

                //List<VM_Product> products = new List<VM_Product>();

                if (shiftId==1)
                {

                    //foreach (VM_Product aProduct in productList)
                    //{
                    //    var product =
                    //    unitOfWork.ProductTransferRepository.Get()
                    //        .Where(x => x.ProductId == aProduct.ProductId && x.StoreId == storeId)
                    //        .ToList();

     
                    //    decimal ProductIn = (decimal)product.Where(x => x.isIn == true && x.TransferDate < fromDate).Select(x => x.Quantity).Sum();
                    //    decimal ProductOut = (decimal)product.Where(x => x.isOut == true && x.TransferDate < fromDate).Select(x => x.Quantity).Sum();

                    //    decimal getProductAvilableQty = ProductIn - ProductOut;
                    //    aProduct.OpeningProduct = getProductAvilableQty;

                

                        
                    //    decimal issue = (decimal)product.Where(x => x.isIn == true && x.ShiftId == shiftId && x.TransferDate.Value.Date == Convert.ToDateTime(printInfo)).Select(x => x.Quantity).Sum();

                    //    aProduct.InProduct = issue;
                    //    aProduct.TotalProduct = aProduct.OpeningProduct + aProduct.InProduct;

                        
                    //    decimal SellproductSelectDate = (decimal)product.Where(x => x.isOut == true && x.ShiftId == shiftId && x.TransferDate.Value.Date == Convert.ToDateTime(printInfo)).Select(x => x.Quantity).Sum();
                    //    aProduct.SellProduct = SellproductSelectDate;

                    //    aProduct.ClosingProduct = aProduct.TotalProduct - aProduct.SellProduct;

                    //    aProduct.TotalPrice = aProduct.UnitPrice * aProduct.SellProduct;

                    //    totalAmount += aProduct.TotalPrice;

                    //    if (getProductAvilableQty > 0 || issue > 0 || SellproductSelectDate > 0)
                    //    {
                    //        products.Add(aProduct);
                    //    }

                    //}



                    products =
                        unitOfWork.CustomRepository.sp_ProductSellHistoryWithOpeningProductForDayShift(storeId,shiftId,fromDate).ToList();


                    if (products.Any())
                    {
                        totalAmount = products.Select(x => x.TotalPrice).Sum();
                        totalProductionCostAmount = products.Select(x => x.TotalProductionCost).Sum();
                    }


                }

                else
                {
                    //foreach (VM_Product aProduct in productList)
                    //{
                    //    var product =
                    //    unitOfWork.ProductTransferRepository.Get()
                    //        .Where(x => x.ProductId == aProduct.ProductId && x.StoreId == storeId)
                    //        .ToList();



                    //    decimal ProductIn = (decimal)product.Where(x => x.isIn == true && x.TransferDate < fromDate).Select(x => x.Quantity).Sum();
                    //    decimal ProductOut = (decimal)product.Where(x => x.isOut == true && x.TransferDate < fromDate).Select(x => x.Quantity).Sum();

                    //    decimal getProductAvilableQty = ProductIn - ProductOut;
                        

                    //    decimal issueForDay = (decimal)product.Where(x => x.isIn == true && x.ShiftId == 1 && x.TransferDate.Value.Date == Convert.ToDateTime(printInfo)).Select(x => x.Quantity).Sum();

                        
                    //    decimal totalProductForDay = getProductAvilableQty + issueForDay;


                    //    decimal SellproductSelectDateForDay = (decimal)product.Where(x => x.isOut == true && x.ShiftId == 1 && x.TransferDate.Value.Date == Convert.ToDateTime(printInfo)).Select(x => x.Quantity).Sum();
                        
                    //    decimal closingProductForDay = totalProductForDay - SellproductSelectDateForDay;
                      

                    //    aProduct.OpeningProduct = closingProductForDay;

                    //    decimal issue = (decimal)product.Where(x => x.isIn == true && x.ShiftId == shiftId && x.TransferDate.Value.Date == Convert.ToDateTime(printInfo)).Select(x => x.Quantity).Sum();

                    //    aProduct.InProduct = issue;
                    //    aProduct.TotalProduct = aProduct.OpeningProduct + aProduct.InProduct;


                    //    decimal SellproductSelectDate = (decimal)product.Where(x => x.isOut == true && x.ShiftId == shiftId && x.TransferDate.Value.Date == Convert.ToDateTime(printInfo)).Select(x => x.Quantity).Sum();
                    //    aProduct.SellProduct = SellproductSelectDate;

                    //    aProduct.ClosingProduct = aProduct.TotalProduct - aProduct.SellProduct;

                    //    aProduct.TotalPrice = aProduct.UnitPrice * aProduct.SellProduct;

                    //    totalAmount += aProduct.TotalPrice;

                    //    if (closingProductForDay > 0 || issue > 0 || SellproductSelectDate > 0)
                    //    {
                    //        products.Add(aProduct);
                    //    }

                    //}


                    products =
                    unitOfWork.CustomRepository.sp_ProductSellHistoryWithOpeningProductForNightShift(storeId, shiftId, fromDate).ToList();


                    if (products.Any())
                    {
                        totalAmount = products.Select(x => x.TotalPrice).Sum();
                        totalProductionCostAmount = products.Select(x => x.TotalProductionCost).Sum();
                    }




                }


                //decimal less = (decimal)
                //    unitOfWork.OtherExpenseRepository.Get()
                //        .Where(
                //            x =>
                //                x.StoreId == storeId && x.ShiftId == shiftId &&
                //                x.TransferDate.Value.Date == Convert.ToDateTime(printInfo))
                //        .Select(x => x.Less)
                //        .Sum();

                //decimal due = (decimal)
                //   unitOfWork.OtherExpenseRepository.Get()
                //       .Where(
                //           x =>
                //               x.StoreId == storeId && x.ShiftId == shiftId &&
                //               x.TransferDate.Value.Date == Convert.ToDateTime(printInfo))
                //       .Select(x => x.Due)
                //       .Sum();
                //decimal complimen = (decimal)
                //   unitOfWork.OtherExpenseRepository.Get()
                //       .Where(
                //           x =>
                //               x.StoreId == storeId && x.ShiftId == shiftId &&
                //               x.TransferDate.Value.Date == Convert.ToDateTime(printInfo))
                //       .Select(x => x.Compliment)
                //       .Sum();
                //decimal damage = (decimal)
                //   unitOfWork.OtherExpenseRepository.Get()
                //       .Where(
                //           x =>
                //               x.StoreId == storeId && x.ShiftId == shiftId &&
                //               x.TransferDate.Value.Date == Convert.ToDateTime(printInfo))
                //       .Select(x => x.Damage)
                //       .Sum();


                decimal less =
                    unitOfWork.CustomRepository.sp_OtherExpenseAmountForSellReportWithOpeningProduct(storeId, shiftId,
                        fromDate).Select(a => a.Less).FirstOrDefault();
                decimal due =
                    unitOfWork.CustomRepository.sp_OtherExpenseAmountForSellReportWithOpeningProduct(storeId, shiftId,
                        fromDate).Select(a => a.Due).FirstOrDefault();
                decimal complimen =
                    unitOfWork.CustomRepository.sp_OtherExpenseAmountForSellReportWithOpeningProduct(storeId, shiftId,
                        fromDate).Select(a => a.Compliment).FirstOrDefault();
                decimal damage =
                    unitOfWork.CustomRepository.sp_OtherExpenseAmountForSellReportWithOpeningProduct(storeId, shiftId,
                        fromDate).Select(a => a.Damage).FirstOrDefault();
                


                totalOtherExpense = less + due + complimen + damage;

                netCash = totalAmount - totalOtherExpense;





                return Json(new
                {
                    success = true,
                    result = products,
                    TotalAmount = totalAmount,
                    TotalProductionCostAmount = totalProductionCostAmount,
                    Less = less,
                    Due = due,
                    Complimen = complimen,
                    Damage = damage,
                    TotalOtherExpense = totalOtherExpense,
                                  NetCash = netCash
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [SessionManger.CheckUserSession]
        [Authorize]
        //public JsonResult GenerateReportForSearchResult(int? storeId, int shiftId, DateTime fromDate)
        public JsonResult GenerateReportForSearchResult(int storeId, int shiftId, DateTime fromDate)
        {
            try
            {
                var date = DateTime.Parse(fromDate.ToString());
                var printInfo = string.Format("{0:yyyy-MM-dd}", date);

                //IEnumerable<VM_Product> sellsPointProductList = (from a in unitOfWork.ProductRepository.Get().Where(a => a.ProductTypeId == (int)ProductType.SellTypeProduct || a.ProductTypeId == (int)ProductType.PurchaseAndSellTypeProduct)
                //                                                 select new VM_Product()
                //                                                 {
                //                                                     ProductId = a.ProductId,
                //                                                     ProductName = a.ProductName,
                //                                                     Unit = a.Unit,
                //                                                     UnitPrice = (decimal)a.UnitPrice
                //                                                 }).ToList();

                List<DAL.ViewModel.VM_Product> products; 

                string groupName;
                decimal totalAmount = 0;
                decimal totalOtherExpense = 0;
                double netCash = 0;
                //List<VM_Product> products = new List<VM_Product>();

                if (shiftId == 1)
                {
                    groupName =
                        unitOfWork.GroupAndShiftMappingRepository.Get()
                            .Where(x => x.TransferDate.Value.Date == Convert.ToDateTime(printInfo))
                            .Select(a => a.Day)
                            .FirstOrDefault();
                    //foreach (VM_Product aProduct in sellsPointProductList)
                    //{
                    //    var product =
                    //    unitOfWork.ProductTransferRepository.Get()
                    //        .Where(x => x.ProductId == aProduct.ProductId && x.StoreId == storeId)
                    //        .ToList();

                        

                    //    decimal ProductIn = (decimal)product.Where(x => x.isIn == true && x.TransferDate < fromDate).Select(x => x.Quantity).Sum();
                    //    decimal ProductOut = (decimal)product.Where(x => x.isOut == true && x.TransferDate < fromDate).Select(x => x.Quantity).Sum();

                    //    decimal getProductAvilableQty = ProductIn - ProductOut;
                    //    aProduct.OpeningProduct = getProductAvilableQty;

                       

                        
                    //    decimal issue = (decimal)product.Where(x => x.isIn == true && x.ShiftId == shiftId && x.TransferDate.Value.Date == Convert.ToDateTime(printInfo)).Select(x => x.Quantity).Sum();

                    //    aProduct.InProduct = issue;
                    //    aProduct.TotalProduct = aProduct.OpeningProduct + aProduct.InProduct;

                        
                    //    decimal SellproductSelectDate = (decimal)product.Where(x => x.isOut == true && x.ShiftId == shiftId && x.TransferDate.Value.Date == Convert.ToDateTime(printInfo)).Select(x => x.Quantity).Sum();
                    //    aProduct.SellProduct = SellproductSelectDate;

                    //    aProduct.ClosingProduct = aProduct.TotalProduct - aProduct.SellProduct;

                    //    aProduct.TotalPrice = aProduct.UnitPrice * aProduct.SellProduct;

                    //    totalAmount += aProduct.TotalPrice;

                    //    if (getProductAvilableQty > 0 || issue > 0 || SellproductSelectDate > 0)
                    //    {
                    //        products.Add(aProduct);
                    //    }

                    //}



                    products =
                        unitOfWork.CustomRepository.sp_ProductSellHistoryWithOpeningProductForDayShift(storeId, shiftId, fromDate).ToList();


                    if (products.Any())
                    {
                        totalAmount = products.Select(x => x.TotalPrice).Sum();
                    }

                }

                else
                {
                    groupName =
                       unitOfWork.GroupAndShiftMappingRepository.Get()
                           .Where(x => x.TransferDate.Value.Date == Convert.ToDateTime(printInfo))
                           .Select(a => a.Night)
                           .FirstOrDefault();
                    //foreach (VM_Product aProduct in sellsPointProductList)
                    //{
                    //    var product =
                    //    unitOfWork.ProductTransferRepository.Get()
                    //        .Where(x => x.ProductId == aProduct.ProductId && x.StoreId == storeId)
                    //        .ToList();



                    //    decimal ProductIn = (decimal)product.Where(x => x.isIn == true && x.TransferDate < fromDate).Select(x => x.Quantity).Sum();
                    //    decimal ProductOut = (decimal)product.Where(x => x.isOut == true && x.TransferDate < fromDate).Select(x => x.Quantity).Sum();

                    //    decimal getProductAvilableQty = ProductIn - ProductOut;
                      

                    //    decimal issueForDay = (decimal)product.Where(x => x.isIn == true && x.ShiftId == 1 && x.TransferDate.Value.Date == Convert.ToDateTime(printInfo)).Select(x => x.Quantity).Sum();

                        
                    //    decimal totalProductForDay = getProductAvilableQty + issueForDay;


                    //    decimal SellproductSelectDateForDay = (decimal)product.Where(x => x.isOut == true && x.ShiftId == 1 && x.TransferDate.Value.Date == Convert.ToDateTime(printInfo)).Select(x => x.Quantity).Sum();
                       

                    //    decimal closingProductForDay = totalProductForDay - SellproductSelectDateForDay;
                       

                    //    aProduct.OpeningProduct = closingProductForDay;

                    //    decimal issue = (decimal)product.Where(x => x.isIn == true && x.ShiftId == shiftId && x.TransferDate.Value.Date == Convert.ToDateTime(printInfo)).Select(x => x.Quantity).Sum();

                    //    aProduct.InProduct = issue;
                    //    aProduct.TotalProduct = aProduct.OpeningProduct + aProduct.InProduct;


                    //    decimal SellproductSelectDate = (decimal)product.Where(x => x.isOut == true && x.ShiftId == shiftId && x.TransferDate.Value.Date == Convert.ToDateTime(printInfo)).Select(x => x.Quantity).Sum();
                    //    aProduct.SellProduct = SellproductSelectDate;

                    //    aProduct.ClosingProduct = aProduct.TotalProduct - aProduct.SellProduct;

                    //    aProduct.TotalPrice = aProduct.UnitPrice * aProduct.SellProduct;

                    //    totalAmount += aProduct.TotalPrice;

                    //    if (closingProductForDay > 0 || issue > 0 || SellproductSelectDate > 0)
                    //    {
                    //        products.Add(aProduct);
                    //    }

                    //}

                    products =
                 unitOfWork.CustomRepository.sp_ProductSellHistoryWithOpeningProductForNightShift(storeId, shiftId, fromDate).ToList();


                    if (products.Any())
                    {
                        totalAmount = products.Select(x => x.TotalPrice).Sum();
                    }


                }

                //decimal less = (decimal)
                //   unitOfWork.OtherExpenseRepository.Get()
                //       .Where(
                //           x =>
                //               x.StoreId == storeId && x.ShiftId == shiftId &&
                //               x.TransferDate.Value.Date == Convert.ToDateTime(printInfo))
                //       .Select(x => x.Less)
                //       .Sum();
                //decimal due = (decimal)
                //   unitOfWork.OtherExpenseRepository.Get()
                //       .Where(
                //           x =>
                //               x.StoreId == storeId && x.ShiftId == shiftId &&
                //               x.TransferDate.Value.Date == Convert.ToDateTime(printInfo))
                //       .Select(x => x.Due)
                //       .Sum();
                //decimal complimen = (decimal)
                //   unitOfWork.OtherExpenseRepository.Get()
                //       .Where(
                //           x =>
                //               x.StoreId == storeId && x.ShiftId == shiftId &&
                //               x.TransferDate.Value.Date == Convert.ToDateTime(printInfo))
                //       .Select(x => x.Compliment)
                //       .Sum();
                //decimal damage = (decimal)
                //   unitOfWork.OtherExpenseRepository.Get()
                //       .Where(
                //           x =>
                //               x.StoreId == storeId && x.ShiftId == shiftId &&
                //               x.TransferDate.Value.Date == Convert.ToDateTime(printInfo))
                //       .Select(x => x.Damage)
                //       .Sum();



                decimal less =
                unitOfWork.CustomRepository.sp_OtherExpenseAmountForSellReportWithOpeningProduct(storeId, shiftId,
                    fromDate).Select(a => a.Less).FirstOrDefault();
                decimal due =
                    unitOfWork.CustomRepository.sp_OtherExpenseAmountForSellReportWithOpeningProduct(storeId, shiftId,
                        fromDate).Select(a => a.Due).FirstOrDefault();
                decimal complimen =
                    unitOfWork.CustomRepository.sp_OtherExpenseAmountForSellReportWithOpeningProduct(storeId, shiftId,
                        fromDate).Select(a => a.Compliment).FirstOrDefault();
                decimal damage =
                    unitOfWork.CustomRepository.sp_OtherExpenseAmountForSellReportWithOpeningProduct(storeId, shiftId,
                        fromDate).Select(a => a.Damage).FirstOrDefault();

                totalOtherExpense = less + due + complimen + damage;

                netCash = (double) (totalAmount - totalOtherExpense);


                int restaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                string restaurantName = unitOfWork.RestaurantRepository.GetByID(restaurantId).Name;
                string restaurantAddress = unitOfWork.RestaurantRepository.GetByID(restaurantId).Address;
                string storeName = unitOfWork.StoreRepository.GetByID(storeId).store_name;
                //string groupName = unitOfWork.GroupForShiftRepository.GetByID(groupId).GroupName;
                string shiftName = unitOfWork.ShiftRepository.GetByID(shiftId).ShiftName;

                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reports/ProductSellHistoryWithOpeningProductReport.rdlc");               
                localReport.SetParameters(new ReportParameter("Date", printInfo));
                localReport.SetParameters(new ReportParameter("StoreName", storeName.ToString()));
                localReport.SetParameters(new ReportParameter("RestaurantName", restaurantName));
                localReport.SetParameters(new ReportParameter("RestaurantAddress", restaurantAddress));
                localReport.SetParameters(new ReportParameter("GroupName", groupName));
                localReport.SetParameters(new ReportParameter("ShiftName", shiftName));
                localReport.SetParameters(new ReportParameter("Less", less.ToString()));
                localReport.SetParameters(new ReportParameter("Due", due.ToString()));
                localReport.SetParameters(new ReportParameter("Complimen", complimen.ToString()));
                localReport.SetParameters(new ReportParameter("Damage", damage.ToString()));
                localReport.SetParameters(new ReportParameter("TotalOtherExpense", totalOtherExpense.ToString()));
                localReport.SetParameters(new ReportParameter("NetCash", netCash.ToString()));
                ReportDataSource reportDataSource = new ReportDataSource("ProductSellHistoryWithOpeningProductDataSet", products);

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
                return Json(new
                {
                    success = true,
                    successMessage = "Product Report generated.",
                    result = products,
                    TotalAmount = totalAmount,
                    Less = less,
                    Due = due,
                    Complimen = complimen,
                    Damage = damage,
                    TotalOtherExpense = totalOtherExpense,
                    NetCash = netCash
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        //public ActionResult ViewPdfReportForSearchResult(int? storeId, int shiftId, DateTime fromDate)
        public ActionResult ViewPdfReportForSearchResult(int storeId, int shiftId, DateTime fromDate)
        {
            try
            {
                var date = DateTime.Parse(fromDate.ToString());
                var printInfo = string.Format("{0:yyyy-MM-dd}", date);

                //IEnumerable<VM_Product> sellsPointProductList = (from a in unitOfWork.ProductRepository.Get().Where(a => a.ProductTypeId == (int)ProductType.SellTypeProduct || a.ProductTypeId == (int)ProductType.PurchaseAndSellTypeProduct)
                //                                                 select new VM_Product()
                //                                                 {
                //                                                     ProductId = a.ProductId,
                //                                                     ProductName = a.ProductName,
                //                                                     Unit = a.Unit,
                //                                                     UnitPrice = (decimal)a.UnitPrice
                //                                                 }).ToList();



                List<DAL.ViewModel.VM_Product> products; 

                string groupName;
                decimal totalAmount = 0;
                decimal totalOtherExpense = 0;
                double netCash = 0;


                //List<VM_Product> products = new List<VM_Product>();
                if (shiftId == 1)
                {
                    groupName =
                        unitOfWork.GroupAndShiftMappingRepository.Get()
                            .Where(x => x.TransferDate.Value.Date == Convert.ToDateTime(printInfo))
                            .Select(a => a.Day)
                            .FirstOrDefault();
                    //foreach (VM_Product aProduct in sellsPointProductList)
                    //{
                    //    var product =
                    //    unitOfWork.ProductTransferRepository.Get()
                    //        .Where(x => x.ProductId == aProduct.ProductId && x.StoreId == storeId)
                    //        .ToList();

              

                    //    decimal ProductIn = (decimal)product.Where(x => x.isIn == true && x.TransferDate < fromDate).Select(x => x.Quantity).Sum();
                    //    decimal ProductOut = (decimal)product.Where(x => x.isOut == true && x.TransferDate < fromDate).Select(x => x.Quantity).Sum();

                    //    decimal getProductAvilableQty = ProductIn - ProductOut;
                    //    aProduct.OpeningProduct = getProductAvilableQty;


                      
                    //    decimal issue = (decimal)product.Where(x => x.isIn == true && x.ShiftId == shiftId && x.TransferDate.Value.Date == Convert.ToDateTime(printInfo)).Select(x => x.Quantity).Sum();

                    //    aProduct.InProduct = issue;
                    //    aProduct.TotalProduct = aProduct.OpeningProduct + aProduct.InProduct;

                       
                    //    decimal SellproductSelectDate = (decimal)product.Where(x => x.isOut == true && x.ShiftId == shiftId && x.TransferDate.Value.Date == Convert.ToDateTime(printInfo)).Select(x => x.Quantity).Sum();
                    //    aProduct.SellProduct = SellproductSelectDate;

                    //    aProduct.ClosingProduct = aProduct.TotalProduct - aProduct.SellProduct;

                    //    aProduct.TotalPrice = aProduct.UnitPrice * aProduct.SellProduct;

                    //    totalAmount += aProduct.TotalPrice;

                    //    if (getProductAvilableQty > 0 || issue > 0 || SellproductSelectDate > 0)
                    //    {
                    //        products.Add(aProduct);
                    //    }

                    //}


                    products =
                     unitOfWork.CustomRepository.sp_ProductSellHistoryWithOpeningProductForDayShift(storeId, shiftId, fromDate).ToList();


                    if (products.Any())
                    {
                        totalAmount = products.Select(x => x.TotalPrice).Sum();
                    }

                }

                else
                {
                    groupName =
                       unitOfWork.GroupAndShiftMappingRepository.Get()
                           .Where(x => x.TransferDate.Value.Date == Convert.ToDateTime(printInfo))
                           .Select(a => a.Night)
                           .FirstOrDefault();
                    //foreach (VM_Product aProduct in sellsPointProductList)
                    //{
                    //    var product =
                    //    unitOfWork.ProductTransferRepository.Get()
                    //        .Where(x => x.ProductId == aProduct.ProductId && x.StoreId == storeId)
                    //        .ToList();



                    //    decimal ProductIn = (decimal)product.Where(x => x.isIn == true && x.TransferDate < fromDate).Select(x => x.Quantity).Sum();
                    //    decimal ProductOut = (decimal)product.Where(x => x.isOut == true && x.TransferDate < fromDate).Select(x => x.Quantity).Sum();

                    //    decimal getProductAvilableQty = ProductIn - ProductOut;
                      

                    //    decimal issueForDay = (decimal)product.Where(x => x.isIn == true && x.ShiftId == 1 && x.TransferDate.Value.Date == Convert.ToDateTime(printInfo)).Select(x => x.Quantity).Sum();

                      
                    //    decimal totalProductForDay = getProductAvilableQty + issueForDay;


                    //    decimal SellproductSelectDateForDay = (decimal)product.Where(x => x.isOut == true && x.ShiftId == 1 && x.TransferDate.Value.Date == Convert.ToDateTime(printInfo)).Select(x => x.Quantity).Sum();
                       

                    //    decimal closingProductForDay = totalProductForDay - SellproductSelectDateForDay;
                     

                    //    aProduct.OpeningProduct = closingProductForDay;

                    //    decimal issue = (decimal)product.Where(x => x.isIn == true && x.ShiftId == shiftId && x.TransferDate.Value.Date == Convert.ToDateTime(printInfo)).Select(x => x.Quantity).Sum();

                    //    aProduct.InProduct = issue;
                    //    aProduct.TotalProduct = aProduct.OpeningProduct + aProduct.InProduct;


                    //    decimal SellproductSelectDate = (decimal)product.Where(x => x.isOut == true && x.ShiftId == shiftId && x.TransferDate.Value.Date == Convert.ToDateTime(printInfo)).Select(x => x.Quantity).Sum();
                    //    aProduct.SellProduct = SellproductSelectDate;

                    //    aProduct.ClosingProduct = aProduct.TotalProduct - aProduct.SellProduct;

                    //    aProduct.TotalPrice = aProduct.UnitPrice * aProduct.SellProduct;

                    //    totalAmount += aProduct.TotalPrice;

                    //    if (closingProductForDay > 0 || issue > 0 || SellproductSelectDate > 0)
                    //    {
                    //        products.Add(aProduct);
                    //    }

                    //}

                    products =
                   unitOfWork.CustomRepository.sp_ProductSellHistoryWithOpeningProductForNightShift(storeId, shiftId, fromDate).ToList();


                    if (products.Any())
                    {
                        totalAmount = products.Select(x => x.TotalPrice).Sum();
                    }

                }

                //decimal less = (decimal)
                //   unitOfWork.OtherExpenseRepository.Get()
                //       .Where(
                //           x =>
                //               x.StoreId == storeId  && x.ShiftId == shiftId &&
                //               x.TransferDate.Value.Date == Convert.ToDateTime(printInfo))
                //       .Select(x => x.Less)
                //       .Sum();
                //decimal due = (decimal)
                //   unitOfWork.OtherExpenseRepository.Get()
                //       .Where(
                //           x =>
                //               x.StoreId == storeId  && x.ShiftId == shiftId &&
                //               x.TransferDate.Value.Date == Convert.ToDateTime(printInfo))
                //       .Select(x => x.Due)
                //       .Sum();
                //decimal complimen = (decimal)
                //   unitOfWork.OtherExpenseRepository.Get()
                //       .Where(
                //           x =>
                //               x.StoreId == storeId  && x.ShiftId == shiftId &&
                //               x.TransferDate.Value.Date == Convert.ToDateTime(printInfo))
                //       .Select(x => x.Compliment)
                //       .Sum();
                //decimal damage = (decimal)
                //   unitOfWork.OtherExpenseRepository.Get()
                //       .Where(
                //           x =>
                //               x.StoreId == storeId  && x.ShiftId == shiftId &&
                //               x.TransferDate.Value.Date == Convert.ToDateTime(printInfo))
                //       .Select(x => x.Damage)
                //       .Sum();



                decimal less =
                unitOfWork.CustomRepository.sp_OtherExpenseAmountForSellReportWithOpeningProduct(storeId, shiftId,
                    fromDate).Select(a => a.Less).FirstOrDefault();
                decimal due =
                    unitOfWork.CustomRepository.sp_OtherExpenseAmountForSellReportWithOpeningProduct(storeId, shiftId,
                        fromDate).Select(a => a.Due).FirstOrDefault();
                decimal complimen =
                    unitOfWork.CustomRepository.sp_OtherExpenseAmountForSellReportWithOpeningProduct(storeId, shiftId,
                        fromDate).Select(a => a.Compliment).FirstOrDefault();
                decimal damage =
                    unitOfWork.CustomRepository.sp_OtherExpenseAmountForSellReportWithOpeningProduct(storeId, shiftId,
                        fromDate).Select(a => a.Damage).FirstOrDefault();



                totalOtherExpense = less + due + complimen + damage;

                netCash = (double)(totalAmount - totalOtherExpense);


                int restaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString()); 
                string restaurantName = unitOfWork.RestaurantRepository.GetByID(restaurantId).Name;
                string restaurantAddress = unitOfWork.RestaurantRepository.GetByID(restaurantId).Address;
                string storeName = unitOfWork.StoreRepository.GetByID(storeId).store_name;
                //string groupName = unitOfWork.GroupForShiftRepository.GetByID(groupId).GroupName;
                string shiftName = unitOfWork.ShiftRepository.GetByID(shiftId).ShiftName;

                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reports/ProductSellHistoryWithOpeningProductReport.rdlc");
                //localReport.SetParameters(new ReportParameter("Date", fromDate.ToString()));
                localReport.SetParameters(new ReportParameter("Date", printInfo));
                localReport.SetParameters(new ReportParameter("StoreName", storeName.ToString()));
                localReport.SetParameters(new ReportParameter("RestaurantName", restaurantName));
                localReport.SetParameters(new ReportParameter("RestaurantAddress", restaurantAddress));
                localReport.SetParameters(new ReportParameter("GroupName", groupName));
                localReport.SetParameters(new ReportParameter("ShiftName", shiftName));
                localReport.SetParameters(new ReportParameter("Less", less.ToString()));
                localReport.SetParameters(new ReportParameter("Due", due.ToString()));
                localReport.SetParameters(new ReportParameter("Complimen", complimen.ToString()));
                localReport.SetParameters(new ReportParameter("Damage", damage.ToString()));
                localReport.SetParameters(new ReportParameter("TotalOtherExpense", totalOtherExpense.ToString()));
                localReport.SetParameters(new ReportParameter("NetCash", netCash.ToString()));
                ReportDataSource reportDataSource = new ReportDataSource("ProductSellHistoryWithOpeningProductDataSet", products);

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