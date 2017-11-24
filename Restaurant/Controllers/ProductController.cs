using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using Restaurant.Models.ViewModel;
using Restaurant.Utility;

namespace Restaurant.Controllers
{

    public class ProductController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public const int purchasableProductType = 1;
        public const int sellableproductType = 2;
        // GET: Product
        [SessionManger.CheckUserSession]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult ProductAdd()
        {
            return View();
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult SaveProduct(VM_Product aProduct)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tblProductInformation product = new tblProductInformation();
                    product.ProductName = aProduct.ProductName;
                    product.ProductTypeId = aProduct.ProductTypeId;
                    product.StoreId = aProduct.StoreId;
                    product.Unit = aProduct.Unit;
                    product.UnitPrice = (decimal)aProduct.UnitPrice;
                    product.ProductionCost = (decimal)aProduct.ProductionCost;
                    product.RestaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                    product.CreatedBy = SessionManger.LoggedInUser(Session);
                    product.CreatedDateTime = DateTime.Now;
                    product.EditedBy = null;
                    product.EditedDateTime = null;
                    unitOfWork.ProductRepository.Insert(product);
                    unitOfWork.Save();
                    return Json(new { success = true, successMessage = "Product Added Successfully." });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Model is not Valid" }, JsonRequestBehavior.AllowGet);
            }
        }

        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetAllProductType()
        {
            try
            {

                var productTypeList = (from a in unitOfWork.ProductTypeRepository.Get()
                                       select new VM_ProductType()
                                   {

                                       ProductTypeId = a.ProductTypeId,
                                       ProductTypeName = a.ProductTypeName

                                   }).ToList();
                return Json(new { success = true, result = productTypeList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetStoreInformation(int? id)
        {
            try
            {
                IEnumerable<VM_StoreInformation> StoreList;
                if (id == 1)
                {
                    StoreList = (from a in unitOfWork.StoreRepository.Get().Where(a => a.is_mainStore == true)
                                 select new VM_StoreInformation()
                                 {
                                     StoreId = a.store_id,
                                     StoreName = a.store_name,
                                 }).ToList();
                }
                else
                {
                    StoreList = (from a in unitOfWork.StoreRepository.Get().Where(a => a.isProductionHouseStore == true)
                                 select new VM_StoreInformation()
                                 {
                                     StoreId = a.store_id,
                                     StoreName = a.store_name,
                                 }).ToList();
                }

                return Json(new { success = true, result = StoreList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetAllProduct()
        {
            try
            {

                var productList = (from a in unitOfWork.ProductRepository.Get()
                                   select new VM_Product()
                                   {
                                       ProductId = a.ProductId,
                                       ProductName = a.ProductName,
                                       ProductTypeId = (int) a.ProductTypeId,
                                       ProductTypeName = a.tblProductType.ProductTypeName,
                                       StoreId = (int) a.StoreId,
                                       StoreName = a.tblStoreInformation.store_name,
                                       Unit = a.Unit,
                                       UnitPrice = (decimal)a.UnitPrice,
                                       ProductionCost = a.ProductionCost

                                   }).ToList();
                return Json(new { success = true, result = productList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [SessionManger.CheckUserSession]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult ProductList()
        {
            return View();
        }

        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult DeleteProduct(int? id)
        {
            try
            {
                tblProductInformation aProduct = unitOfWork.ProductRepository.GetByID(id);
                if (aProduct == null)
                {
                    return Json(new { success = false, errorMessage = "Product Delete Failed." }, JsonRequestBehavior.AllowGet);
                }
                unitOfWork.ProductRepository.Delete(aProduct);
                unitOfWork.Save();
                return Json(new { success = true, message = "Product has deleted successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult UpdateProduct(VM_Product aProduct)
        {
            tblProductInformation productInformation = unitOfWork.ProductRepository.GetByID(aProduct.ProductId);
            productInformation.ProductId = aProduct.ProductId;
            productInformation.ProductName = aProduct.ProductName;
            productInformation.ProductTypeId = aProduct.ProductTypeId;
            productInformation.StoreId = aProduct.StoreId;
            productInformation.Unit = aProduct.Unit;
            productInformation.UnitPrice = (decimal)aProduct.UnitPrice;
            productInformation.ProductionCost = aProduct.ProductionCost;
            productInformation.EditedBy = SessionManger.LoggedInUser(Session);
            productInformation.EditedDateTime = DateTime.Now;
            try
            {
                unitOfWork.ProductRepository.Update(productInformation);
                unitOfWork.Save();
                return Json(new { success = true, successMessage = "Successfully Edited" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {

                return Json(new { success = false, errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [SessionManger.CheckUserSession]
        [Authorize]
        public List<VM_Product> GetSellAbleProduct()
        {

            var sellableproducts = unitOfWork.ProductRepository.Get()
                .Where(aSellableProduyct => aSellableProduyct.ProductTypeId == sellableproductType)
                .Select(theProduct =>
                    new VM_Product()
                    {
                       ProductName = theProduct.ProductName,
                       UnitPrice = Convert.ToDecimal(theProduct.UnitPrice),
                       Unit = Convert.ToString(theProduct.Unit),
                       ProductId = Convert.ToInt32(theProduct.ProductId),
              
                       
                       
                    }).ToList();
            return sellableproducts;

        }
        public List<VM_Product> GetPurchaseProduct()
        {

            var purchasableProducts = unitOfWork.ProductRepository.Get()
                .Where(aSellableProduyct => aSellableProduyct.ProductTypeId == 1 || aSellableProduyct.ProductTypeId == 3)
                .Select(theProduct =>
                    new VM_Product()
                    {
                        ProductName = theProduct.ProductName,
                        UnitPrice = Convert.ToDecimal(theProduct.UnitPrice),
                        Unit = Convert.ToString(theProduct.Unit),
                        ProductId = Convert.ToInt32(theProduct.ProductId),
                        
                      
                        
                    }).ToList();
            return purchasableProducts;

        }

        public JsonResult GetSellAbleProductJsonResult()
        {
            try
            {
                var sellableproductList = GetSellAbleProduct();
                return Json(new { success = true, result = sellableproductList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            
        }
        public JsonResult GetPurchaseProductJsonResult()
        {
            try
            {
                var purchaseProductList = GetPurchaseProduct();
                return Json(new { success = true, result = purchaseProductList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            
        }

    }
}