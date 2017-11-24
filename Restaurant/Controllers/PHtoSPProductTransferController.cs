using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using Restaurant.Models.ViewModel;
using Restaurant.Utility;
using System.Transactions;
using System.Web.UI.WebControls;
using DAL.ViewModel;
using Microsoft.Reporting.WebForms;
using Restaurant.Models.Enam;

namespace Restaurant.Controllers
{
    public class PHtoSPProductTransferController : Controller
    {
        // GET: PHtoSPProductTransfer
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult PHtoSPProductTransfer()
        {
            return View();
        }

        UnitOfWork unitOfWork = new UnitOfWork();
        RestaurantEntities res = new RestaurantEntities();

        [Authorize]
        [SessionManger.CheckUserSession]
        //public JsonResult GetProductionHouseList()
        //{
        //    try
        //    {
        //        var productionHouseList = unitOfWork.StoreRepository.Get()
        //            .Where(x => x.isProductionHouseStore == true)
        //            .Select(s => new
        //            {
        //                ProductionHouseId = s.store_id,
        //                ProductionHouseName = s.store_name
        //            })
        //            .ToList();
        //        return Json(new { success = true, result = productionHouseList }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exception)
        //    {
        //        return Json(new { success = true, result = exception.Message });
        //    }
        //}


