using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using DAL.ViewModel;
using Microsoft.Ajax.Utilities;
using Restaurant.Models.ViewModel;
using Restaurant.Utility;

namespace Restaurant.Controllers
{
    public class ProductSoldController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult ProductSoldForm()
        {
            return View();

        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllSellsPoint()
        {
            try
            {
                //var sellsPointList = unitOfWork.StoreRepository.Get()
                //    .Where(a => a.IsSellsPointStore == true)
                //    .Select(s => new
                //    {
                //        s.store_name,s.store_id

                //    }).ToList();
                var sellsPointList = (from a in unitOfWork.StoreRepository.Get().Where(a => a.IsSellsPointStore == true)
                                      select new tblStoreInformation()
                                      {
                                          store_id = a.store_id,
                                          store_name = a.store_name
                                      }).ToList();



                return Json(new { success = true, result = sellsPointList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
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
        //public JsonResult GetProductSold(int sellsPointId)
        //{
        //    try
        //    {
        //        var Product =
        //            unitOfWork.ProductTransferRepository.Get()
        //                .Where(a => a.StoreId == sellsPointId).DistinctBy(a => a.ProductId)
        //                .Select(p => new VM_ProductSold()
        //                {
        //                  ProductName = unitOfWork.ProductRepository.GetByID(p.ProductId).ProductName,
        //                  ProductId = (int)p.ProductId,
        //                  Unit = unitOfWork.ProductRepository.GetByID(p.ProductId).Unit,
        //                  UnitPrice = unitOfWork.ProductRepository.GetByID(p.ProductId).UnitPrice,
        //                  AvailableQuantity = AvailableQuantity(sellsPointId, Convert.ToInt32(p.ProductId))                        
                     
        //                }).ToList();
        //        return Json(new { success = true, result = Product }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public JsonResult GetProductSold(int sellsPointId, int shiftId, DateTime fromDate)
        public JsonResult GetProductSold(int sellsPointId, int shiftId)
        {
            try
            {
                DateTime today = DateTime.Now;

                var dateToday = DateTime.Parse(today.ToString());
                var date = string.Format("{0:yyyy-MM-dd}", dateToday);

                List<DAL.ViewModel.VM_Product> Product;
                if (shiftId==1)
                {
                    Product =
                       unitOfWork.CustomRepository.sp_ProductSoldForDayShift(sellsPointId, shiftId, Convert.ToDateTime(date)).ToList();
                     //Product = unitOfWork.CustomRepository.sp_ProductSoldForDayShift(sellsPointId, shiftId, fromDate).ToList();
                }

                else
                {
                    Product =
                       unitOfWork.CustomRepository.sp_ProductSoldForNightShift(sellsPointId, shiftId, Convert.ToDateTime(date)).ToList();
                    //Product = unitOfWork.CustomRepository.sp_ProductSoldForNightShift(sellsPointId, shiftId, fromDate).ToList();
                }
                  
                return Json(new { success = true, result = Product }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [Authorize]
        [SessionManger.CheckUserSession]
        //public JsonResult GetAvailableQuantity(int sellsPointId, int productId)
        //{
        //    //first check prodt transfer table where sellspointid = storeid && productid== prodid

        //    try
        //    {
        //       var availableProduct = AvailableQuantity(sellsPointId, productId);
        //        return Json(new { success = true, result = availableProduct }, JsonRequestBehavior.AllowGet);

        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }


        //}

        //public JsonResult GetAvailableQuantity(int sellsPointId,int shiftId, int productId,DateTime fromDate)
        public JsonResult GetAvailableQuantity(int sellsPointId,int shiftId, int productId)
        {
            //first check prodt transfer table where sellspointid = storeid && productid== prodid

            try
            {
                DateTime today = DateTime.Now;

                var dateToday = DateTime.Parse(today.ToString());
                var date = string.Format("{0:yyyy-MM-dd}", dateToday);

                var availableProduct = AvailableQuantity(sellsPointId, shiftId, productId, Convert.ToDateTime(date));

                //var availableProduct = AvailableQuantity(sellsPointId, shiftId, productId, fromDate);
                return Json(new { success = true, result = availableProduct }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

        [Authorize]
        [SessionManger.CheckUserSession]
        //public JsonResult AddSoldProductInfoToProductTransfer(int storeId, int shiftId, DateTime fromDate, List<VM_ProductSold> productsoldList)
        public JsonResult AddSoldProductInfoToProductTransfer(int storeId, int shiftId, List<VM_ProductSold> productsoldList)
        {
            try
            {
                foreach (var productsold in productsoldList)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        tblProductTransfer productTransfer = new tblProductTransfer();
                        productTransfer.StoreId = storeId;
                        //productTransfer.GroupId = groupId;
                        productTransfer.ShiftId = shiftId;
                        //productTransfer.TransferDate = fromDate;
                        productTransfer.TransferDate = DateTime.Now;
                        productTransfer.ProductId = productsold.ProductId;
                        productTransfer.Quantity = productsold.Quantity;
                        productTransfer.Unit = unitOfWork.ProductRepository.GetByID(productsold.ProductId).Unit;
                        productTransfer.UnitPrice = unitOfWork.ProductRepository.GetByID(productsold.ProductId).UnitPrice;
                        productTransfer.isOut = true;
                        productTransfer.DateTime = DateTime.Now;
                        productTransfer.CreatedBy = SessionManger.LoggedInUser(Session);
                        productTransfer.EditedBy = null;
                        productTransfer.RestaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                        productTransfer.CreatedDateTime = DateTime.Now;
                        unitOfWork.ProductTransferRepository.Insert(productTransfer);
                        unitOfWork.Save();



                        
                        //add product to productSold table

                        tblProductSold productSold = new tblProductSold();

                        productSold.StoreId = storeId;
                        //productSold.GroupId = groupId;
                        productSold.ShiftId = shiftId;
                        //productSold.TransferDate = fromDate;
                        productSold.TransferDate = DateTime.Now;
                        productSold.ProductId = productsold.ProductId;
                        productSold.Quantity = productsold.Quantity;
                        productSold.Unit = unitOfWork.ProductRepository.GetByID(productsold.ProductId).Unit;
                        productSold.UnitPrice = unitOfWork.ProductRepository.GetByID(productsold.ProductId).UnitPrice;
                        productSold.DateTime = DateTime.Now;
                        productSold.CreatedBy = SessionManger.LoggedInUser(Session);
                        productSold.EditedBy = null;
                        productSold.RestaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                        productSold.CreatedDateTime = DateTime.Now;
                        unitOfWork.ProductSoldRepository.Insert(productSold);
                        unitOfWork.Save();


                        if (shiftId == 1)
                        {
                            unitOfWork.CustomRepository.sp_InsertTotalAmountDailyByShiftWise();
                        }
                        else
                        {
                            unitOfWork.CustomRepository.sp_InsertTotalAmountDailyForNightShift();
                        }

                        scope.Complete();


                    

                    }
                }
                return Json(new { success = true, successMessage = "Product Sold" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            //return Json(new { success = true, successMessage = "Product Sold" });
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        //public decimal? AvailableQuantity(int sellsPointId, int productId)
        //{
        //    //first check prodt transfer table where sellspointid = storeid && productid== prodid

        //    try
        //    {
        //        var productTransferbyPIdAndSId =
        //            unitOfWork.ProductTransferRepository.Get()
        //                .Where(a => a.StoreId == sellsPointId && a.ProductId == productId).ToList();
        //        //in this sellspoint id how many product in 
        //        var numberOfProductIn = productTransferbyPIdAndSId.Where(a => a.isIn == true).Sum(a => a.Quantity);
        //        //in this sellspoint id how many product out
        //        var numberOfProductOut = productTransferbyPIdAndSId.Where(a => a.isOut == true).Sum(a => a.Quantity);
        //        var availableProduct = numberOfProductIn - numberOfProductOut;

        //        return availableProduct;

        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }


        //}


        //public decimal? AvailableQuantity(int sellsPointId, int shiftId, int productId, DateTime fromDate)
        public decimal? AvailableQuantity(int sellsPointId, int shiftId, int productId, DateTime fromDate)
        {
           

            try
            {
                DateTime today = DateTime.Now;

                var dateToday = DateTime.Parse(today.ToString());
                var date = string.Format("{0:yyyy-MM-dd}", dateToday);




                decimal availableProduct;
                if (shiftId == 1)
                {
                    availableProduct = unitOfWork.CustomRepository.sp_CheckAvailableQuantityForProductForProductSoldForDayShift(sellsPointId, shiftId, productId, Convert.ToDateTime(date)).Select(a => a.AvailableQuantity).FirstOrDefault();
                    //availableProduct = unitOfWork.CustomRepository.sp_CheckAvailableQuantityForProductForProductSoldForDayShift(sellsPointId, shiftId, productId, fromDate).Select(a => a.AvailableQuantity).FirstOrDefault();
                }

                else
                {
                    
                    availableProduct = unitOfWork.CustomRepository.sp_CheckAvailableQuantityForProductForProductSoldForNightShift(sellsPointId, shiftId, productId, Convert.ToDateTime(date)).Select(a => a.AvailableQuantity).FirstOrDefault();
                    //availableProduct = unitOfWork.CustomRepository.sp_CheckAvailableQuantityForProductForProductSoldForNightShift(sellsPointId, shiftId, productId, fromDate).Select(a => a.AvailableQuantity).FirstOrDefault();
                }
                  

                return availableProduct;

            }
            catch (Exception ex)
            {
                return 0;
            }


        }

    }
}