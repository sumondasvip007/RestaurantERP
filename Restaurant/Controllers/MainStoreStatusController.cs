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
    public class MainStoreStatusController : Controller
    {
        // GET: MainStoreStatus
        UnitOfWork unitOfWork = new UnitOfWork();
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult MainStoreProductStatus()
        {
            return View();
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetAllMainStoreList()
        {
            try
            {
                IEnumerable<VM_StoreInformation> MainStoreList = (from a in unitOfWork.StoreRepository.Get().Where(a => a.is_mainStore == true)
                                                                        select new VM_StoreInformation()
                                                                        {
                                                                            StoreId = a.store_id,
                                                                            StoreName = a.store_name,
                                                                        }).ToList();
                return Json(new { success = true, result = MainStoreList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetAllPurchaseTypeProductList()
        {
            try
            {
                IEnumerable<VM_Product> MainStoreProductList = (from a in unitOfWork.ProductRepository.Get().Where(a => a.ProductTypeId == (int)ProductType.PurchaseTypeProduct || a.ProductTypeId == (int)ProductType.PurchaseAndSellTypeProduct)
                                                                 select new VM_Product()
                                                              {
                                                                  ProductId = a.ProductId,
                                                                  ProductName = a.ProductName,
                                                                  Unit = a.Unit,
                                                                  UnitPrice = (decimal)a.UnitPrice
                                                              }).ToList();
                return Json(new { success = true, result = MainStoreProductList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        //public JsonResult GetMainStoreProductQuantityList(int? storeId, IEnumerable<VM_Product> productList)
        public JsonResult GetMainStoreProductQuantityList(int storeId)
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
                //    if (getProductAvilableQty > 0)
                //    {
                //        aProduct.Quantity = getProductAvilableQty;
                //        aProduct.TotalPrice = aProduct.UnitPrice * getProductAvilableQty;
                //        totalAmount += aProduct.TotalPrice;

                //        products.Add(aProduct);
                //    }

                //    }
                List<DAL.ViewModel.VM_Product> products = unitOfWork.CustomRepository.sp_MainStoreProductStatus(storeId).ToList();

                if (products.Any())
                {
                    totalAmount = products.Select(x => x.Quantity).Sum();
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
        public ActionResult ViewPdfReportForSearchResult(int storeId)
        {
            //IEnumerable<VM_Product> MainStoreProductList = (from a in unitOfWork.ProductRepository.Get().Where(a => a.ProductTypeId == (int)ProductType.PurchaseTypeProduct || a.ProductTypeId == (int)ProductType.PurchaseAndSellTypeProduct)
            //                                                select new VM_Product()
            //                                                {
            //                                                    ProductId = a.ProductId,
            //                                                    ProductName = a.ProductName,
            //                                                    Unit = a.Unit,
            //                                                    UnitPrice = (decimal)a.UnitPrice
            //                                                }).ToList();
            try
            {
                decimal totalAmount = 0;
                //List<VM_Product> products = new List<VM_Product>();
                //foreach (VM_Product aProduct in MainStoreProductList)
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

                List<DAL.ViewModel.VM_Product> products = unitOfWork.CustomRepository.sp_MainStoreProductStatus(storeId).ToList();

                //if (products.Any())
                //{
                //    totalAmount = products.Select(x => x.Quantity).Sum();
                //}


                string storeName = unitOfWork.StoreRepository.GetByID(storeId).store_name;
                int restaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString()); ;
                string restaurantName = unitOfWork.RestaurantRepository.GetByID(restaurantId).Name;
                string restaurantAddress = unitOfWork.RestaurantRepository.GetByID(restaurantId).Address;

                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reports/MainStoreStatusReport.rdlc");
                localReport.SetParameters(new ReportParameter("StoreName", storeName));
                localReport.SetParameters(new ReportParameter("RestaurantName", restaurantName));
                localReport.SetParameters(new ReportParameter("RestaurantAddress", restaurantAddress));
                ReportDataSource reportDataSource = new ReportDataSource("MainStoreStatusDataSet", products);

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