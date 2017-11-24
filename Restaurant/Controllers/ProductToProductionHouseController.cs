using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using Microsoft.Ajax.Utilities;
using Microsoft.Reporting.WebForms;
using Restaurant.Models.ViewModel;
using Restaurant.Utility;

namespace Restaurant.Controllers
{
    public class ProductToProductionHouseController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
       
        [SessionManger.CheckUserSession]
        [Authorize]
     
       

        public ActionResult ProductToProductionHouse()
        {
            return View();
        }


        
        [HttpPost]
        [SessionManger.CheckUserSession]
        [Authorize]

        public JsonResult GetAllProductByOwnStore(int? id)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                //view give me own store id i have to convert it to productHouse Id 
                var productionHouseInfo = unitOfWork.ProductionHouseInformationRepository.Get().Where(a => a.OwnStore == id).FirstOrDefault();

                //var productId = unitOfWork.ProductTransferRepository.Get().Where(a =>
                //{
                //    return productionHouseInfo != null && a.StoreId == productionHouseInfo.MainStore;
                //}).ToList().DistinctBy(a=>a.ProductId);

                var productId =
                    unitOfWork.CustomRepository.sp_MainStoreProductStatus(productionHouseInfo.MainStore).ToList();

                var productList = new List<Vm_ProductTransfetToProductionHouse>();
                foreach (var aProduct in productId)
                {
                    if (aProduct != null)
                    {
                        Vm_ProductTransfetToProductionHouse newProduct = new Vm_ProductTransfetToProductionHouse();
                        var productInfo = unitOfWork.ProductRepository.GetByID(aProduct.ProductId);
                        
                        if (productInfo != null)
                        {       
                                newProduct.MainStoreQuantity = Convert.ToDecimal(aProduct.Quantity);                               
                                newProduct.ProductInformation.ProductId = productInfo.ProductId;
                                newProduct.ProductInformation.ProductName = productInfo.ProductName;
                                newProduct.ProductInformation.Unit = productInfo.Unit;
                                newProduct.AvailableQuatity = ProductAvailableQuantity(productionHouseInfo.OwnStore,
                                Convert.ToInt32(aProduct.ProductId));
                                productList.Add(newProduct);
                        }
                    }
                }
                return Json(new { result = productList, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetSuppierInfoByProductId(int? productId)
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork();
               var suppier = unitOfWork.SuppliersProductRepository.Get().Where(a => a.ProductId == productId).Select(
                    a => new 
                    {
                        a.SupplierId,
                        a.ProductId                       
                    }).FirstOrDefault();

               return Json(new { result = suppier, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }


       
        [Authorize]
        [SessionManger.CheckUserSession]
        [HttpPost]
        public JsonResult AddProductToProductionHouse( List<Vm_ProductTransfetToProductionHouse> ProductList)
        {
            try
            {
                tblStoreInformation mainStore = null;
                tblProductTransfer productTransfer = null;
                foreach (var productTransfetToProductionHouse in ProductList) 
                {
                    if (productTransfetToProductionHouse.Quantity != null)
                    {


                        productTransfer = new tblProductTransfer();
                        productTransfer.DateTime = DateTime.Now;
                        productTransfer.CreatedBy = SessionManger.LoggedInUser(Session);
                        productTransfer.EditedBy = null;
                        productTransfer.RestaurantId =
                            Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                        productTransfer.CreatedDateTime = DateTime.Now;
                        productTransfer.StoreId = productTransfetToProductionHouse.StoreId;
                        productTransfer.SupplierId = null;
                        productTransfer.ProductId = productTransfetToProductionHouse.ProductId;
                        productTransfer.Quantity = productTransfetToProductionHouse.Quantity;
                        productTransfer.TransferDate = DateTime.Now;
                        productTransfer.isIn = true;
                        productTransfer.Unit = productTransfetToProductionHouse.Unit;
                        unitOfWork.ProductTransferRepository.Insert(productTransfer);
                        unitOfWork.Save();
                        //--------- Get Main Store of  --> Own Store---------------
                        mainStore = unitOfWork.StoreRepository.GetByID(productTransfer.StoreId);
                        //----------------- Out Product to Main Store  ------------
                        tblProductTransfer productTransfer2 = new tblProductTransfer();
                        productTransfer2.StoreId = mainStore.ParentStoreId;
                        productTransfer2.SupplierId = null;
                        productTransfer2.ProductId = productTransfetToProductionHouse.ProductId;
                        productTransfer2.Quantity = productTransfetToProductionHouse.Quantity;
                        productTransfer2.TransferDate = DateTime.Now;
                        productTransfer2.Unit = productTransfetToProductionHouse.Unit;
                        productTransfer2.isOut = true;
                        productTransfer2.CreatedDateTime = DateTime.Now;
                        unitOfWork.ProductTransferRepository.Insert(productTransfer2);
                        productTransfer2.DateTime = DateTime.Now;
                        productTransfer2.CreatedBy = SessionManger.LoggedInUser(Session);
                        productTransfer2.EditedBy = null;
                        productTransfer2.RestaurantId =
                            Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                        unitOfWork.Save();
                    }
                }
                ProductToProductionHouseReport(Convert.ToInt32(mainStore.ParentStoreId), Convert.ToInt32(productTransfer.StoreId), ProductList);

             
                //----------- In a  Product  to Own Store------------------
               
                return Json(new { success = true, successMessage = "Product Transfer to Product House  Successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetProductAvailableQuantity(
            int storeId ,  int productId)
        {
            try
            {
                decimal getProductAvilableQty = ProductAvailableQuantity(storeId, productId);

             // var getProductAvilableQty = unitOfWork.CustomRepository.sp_GetProductAvailableQuantity(storeId,productId);
                return Json(new { result = getProductAvilableQty, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        [Authorize]
        [SessionManger.CheckUserSession]
        private void ProductToProductionHouseReport(int mainStoreId, int productionHouseId, List<Vm_ProductTransfetToProductionHouse> productList)
        {

            var newProductList = new List<Vm_ProductTransfetToProductionHouse>();
           
            foreach (var product in productList)
            {
               Vm_ProductTransfetToProductionHouse newProduct = new Vm_ProductTransfetToProductionHouse();

                newProduct.MainStoreId =  mainStoreId;
                newProduct.MainStoreName = unitOfWork.StoreRepository.GetByID(mainStoreId).store_name;
                newProduct.StoreId = productionHouseId;
                newProduct.StoreName = unitOfWork.StoreRepository.GetByID(productionHouseId).store_name;
                newProduct.ProductId = product.ProductId;
                newProduct.ProductName = unitOfWork.ProductRepository.GetByID(product.ProductId).ProductName;
                newProduct.Quantity = product.Quantity;
                newProduct.Unit = product.Unit;
                newProductList.Add(newProduct);

            }

            int restaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString()); ;
            string restaurantName = unitOfWork.RestaurantRepository.GetByID(restaurantId).Name;
            string restaurantAddress = unitOfWork.RestaurantRepository.GetByID(restaurantId).Address;

            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/ProductToProductionHouseReport.rdlc");
            localReport.SetParameters(new ReportParameter("RestaurantName", restaurantName));
            localReport.SetParameters(new ReportParameter("RestaurantAddress", restaurantAddress));
            ReportDataSource reportDataSource = new ReportDataSource("ProductToProductionHouseDataSet", newProductList);
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
            var fileName = RenameReportFile(mainStoreId, productionHouseId, DateTime.Now, "Main", "Pro");
            var path = System.IO.Path.Combine(Server.MapPath("~/pdfReport"));


            //var saveAs = string.Format("{0}.pdf", Path.Combine(path, "myfilename"));
            var saveAs = path + "\\" + fileName + ".pdf";

            var idx = 0;
            while (System.IO.File.Exists(saveAs))
            {
                idx++;
                saveAs = string.Format("{0}.{1}.pdf", Path.Combine(path, "myfilename"), idx);
            }

            using (var stream = new FileStream(saveAs, FileMode.Create, FileAccess.Write))
            {
                stream.Write(renderedBytes, 0, renderedBytes.Length);
                stream.Close();
            }

            tblChalanReport report = new tblChalanReport()
            {
                ToStore = Convert.ToString(mainStoreId),
                FromStore = Convert.ToString(productionHouseId),
                Date = DateTime.Now,
                ReportName = fileName


            };
            SaveToDatabase(report);
            localReport.Dispose();
        }
        public string RenameReportFile(int fromMainStore, int toProductionHouse, DateTime date, string fromStoreprefix, string toStoreprefix)
        {
            string newDate = date.Day + "-" + date.Month + "-" + date.Year + "_" + date.Hour + "." + date.Minute + "." + date.Second;
            string fileName = "";
            fileName = fromStoreprefix + "[" + fromMainStore + "]_" + toStoreprefix + "[" + toProductionHouse + "]_" + newDate
                ;
            return fileName;
        }
        private void SaveToDatabase(tblChalanReport report)
        {
            unitOfWork.ChalanReport.Insert(report);
            unitOfWork.Save();
        }
        private decimal ProductAvailableQuantity(
            int storeId, int productId)
        {
            try
            {
                var store = unitOfWork.StoreRepository.GetByID(storeId);
                //var product =
                //    unitOfWork.ProductTransferRepository.Get()
                //        .Where(x => x.ProductId == productId && x.StoreId == store.ParentStoreId)
                //        .ToList();

                //decimal ProductIn = (decimal)product.Where(x => x.isIn == true).Select(x => x.Quantity).Sum();
                //decimal ProductOut = (decimal)product.Where(x => x.isOut == true).Select(x => x.Quantity).Sum();

                //decimal getProductAvilableQty = ProductIn - ProductOut;

                decimal getProductAvilableQty = unitOfWork.CustomRepository.sp_AvailableQuantityForProductToProductionHouse((int)store.ParentStoreId, productId).Select(a => a.Quantity).FirstOrDefault();
                return getProductAvilableQty;
            }
            catch (Exception exception)
            {
                return 0;
            }

        }



    }
}