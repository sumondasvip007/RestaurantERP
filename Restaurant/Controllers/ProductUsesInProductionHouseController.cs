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
    public class ProductUsesInProductionHouseController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        [SessionManger.CheckUserSession]
        [Authorize]
        public ActionResult ProductUsesInProduintionHouse()
        {
            return View();
        }

        [SessionManger.CheckUserSession]
        [Authorize]
        public ActionResult ProudtionHouseDropDown()
        {
            return View();
        }

        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetAvailableProductInProductionHouseJsonResult(int storeId, int productId)
        {
            try
            {
                var availableProduct = GetAvailableProductInProductionHouse(storeId, productId);

                return Json(new {success = true, result = availableProduct}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new {success = false, errorMessage = ex.Message}, JsonRequestBehavior.AllowGet);
            }

        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult ProductUseInProductionHouse(List<VM_Product> productList)
        {
            try
            {

                foreach (var product in productList)
                {
                    tblProductTransfer productTransfer = new tblProductTransfer();
                    productTransfer.DateTime = DateTime.Now;
                    productTransfer.CreatedBy = SessionManger.LoggedInUser(Session);
                    productTransfer.EditedBy = null;
                    productTransfer.RestaurantId =
                        Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                    productTransfer.CreatedDateTime = DateTime.Now;
                    productTransfer.StoreId = product.StoreId;
                    productTransfer.SupplierId = null;
                    productTransfer.ProductId = product.ProductId;
                    productTransfer.Quantity = product.Quantity;
                    productTransfer.TransferDate = DateTime.Now;
                    productTransfer.isOut = true;
                    productTransfer.Unit = product.Unit;
                    unitOfWork.ProductTransferRepository.Insert(productTransfer);
                }
                unitOfWork.Save();
                return Json(new {success = true, successMesseage = "Raw Metarial Taken From ProductionHouse Successfully"},
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new {success = false, errorMessage = ex.Message}, JsonRequestBehavior.AllowGet);
            }

        }
        [Authorize]
        [SessionManger.CheckUserSession]
        private decimal GetAvailableProductInProductionHouse(int storeId, int productId)
        {
            //decimal availableProduct = 0;
            //var productInProductionHouse =
            //    unitOfWork.ProductTransferRepository.Get()
            //        .Where(productTranfer => productTranfer.ProductId == productId && productTranfer.StoreId == storeId)
            //        .ToList();

            //var productIn = productInProductionHouse.Where(d => d.isIn == true).Sum(a => a.Quantity);
            //var productOut = productInProductionHouse.Where(d => d.isOut == true).Sum(a => a.Quantity);
            //availableProduct = Convert.ToDecimal(productIn - productOut);

            decimal availableProduct = unitOfWork.CustomRepository.sp_AvailableQuantityForProductUsesInProductionHouse(storeId, productId).Select(a => a.Quantity).FirstOrDefault();

            return availableProduct;
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetPurchaseProductJsonResult(int storeId)
        {
            try
            {
                //ProductController productController = new ProductController();
                //var purchaseProductList = productController.GetPurchaseProduct().Select(theProduct => new VM_Product()
                //{
                //    ProductName = theProduct.ProductName,
                //    UnitPrice = Convert.ToDecimal(theProduct.UnitPrice),
                //    Unit = Convert.ToString(theProduct.Unit),
                //    ProductId = Convert.ToInt32(theProduct.ProductId),
                //    AvilableQuatity = GetAvailableProductInProductionHouse(storeId,theProduct.ProductId)
                //});

                
                // var purchaseProduct = unitOfWork.ProductRepository.Get()
                // .Where(a => a.ProductTypeId == 1 || a.ProductTypeId == 3).Select(theProduct => new VM_Product()
                //{
                //    ProductName = theProduct.ProductName,
                //    UnitPrice = Convert.ToDecimal(theProduct.UnitPrice),
                //    Unit = Convert.ToString(theProduct.Unit),
                //    ProductId = Convert.ToInt32(theProduct.ProductId),
                //    AvilableQuatity = GetAvailableProductInProductionHouse(storeId,theProduct.ProductId)
                //});


                //var purchaseProduct = unitOfWork.ProductRepository.Get()
                //    .Where(a => a.ProductTypeId == 1 || a.ProductTypeId == 3).ToList();

                var purchaseProductList = unitOfWork.CustomRepository.sp_PurchaseAndBothTypeProductListForProductUsesInProductionHouse(storeId).ToList();

         

                // List<VM_Product> purchaseProductList = new List<VM_Product>();

                // foreach (var product in purchaseProduct)
                //{
                //    VM_Product aProduct=new VM_Product();

                //    aProduct.ProductName = product.ProductName;
                //    aProduct.UnitPrice = Convert.ToDecimal(product.UnitPrice);
                //    aProduct.Unit = product.Unit;
                //    aProduct.ProductId = product.ProductId;
                //    aProduct.AvilableQuatity = GetAvailableProductInProductionHouse(storeId, product.ProductId);

                //    if (aProduct.AvilableQuatity>0)
                //    {
                //        purchaseProductList.Add(aProduct);
                //    }

                //}


                
                return Json(new { success = true, result = purchaseProductList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }



    }
}