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
    public class ProductEntryToProductionHouseController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: ProductEntryToProductionHouse
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult ProductToHouse()
        {
            return View();
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetProductList(int id)
        {
            try
            {
                
                var productList = unitOfWork.ProductionHouseToProductMappingRepository.Get()
                    .Where(x => x.ProductionHouseId == id) //Purchase == 1 && sell == 2
                    .Select(s => new VM_ProductList
                    {
                        ProductId = s.ProductId,
                        ProductName = s.tblProductInformation.ProductName
                    })
                    .ToList();
                return Json(new {success = true, result = productList}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new {success = false, result = exception.Message});
            }
        }
        
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetProductionHouseProductList(int id)
        {
            try
            {
                var ProductionHouseProductList = unitOfWork.ProductionHouseToProductMappingRepository.Get()
                    .Where(x => x.ProductionHouseId == id) //Purchase == 1 && sell == 2
                    .Select(s => new VM_ProductToStore
                    {
                        ProductId = s.ProductId,
                        ProductName = s.tblProductInformation.ProductName,
                        Unit = s.tblProductInformation.Unit,
                        UnitPrice = (double)s.tblProductInformation.UnitPrice
                    })
                    .ToList();
                return Json(new { success = true, result = ProductionHouseProductList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        
            [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult SaveProductToProductionHouse(int StoreId, List<VM_ProductToStore> productList)
        {
                try
                {
                    foreach (VM_ProductToStore aProduct in productList)
                    {
                        tblProductTransfer aProductTransfer = new tblProductTransfer();
                        aProductTransfer.StoreId = StoreId;
                        aProductTransfer.TransferDate = DateTime.Now;
                        aProductTransfer.SupplierId = null;
                        aProductTransfer.ProductId = aProduct.ProductId;
                        aProductTransfer.Quantity = aProduct.Quantity;
                        aProductTransfer.Unit = aProduct.Unit;
                        aProductTransfer.UnitPrice = (decimal?)aProduct.UnitPrice;
                        aProductTransfer.isIn = true;
                        aProductTransfer.isOut = false;
                        aProductTransfer.DateTime = DateTime.Now;
                        aProductTransfer.RestaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                        aProductTransfer.CreatedBy = SessionManger.LoggedInUser(Session);
                        aProductTransfer.CreatedDateTime = DateTime.Now;
                        aProductTransfer.EditedBy = null;
                        aProductTransfer.EditedDateTime = null;
                        unitOfWork.ProductTransferRepository.Insert(aProductTransfer);
                        // MAY BE, DATA INSERTION IN TABLE tblProductEntryToProductionHouse SHOULD BE IMPLEMENTED HERE. 
                    }
                    unitOfWork.Save();
                    return Json(new { success = true, successMessage = "Product Added Successfully" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
                }
        }
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

        //public JsonResult Save(tblProductEntryToProductionHouse product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            product.CreatedDate = DateTime.Now;
        //            unitOfWork.ProductEntryToProductionHouseRepository.Insert(product);
        //            unitOfWork.Save();

        //            return Json(new {success = true, successMessage = "Action Saved Successfully"},
        //                JsonRequestBehavior.AllowGet);
        //        }
        //        catch (Exception e)
        //        {
        //            return Json(new {success = false, errorMessage = e.Message}, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    else
        //    {
        //        return Json(new {success = false, errorMessage = "Model is not Valid"}, JsonRequestBehavior.AllowGet);
        //    }
        //}
    }
}