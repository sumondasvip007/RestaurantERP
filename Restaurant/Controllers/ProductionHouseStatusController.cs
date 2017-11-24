using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using Microsoft.Reporting.WebForms;
using Restaurant.Models.Enam;
using Restaurant.Models.ViewModel;
using Restaurant.Utility;

namespace Restaurant.Controllers
{
    public class ProductionHouseStatusController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult ProductionHouseStatus()
        {
            return View();
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetSellAbleProductByProductionHouse(int storeId)
        {
            //Get All seallableProduct         
            try
            {

                //var products =  GetSellAbleProductByProductionHouseList(storeId);
                var products =  unitOfWork.CustomRepository.sp_SellableProductStatusInProductionHouse(storeId);

                return Json(new { success = true, result = products }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { success = false, result = exception.Message });
            }


        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetPursableProductByProductionHouse(int storeId)
        {
            try
            {
                //var products = GetPursableProductByProductionHouseList(storeId);
                var products = unitOfWork.CustomRepository.sp_PuschaseableProductStatusInProductionHouse(storeId);
                

                return Json(new { success = true, result = products }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { success = false, result = exception.Message });
            }



        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public List<VM_Product> GetPursableProductByProductionHouseList(int storeId)
        {
            //unitOfWork.SuppliersProductRepository.Get().Where(a=>a.)
            //Get All pursable Product  
            var pursableProduct = unitOfWork.ProductRepository.Get().Where(a => a.ProductTypeId == (int)ProductType.PurchaseTypeProduct || a.ProductTypeId == (int)ProductType.PurchaseAndSellTypeProduct).ToList();

            //list of pusable product status
            List<VM_Product> products = new List<VM_Product>();

            foreach (var product in pursableProduct)
            {
                VM_Product vm_Product = new VM_Product();

                vm_Product.ProductName = product.ProductName;
                vm_Product.Unit = product.Unit;

                vm_Product.UnitPrice =
                    Convert.ToDecimal(unitOfWork.ProductRepository.GetByID(product.ProductId).UnitPrice);


                //product tranfer history ,list of product in and list of product out 
                var productTranfer =
                    unitOfWork.ProductTransferRepository.Get()
                        .Where(a => a.ProductId == product.ProductId && a.StoreId == storeId).ToList();

                var productIn = (decimal)productTranfer.Where(a => a.isIn == true).Select(a => a.Quantity).Sum();

                var productOut = (decimal)productTranfer.Where(a => a.isOut == true).Select(a => a.Quantity).Sum();

                if (productIn > productOut)
                {
                    vm_Product.Quantity = Convert.ToDecimal(productIn - productOut);

                    vm_Product.TotalPrice = Convert.ToDecimal(vm_Product.Quantity * vm_Product.UnitPrice);

                    products.Add(vm_Product);
                }

            }
            return products;
        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public List<VM_Product> GetSellAbleProductByProductionHouseList(int storeId)
        {
                   
          
                var sellAbleProduct = unitOfWork.ProductionHouseToProductMappingRepository.Get().Where(a => a.ProductionHouseId == storeId).ToList();

                //list of pusable product status
                List<VM_Product> products = new List<VM_Product>();

                foreach (var product in sellAbleProduct)
                {
                    VM_Product vm_Product = new VM_Product();
                    vm_Product.ProductName = product.tblProductInformation.ProductName;
                    vm_Product.Unit = product.tblProductInformation.Unit;

                    vm_Product.UnitPrice =
                  Convert.ToDecimal(unitOfWork.ProductRepository.GetByID(product.ProductId).UnitPrice);
                    //product tranfer history ,list of product in and list of product out 
                    var productTranfer =
                        unitOfWork.ProductTransferRepository.Get()
                            .Where(a => a.ProductId == product.ProductId && a.StoreId == storeId).ToList();

                    var productIn = productTranfer.Where(a => a.isIn == true).Select(a => a.Quantity).Sum();

                    var productOut = productTranfer.Where(a => a.isOut == true).Select(a => a.Quantity).Sum();
                    if (productIn > productOut)
                    {
                        vm_Product.Quantity = Convert.ToDecimal(productIn - productOut);

                        vm_Product.TotalPrice = Convert.ToDecimal(vm_Product.Quantity * vm_Product.UnitPrice);

                        products.Add(vm_Product);
                    }
                }

                return products;
                //return Json(new { success = true, result = products }, JsonRequestBehavior.AllowGet);
            }

        [SessionManger.CheckUserSession]
        [Authorize]
        public ActionResult ViewPdfReportForSellebleProduct(int storeId)
        {
            try
            {

                string storeName = unitOfWork.StoreRepository.GetByID(storeId).store_name;
                int restaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString()); ;
                string restaurantName = unitOfWork.RestaurantRepository.GetByID(restaurantId).Name;
                string restaurantAddress = unitOfWork.RestaurantRepository.GetByID(restaurantId).Address;

                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reports/ProductionHouseStatusReport.rdlc");
     
               // localReport.SetParameters(new ReportParameter("StoreName", StoreName));
                var a = unitOfWork.CustomRepository.sp_SellableProductStatusInProductionHouse(storeId);
                localReport.SetParameters(new ReportParameter("RestaurantName", restaurantName));
                localReport.SetParameters(new ReportParameter("RestaurantAddress", restaurantAddress));
                localReport.SetParameters(new ReportParameter("StoreName", storeName));
                ReportDataSource reportDataSource = new ReportDataSource("ProductionHouseSellableProductStatusDataSet", a);

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

        [SessionManger.CheckUserSession]
        [Authorize]
        public ActionResult ViewPdfReportForPurchasableProduct(int storeId)
        {
            try
            {

                string storeName = unitOfWork.StoreRepository.GetByID(storeId).store_name;
                int restaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString()); ;
                string restaurantName = unitOfWork.RestaurantRepository.GetByID(restaurantId).Name;
                string restaurantAddress = unitOfWork.RestaurantRepository.GetByID(restaurantId).Address;

                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reports/ProductionHouseStatusReport.rdlc");

                // localReport.SetParameters(new ReportParameter("StoreName", StoreName));
                var a = unitOfWork.CustomRepository.sp_PuschaseableProductStatusInProductionHouse(storeId);
                localReport.SetParameters(new ReportParameter("RestaurantName", restaurantName));
                localReport.SetParameters(new ReportParameter("RestaurantAddress", restaurantAddress));
                localReport.SetParameters(new ReportParameter("StoreName", storeName));
                ReportDataSource reportDataSource = new ReportDataSource("ProductionHouseSellableProductStatusDataSet", a);

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
