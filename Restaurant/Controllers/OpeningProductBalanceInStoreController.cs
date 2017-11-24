using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using Microsoft.Ajax.Utilities;
using Restaurant.Models.ViewModel;
using Restaurant.Utility;

namespace Restaurant.Controllers
{
    public class OpeningProductBalanceInStoreController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: OpeningProductBalanceInStore
        [SessionManger.CheckUserSession]
        [Authorize]
       
        public ActionResult OpeningProductBalanceInStore()
        {
            return View();
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetAllStoreList()
        {
            try
            {
                IEnumerable<VM_StoreInformation> StoreList = (from a in unitOfWork.StoreRepository.Get()
                                                              select new VM_StoreInformation()
                                                              {
                                                                  StoreId = a.store_id,
                                                                  StoreName = a.store_name,
                                                                  is_mainStore = a.is_mainStore,
                                                                  isProductionHouseStore = (bool)a.isProductionHouseStore,
                                                                  IsSellsPointStore = (bool)a.IsSellsPointStore

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
        public JsonResult GetAllProductList()
        {
            try
            {
                IEnumerable<VM_ProductList> ProductList = (from a in unitOfWork.ProductRepository.Get()
                                                           select new VM_ProductList()
                                                           {
                                                               ProductId = a.ProductId,
                                                               ProductName = a.ProductName,
                                                               Unit = a.Unit

                                                           }).ToList();
                return Json(new { success = true, result = ProductList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [SessionManger.CheckUserSession]
        [Authorize]
        [HttpPost]
        public JsonResult OpeningProductBalanceInStore(int StoreId, List<VM_ProductToStore> productList, DateTime fromDate)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (unitOfWork.StoreRepository.GetByID(StoreId).is_mainStore == true)
                    {
                        foreach (VM_ProductToStore aProduct in productList)
                        {
                            tblProductTransfer aProductTransfer = new tblProductTransfer();

                            aProductTransfer.StoreId = StoreId;
                            aProductTransfer.TransferDate = fromDate;
                            aProductTransfer.SupplierId = null;
                            aProductTransfer.ProductId = aProduct.ProductId;
                            aProductTransfer.Quantity =  (decimal?) aProduct.OpeningBalance;
                            aProductTransfer.Unit = aProduct.Unit;
                            aProductTransfer.UnitPrice = null;
                            aProductTransfer.isIn = true;
                            aProductTransfer.isOut = false;
                            aProductTransfer.DateTime = DateTime.Now;
                            aProductTransfer.RestaurantId =
                                Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                            aProductTransfer.CreatedBy = SessionManger.LoggedInUser(Session);
                            aProductTransfer.CreatedDateTime = DateTime.Now;
                            aProductTransfer.EditedBy = null;
                            aProductTransfer.EditedDateTime = null;
                            unitOfWork.ProductTransferRepository.Insert(aProductTransfer);
                           
                        }

                        unitOfWork.Save();

                        return Json(new {success = true, successMessage = "Opening Product Balance  Added Successfully"}, JsonRequestBehavior.AllowGet);
                    }

                    if (unitOfWork.StoreRepository.GetByID(StoreId).isProductionHouseStore == true)
                    {
                        foreach (VM_ProductToStore aProduct in productList)
                        {
                            tblProductTransfer product1 = new tblProductTransfer();


                            product1.StoreId = StoreId;
                            product1.TransferDate = fromDate;
                            product1.ProductId = aProduct.ProductId;
                            product1.Quantity = (decimal?)aProduct.OpeningBalance;
                            product1.Unit = aProduct.Unit;
                            product1.isIn = true;
                            product1.isOut = false;
                            product1.DateTime = DateTime.Now;
                            product1.RestaurantId = int.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                            product1.CreatedBy = SessionManger.LoggedInUser(Session);
                            product1.CreatedDateTime = DateTime.Now;
                            product1.EditedBy = null;
                            product1.EditedDateTime = null;
                            unitOfWork.ProductTransferRepository.Insert(product1);
                     

                        }

                        unitOfWork.Save();

                        return Json(new { success = true, successMessage = "Opening Product Balance  Added Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    if (unitOfWork.StoreRepository.GetByID(StoreId).IsSellsPointStore == true)
                    {
                 //var SellsPoint = unitOfWork.SellsPointRepository.Get().Where(x => x.SellsPointStoreId == StoreId).First();
                           

                        foreach (VM_ProductToStore aProduct in productList)
                        {

                            //tblPHtoSPProductTransfer product = new tblPHtoSPProductTransfer();

                            //product.SellsPointId = SellsPoint.SellsPointId;
                            //product.ProductionHouseId = null;
                            //product.ProductId = aProduct.ProductId;
                            //product.Quantity = (decimal?)aProduct.OpeningBalance;
                            //product.Unit = aProduct.Unit;
                            //product.isIn = true;
                            //product.isOut = false;
                            //product.RestaurantId = int.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                            //product.CreatedBy = SessionManger.LoggedInUser(Session);
                            //product.CreatedDateTime = DateTime.Now;
                            //product.EditedBy = null;
                            //product.EditedDateTime = null;
                            //unitOfWork.PHToSPProductTransferRepository.Insert(product);
                            //unitOfWork.Save();

                            tblProductTransfer product2 = new tblProductTransfer();
                            product2.StoreId = StoreId;
                            product2.TransferDate = fromDate;
                            product2.ProductId = aProduct.ProductId;
                            product2.Quantity = (decimal?)aProduct.OpeningBalance;
                            product2.Unit = aProduct.Unit;
                            product2.isIn = true;
                            product2.isOut = false;
                            product2.DateTime = DateTime.Now;
                            product2.RestaurantId = int.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                            product2.CreatedBy = SessionManger.LoggedInUser(Session);
                            product2.CreatedDateTime = DateTime.Now;
                            product2.EditedBy = null;
                            product2.EditedDateTime = null;
                            unitOfWork.ProductTransferRepository.Insert(product2);
                           

                        }
                        unitOfWork.Save();
                        return Json(new { success = true, successMessage = "Opening Product Balance  Added Successfully" }, JsonRequestBehavior.AllowGet);

                    }
                 
                    else
                    {
                        return Json(new { success = false, errorMessage = "Opening Product Balance not Save" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    return Json(new {success = false, errorMessage = ex.Message}, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Model is not Valid" }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}