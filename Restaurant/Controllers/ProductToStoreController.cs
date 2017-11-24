using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using Restaurant.Models.ViewModel;
using Restaurant.Utility;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace Restaurant.Controllers
{
    public class ProductToStoreController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: ProductToStore
        [SessionManger.CheckUserSession]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult ProductToStore()
        {
            return View();
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetAllStoreList()
        {
            try
            {
                IEnumerable<VM_StoreInformation> StoreList = (from a in unitOfWork.StoreRepository.Get().Where(a=>a.is_mainStore==true)
                                                              select new VM_StoreInformation()
                                                              {
                                                                  StoreId = a.store_id,
                                                                  StoreName = a.store_name,
                                                              }).ToList();
                return Json(new { success = true, result = StoreList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [SessionManger.CheckUserSession]
        [Authorize]
        [HttpGet]
        public JsonResult GetStoreInfo(int? storeId)
        {

            try
            {
                var storeInfo = unitOfWork.StoreRepository.GetByID(storeId).isProductionHouseStore;

                return Json(new { success = true, result = storeInfo }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            
        }



        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetAllSupplierList()
        {
            try
            {
                IEnumerable<tblSupplierInformation> supplierList = (from a in unitOfWork.SuppliersInformationRepository.Get()
                                                              select new tblSupplierInformation()
                                                              {
                                                                  SupplierId = a.SupplierId,
                                                                  SupplierName = a.SupplierName,
                                                              }).ToList();
                return Json(new { success = true, result = supplierList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetSupplierProductList(int id)
        {
            try
            {
                IEnumerable<VM_ProductToStore> SupplierProductList;
                  SupplierProductList = (from a in unitOfWork.SuppliersProductRepository.Get().Where(a => a.SupplierId == id)
                                         select new VM_ProductToStore()
                                 {
                                     //StoreId = 
                                     //StoreName = 
                                     SupplierId = id,
                                     SupplierName = unitOfWork.SuppliersInformationRepository.GetByID(id).SupplierName,
                                     ProductId = (int)a.ProductId,
                                     ProductName = a.tblProductInformation.ProductName,
                                     Unit = a.tblProductInformation.Unit,
                                     UnitPrice = (double) a.tblProductInformation.UnitPrice,
                                     ProductTypeId =  a.tblProductInformation.ProductTypeId ?? 0
                                    
                                 }).ToList();
                  return Json(new { success = true, result = SupplierProductList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult SaveProductToStore(int SupplierId, int StoreId, List<VM_ProductToStore> productList)
        {
                try
                {
                    foreach (VM_ProductToStore aProduct in productList)
                    {
                        tblProductTransfer aProductTransfer = new tblProductTransfer();
                        tblProductFromSupplier aProductFromSupplier=new tblProductFromSupplier();
                        aProductFromSupplier.StoreId = aProductTransfer.StoreId = StoreId;
                        aProductFromSupplier.SupplierId = aProductTransfer.SupplierId = SupplierId;
                        aProductFromSupplier.ProductId = aProductTransfer.ProductId = aProduct.ProductId;
                        aProductFromSupplier.Quantity = aProductTransfer.Quantity = aProduct.Quantity;
                        aProductFromSupplier.Unit = aProductTransfer.Unit = aProduct.Unit;
                        aProductFromSupplier.UnitPrice = aProductTransfer.UnitPrice = (decimal?)aProduct.UnitPrice;
                        aProductFromSupplier.isIn = aProductTransfer.isIn = true;
                        aProductFromSupplier.isOut = aProductTransfer.isOut = false;
                        aProductTransfer.TransferDate = DateTime.Now;
                        //aProductFromSupplier = aProductTransfer.CreatedDateTime = DateTime.Now;
                        aProductFromSupplier.DateTime = aProductTransfer.DateTime = DateTime.Now;
                        aProductFromSupplier.RestaurantId = aProductTransfer.RestaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                        aProductFromSupplier.CreatedBy = aProductTransfer.CreatedBy = SessionManger.LoggedInUser(Session);
                        aProductFromSupplier.CreatedDateTime = aProductTransfer.CreatedDateTime = DateTime.Now;
                        aProductFromSupplier.EditedBy = aProductTransfer.EditedBy = null;
                        aProductFromSupplier.EditedDateTime = aProductTransfer.EditedDateTime = null;
                        unitOfWork.ProductTransferRepository.Insert(aProductTransfer);
                        unitOfWork.ProductFromSupplierRepository.Insert(aProductFromSupplier);
                    }
                    unitOfWork.Save();
                    SupplierProductToStoreReport(SupplierId, StoreId, productList);
                   
                    return Json(new { success = true, successMessage = "Product Added Successfully" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
                }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        private void SupplierProductToStoreReport(int SupplierId, int StoreId, List<VM_ProductToStore> productList)
        {
            var newProductList = new List<VM_ProductToStore>();
            foreach (var product in productList)
            {
                VM_ProductToStore newProduct = new VM_ProductToStore();
                newProduct.StoreId = StoreId;
                newProduct.StoreName = unitOfWork.StoreRepository.GetByID(StoreId).store_name;
                newProduct.SupplierId = SupplierId;
                newProduct.SupplierName = product.SupplierName;
                newProduct.ProductName = product.ProductName;
                newProduct.Quantity = product.Quantity;
                newProduct.Unit = product.Unit;
                newProduct.UnitPrice = product.UnitPrice;
                newProductList.Add(newProduct);
            }

            int restaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString()); ;
            string restaurantName = unitOfWork.RestaurantRepository.GetByID(restaurantId).Name;
            string restaurantAddress = unitOfWork.RestaurantRepository.GetByID(restaurantId).Address;

            LocalReport localReport = new LocalReport();                            
            localReport.ReportPath = Server.MapPath("~/Reports/SupplierProductToStoreReport.rdlc");
            localReport.SetParameters(new ReportParameter("RestaurantName", restaurantName));
            localReport.SetParameters(new ReportParameter("RestaurantAddress", restaurantAddress));
            ReportDataSource reportDataSource = new ReportDataSource("SupplierProductToStoreDataSet", newProductList);
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

            var fileName = RenameReportFile(SupplierId, StoreId, DateTime.Now, "Sup", "Main");
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
                ToStore = Convert.ToString(StoreId),
                Supplier = Convert.ToString(SupplierId),
                Date = DateTime.Now,
                ReportName = fileName
            };
            SaveToDatabase(report);
            localReport.Dispose();
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public string RenameReportFile(int fromSupplier, int toMainStore, DateTime date, string fromStoreprefix, string toStoreprefix)
        {
            string newDate = date.Day + "-" + date.Month + "-" + date.Year + "_" + date.Hour + "." + date.Minute + "." + date.Second;
            string fileName = "";
            fileName = fromStoreprefix + "[" + fromSupplier + "]_" + toStoreprefix + "[" + toMainStore + "]_" + newDate
                ;
            return fileName;
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        //[ValidateAntiForgeryToken]
        private void SaveToDatabase(tblChalanReport report)
        {
            unitOfWork.ChalanReport.Insert(report);
            unitOfWork.Save();
        }

    }
}