        public JsonResult GetProductionHouseList()
        {
            try
            {
                var productionHouseList = unitOfWork.StoreRepository.Get()
                    .Where(x => x.isProductionHouseStore == true || x.is_mainStore==true)
                    .Select(s => new
                    {
                        ProductionHouseId = s.store_id,
                        ProductionHouseName = s.store_name
                    })
                    .ToList();
                return Json(new { success = true, result = productionHouseList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { success = true, result = exception.Message });
            }
        }



        //public JsonResult GetProductionHouseList()
        //{
        //    try
        //    {
        //        var productionHouseList = unitOfWork.ProductionHouseInformationRepository.Get()
        //            .Select(s => new
        //            {
        //                s.ProductionHouseId,
        //                s.ProductionHouseName,
        //            })
        //            .ToList();
        //        return Json(new { success = true, result = productionHouseList }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exception)
        //    {
        //        return Json(new { success = true, result = exception.Message });
        //    }
        //}

        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetSellsPointList()
        {
            try
            {
                var sellsPointist = unitOfWork.StoreRepository.Get()
                     .Where(x => x.IsSellsPointStore == true)
                     .Select(s => new
                     {
                         SellsPointStoreId = s.store_id,
                         SellsPointName = s.store_name
                     })
                     .ToList();
                return Json(new { success = true, result = sellsPointist }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { success = true, result = exception.Message });
            }
        }

        //public JsonResult GetSellsPointList()
        //{
        //    try
        //    {
        //        var sellsPointist = unitOfWork.SellsPointRepository.Get()
        //             .Select(s => new
        //             {
        //                 s.SellsPointStoreId,
        //                 s.SellsPointName,
        //             })
        //             .ToList();
        //        return Json(new { success = true, result = sellsPointist }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception exception)
        //    {
        //        return Json(new { success = true, result = exception.Message });
        //    }
        //}

        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetUnits()
        {
            try
            {
                var unitList = unitOfWork.MeasurementUnitRepository.Get()
                    .Select(s => new
                    {
                        id = s.UnitId,
                        unit = s.UnitName
                    })
                    .ToList();
                return Json(new { success = true, result = unitList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { success = false, result = exception.Message });
            }
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

        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetProductionHouseProductList(int? ProductionHouseStoreId)
        {
            try
            {
                //var ProductionHouseID = unitOfWork.StoreRepository.GetByID(ProductionHouseStoreId);


                IEnumerable<VM_PHtoSPProductTransfer> productionHouseProductList = null;

                var mainStoreId = unitOfWork.StoreRepository.Get()
                     .Where(x => x.is_mainStore == true && x.store_id == ProductionHouseStoreId && x.store_id!=2)
                     .Select(s => new
                     {
                         s.store_id,
                         s.store_name
                     })
                     .ToList();


                if (mainStoreId.Any())
                {
                   
                    //productionHouseProductList = unitOfWork.ProductTransferRepository.Get()
                    //    .Where(s => s.StoreId == ProductionHouseStoreId && s.tblProductInformation.ProductTypeId==3)
                    productionHouseProductList=(from s in unitOfWork.ProductRepository.Get().Where(s =>s.ProductTypeId == (int)ProductType.PurchaseAndSellTypeProduct)
                        select new VM_PHtoSPProductTransfer()
                        {
                            IsSelected = false,
                            ProductId =(int) s.ProductId,
                            ProductName = s.ProductName,
                            Unit = s.Unit,
                            AvaliableQuatity =
                                GetAvailableQuantity(Convert.ToInt32(ProductionHouseStoreId), Convert.ToInt32(s.ProductId)),
                            Quantity = 0,
                            //Qty = 0
                        }).ToList();
                       
                        //.Distinct();

                }



                else
                {

                    productionHouseProductList = unitOfWork.ProductionHouseToProductMappingRepository.Get()
                        .Where(s => s.ProductionHouseId == ProductionHouseStoreId)
                        .Select(s => new VM_PHtoSPProductTransfer
                        {
                            IsSelected = false,
                            ProductId = s.ProductId,
                            ProductName = s.tblProductInformation.ProductName,
                            Unit = s.tblProductInformation.Unit,
                            AvaliableQuatity =
                                GetAvailableQuantity(Convert.ToInt32(ProductionHouseStoreId), s.ProductId),
                            Quantity = 0,
                            //Qty = 0
                        })
                        .ToList()
                        .Distinct();

                }

                List<VM_PHtoSPProductTransfer> products = new List<VM_PHtoSPProductTransfer>();
                foreach (VM_PHtoSPProductTransfer aProduct in productionHouseProductList)
                {
                 
                    if (aProduct.AvaliableQuatity > 0)
                    {
                        aProduct.IsSelected = false;
                        aProduct.ProductId = aProduct.ProductId;
                        aProduct.ProductName = aProduct.ProductName;
                        aProduct.Unit = aProduct.Unit;
                        aProduct.AvaliableQuatity = aProduct.AvaliableQuatity;
                        aProduct.Quantity = aProduct.Quantity;
                        products.Add(aProduct);
                    }
                }








                return Json(new { success = true, result = products }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { success = true, result = exception.Message });
            }
        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAvailableProductQuantity(int storeId, int productId)
        {
            try
            {
             decimal  getProductAvilableQty =   GetAvailableQuantity(storeId, productId);


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
        //public JsonResult SaveProductToSellPoint(int SellsPointId, int ProductionHouseStoreId,int shiftId,DateTime fromDate, List<VM_PHtoSPProductTransfer> productList)
        public JsonResult SaveProductToSellPoint(int SellsPointId, int ProductionHouseStoreId,int shiftId, List<VM_PHtoSPProductTransfer> productList)
        {
           
            try
            {

                var productionHouse = unitOfWork.ProductionHouseInformationRepository.Get().Where(x => x.OwnStore == ProductionHouseStoreId).FirstOrDefault();
                //var mainStore = unitOfWork.StoreRepository.Get().Where(x => x.is_mainStore == true && x.store_id == ProductionHouseStoreId && x.store_id!=2).First();
                var SellsPoint = unitOfWork.SellsPointRepository.Get().Where(x => x.SellsPointStoreId == SellsPointId).FirstOrDefault();


                if (productionHouse!=null)
                {
                    foreach (VM_PHtoSPProductTransfer vmProduct in productList)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            tblPHtoSPProductTransfer aProduct = new tblPHtoSPProductTransfer();
                            aProduct.SellsPointId = SellsPoint.SellsPointId;
                            aProduct.ProductionHouseId = productionHouse.ProductionHouseId;
                            //aProduct.GroupId = groupId;
                            aProduct.ShiftId = shiftId;
                            //aProduct.TransferDate = fromDate;
                            aProduct.TransferDate = DateTime.Now; 
                            aProduct.ProductId = vmProduct.ProductId;
                            aProduct.Quantity = (decimal)vmProduct.Quantity;
                            aProduct.Unit = vmProduct.Unit;
                            aProduct.isIn = true;
                            aProduct.RestaurantId = int.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                            aProduct.CreatedBy = SessionManger.LoggedInUser(Session);
                            aProduct.CreatedDateTime = DateTime.Now;
                            aProduct.EditedBy = null;
                            aProduct.EditedDateTime = null;
                            unitOfWork.PHToSPProductTransferRepository.Insert(aProduct);
                            unitOfWork.Save();


                            tblProductTransfer product = new tblProductTransfer();
                            product.StoreId = ProductionHouseStoreId;
                            //product.GroupId = groupId;
                            product.ShiftId = shiftId;
                            //product.TransferDate = fromDate;
                            product.TransferDate = DateTime.Now;
                            product.ProductId = vmProduct.ProductId;
                            product.Quantity = (decimal)vmProduct.Quantity;
                            product.Unit = vmProduct.Unit;
                            product.isOut = true;
                            product.CreatedBy = SessionManger.LoggedInUser(Session);
                            product.CreatedDateTime = DateTime.Now;
                            product.EditedBy = null;
                            product.EditedDateTime = null;
                            unitOfWork.ProductTransferRepository.Insert(product);
                            unitOfWork.Save();

                            tblProductTransfer product2 = new tblProductTransfer();
                            product2.StoreId = SellsPointId;
                            //product2.GroupId = groupId;
                            product2.ShiftId = shiftId;
                            //product2.TransferDate = fromDate;
                            product2.TransferDate = DateTime.Now;
                            product2.ProductId = vmProduct.ProductId;
                            product2.Quantity = (decimal)vmProduct.Quantity;
                            product2.Unit = vmProduct.Unit;
                            product2.isIn = true;
                            product2.CreatedBy = SessionManger.LoggedInUser(Session);
                            product2.CreatedDateTime = DateTime.Now;
                            product2.EditedBy = null;
                            product2.EditedDateTime = null;
                            unitOfWork.ProductTransferRepository.Insert(product2);
                            unitOfWork.Save();
                            scope.Complete();
                        }

                    } 
                }

                else
                {

                    foreach (VM_PHtoSPProductTransfer vmProduct in productList)
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            tblPHtoSPProductTransfer aProduct = new tblPHtoSPProductTransfer();
                            aProduct.SellsPointId = SellsPoint.SellsPointId;
                            aProduct.ProductionHouseId = null;
                            //aProduct.GroupId = groupId;
                            aProduct.ShiftId = shiftId;
                            //aProduct.TransferDate = fromDate;
                            aProduct.TransferDate = DateTime.Now;
                            aProduct.ProductId = vmProduct.ProductId;
                            aProduct.Quantity = (decimal)vmProduct.Quantity;
                            aProduct.Unit = vmProduct.Unit;
                            aProduct.isIn = true;
                            aProduct.RestaurantId = int.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                            aProduct.CreatedBy = SessionManger.LoggedInUser(Session);
                            aProduct.CreatedDateTime = DateTime.Now;
                            aProduct.EditedBy = null;
                            aProduct.EditedDateTime = null;
                            unitOfWork.PHToSPProductTransferRepository.Insert(aProduct);
                            unitOfWork.Save();


                            tblProductTransfer product = new tblProductTransfer();
                            product.StoreId = ProductionHouseStoreId;
                            //product.GroupId = groupId;
                            product.ShiftId = shiftId;
                            //product.TransferDate = fromDate;
                            product.TransferDate = DateTime.Now;
                            product.ProductId = vmProduct.ProductId;
                            product.Quantity = (decimal)vmProduct.Quantity;
                            product.Unit = vmProduct.Unit;
                            product.isOut = true;
                            product.CreatedBy = SessionManger.LoggedInUser(Session);
                            product.CreatedDateTime = DateTime.Now;
                            product.EditedBy = null;
                            product.EditedDateTime = null;
                            unitOfWork.ProductTransferRepository.Insert(product);
                            unitOfWork.Save();

                            tblProductTransfer product2 = new tblProductTransfer();
                            product2.StoreId = SellsPointId;
                            //product2.GroupId = groupId;
                            product2.ShiftId = shiftId;
                            //product2.TransferDate = fromDate;
                            product2.TransferDate = DateTime.Now;
                            product2.ProductId = vmProduct.ProductId;
                            product2.Quantity = (decimal)vmProduct.Quantity;
                            product2.Unit = vmProduct.Unit;
                            product2.isIn = true;
                            product2.CreatedBy = SessionManger.LoggedInUser(Session);
                            product2.CreatedDateTime = DateTime.Now;
                            product2.EditedBy = null;
                            product2.EditedDateTime = null;
                            unitOfWork.ProductTransferRepository.Insert(product2);
                            unitOfWork.Save();
                            scope.Complete();
                        }

                    }
                }

                this.ProductionHouseToSellsPointReport(Convert.ToInt32(SellsPointId), Convert.ToInt32(ProductionHouseStoreId), productList);
                //unitOfWork.Save();
                return Json(new { success = true, successMessage = " Successfully Added" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { success = false, errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        [Authorize]
        [SessionManger.CheckUserSession]
        private void ProductionHouseToSellsPointReport(int SellsPointId, int ProductionHouseStoreId, List<VM_PHtoSPProductTransfer> productList)
        {

            var newProductList = new List<VM_PHtoSPProductTransfer>();
            int serial = 0;

            foreach (var product in productList)
            {
                VM_PHtoSPProductTransfer newProduct = new VM_PHtoSPProductTransfer();
                newProduct.SL = ++serial;
                newProduct.ProductName = unitOfWork.ProductRepository.GetByID(product.ProductId).ProductName; ;
                newProduct.Quantity = product.Quantity;
                newProduct.Unit = product.Unit;
                newProduct.ProductionHouseStoreName =
                    unitOfWork.StoreRepository.GetByID(ProductionHouseStoreId).store_name;
           
                newProduct.SellsPointName = unitOfWork.StoreRepository.GetByID(SellsPointId).store_name;
                newProductList.Add(newProduct);
            }

            int restaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString()); ;
            string restaurantName = unitOfWork.RestaurantRepository.GetByID(restaurantId).Name;
            string restaurantAddress = unitOfWork.RestaurantRepository.GetByID(restaurantId).Address;

            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/ProductionHouseToSellPointReport.rdlc");
            localReport.SetParameters(new ReportParameter("RestaurantName", restaurantName));
            localReport.SetParameters(new ReportParameter("RestaurantAddress", restaurantAddress));
            ReportDataSource reportDataSource = new ReportDataSource("ProductionHouseToSellPointReport", newProductList);
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
            //rename file 
            var fileName = RenameReportFile(ProductionHouseStoreId,SellsPointId,DateTime.Now,"Pro","Sell");

            var path = System.IO.Path.Combine(Server.MapPath("~/pdfReport"));


            var saveAs = path+ "\\" + fileName + ".pdf" ;

            var idx = 0;
            while (System.IO.File.Exists(saveAs))
            {
                idx++;
                saveAs = string.Format("{0}.{1}.pdf", Path.Combine(path, fileName), idx);
            }

            using (var stream = new FileStream(saveAs, FileMode.Create, FileAccess.Write))
            {
                stream.Write(renderedBytes, 0, renderedBytes.Length);
                stream.Close();
            }
            var report =  new tblChalanReport();
            report.FromStore = Convert.ToString(ProductionHouseStoreId);
            report.ToStore = Convert.ToString(SellsPointId);
            report.Date = DateTime.Now.Date;
            report.ReportName = fileName;
            
            SaveToDatabase(report);

            localReport.Dispose();
        }

        public string RenameReportFile(int fromStore , int ToStore , DateTime date , string fromStoreprefix , string toStoreprefix)
        {
            string newDate = date.Day +"-" +date.Month + "-" +date.Year+"_"+ date.Hour+"."+date.Minute+"."+date.Second;
            string fileName = "";
            fileName = fromStoreprefix + "[" + fromStore + "]_" + toStoreprefix + "[" + ToStore + "]_" + newDate    
                ;
            return fileName;
        }

        private void SaveToDatabase(tblChalanReport report)
        {
           unitOfWork.ChalanReport.Insert(report);
           unitOfWork.Save();
        }

        public decimal GetAvailableQuantity(int storeId, int productId)
        {
            //var store = unitOfWork.StoreRepository.GetByID(storeId);

            //var product =
            //    unitOfWork.ProductTransferRepository.Get()
            //        .Where(x => x.ProductId == productId && x.StoreId == storeId)
            //        .ToList();

            //decimal ProductIn = (decimal)product.Where(x => x.isIn == true).Select(x => x.Quantity).Sum();
            //decimal ProductOut = (decimal)product.Where(x => x.isOut == true).Select(x => x.Quantity).Sum();

            //decimal getProductAvilableQty = ProductIn - ProductOut;
            decimal getProductAvilableQty = unitOfWork.CustomRepository.sp_AvailableQuantityForProductToProductionHouse(storeId, productId).Select(a => a.Quantity).FirstOrDefault();

            //if (getProductAvilableQty < 1)
            //    getProductAvilableQty = 0;
            return getProductAvilableQty;
        }




    }
}