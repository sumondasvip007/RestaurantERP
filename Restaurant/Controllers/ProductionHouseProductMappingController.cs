using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using Restaurant.Models.Enam;
using Restaurant.Models.ViewModel;
using Restaurant.Utility;

namespace Restaurant.Controllers
{
    public class ProductionHouseProductMappingController : Controller
    {
        // GET: SupplierProductMapping
        UnitOfWork unitOfWork = new UnitOfWork();
      

        // GET: SupplierProductMapping
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult PhpMapping()
        {
            return View();
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult GetProductionHouse()
        {
            try
            {
                //var supplierList = unitOfWork.ProductionHouseInformationRepository.Get()
                //    .Select(s => new
                //    {
                //        id = s.ProductionHouseId,
                //        name = s.ProductionHouseName
                //    })
                //    .ToList();
                var supplierList = unitOfWork.StoreRepository.Get().Where(a=>a.isProductionHouseStore==true)
                   .Select(s => new
                   {
                       id = s.store_id,
                       name = s.store_name
                   })
                   .ToList();
                return Json(new { success = true, result = supplierList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { success = false, result = exception.Message });
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetProductList(int id)
        {
            try
            {
                var selltypeProductList = unitOfWork.ProductRepository.Get()
                    .Where(x => x.ProductTypeId == (int)ProductType.SellTypeProduct || x.ProductTypeId == (int)ProductType.PurchaseAndSellTypeProduct)         //Purchase == 1 && sell == 2
                    .Select(s => new VM_ProductList
                    {
                        ProductId = s.ProductId,
                        ProductName = s.ProductName,
                        IsSelected = s.tblProductionHouseToProductMappings.Any(a => a.ProductionHouseId == id && a.ProductId == s.ProductId),
                        ProductionHouseId = id
                    })
                    .ToList();
                return Json(new { success = true, result = selltypeProductList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { success = false, result = exception.Message });
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult MapPtoductToProductionHouse(List<VM_ProductList> product)
        {
            try
            {
                //var user = (tblRestaurantUser)SessionManger.LoggedInUser(Session);
                var productionHouseId = product.Select(s => s.ProductionHouseId).FirstOrDefault();
                DeleteSupplierProductList(productionHouseId);
                foreach (VM_ProductList vmProductList in product)
                {
                    if (vmProductList.IsSelected)
                    {
                        tblProductionHouseToProductMapping mapping = new tblProductionHouseToProductMapping();
                        mapping.ProductId = vmProductList.ProductId;
                        mapping.ProductionHouseId = vmProductList.ProductionHouseId;
                        mapping.CreatedDate = DateTime.Now;
                        //mapping.CreatedBy = user.UserId;
                        //mapping.RestuarentId = user.restaurant_id.ToString();
                        unitOfWork.ProductionHouseToProductMappingRepository.Insert(mapping);
                    }

                }
                unitOfWork.Save();
                return Json(new { success = true, successMessage = " Successfully Added" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { success = false, errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public void DeleteSupplierProductList(int id)
        {
            try
            {
                List<tblProductionHouseToProductMapping> list =
                    unitOfWork.ProductionHouseToProductMappingRepository.Get().Where(s => s.ProductionHouseId == id).ToList();
                if (list.Count > 0)
                {
                    foreach (var product in list)
                    {
                        unitOfWork.ProductionHouseToProductMappingRepository.Delete(product);
                    }
                }

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult EditSupplierProductList(tblSuppliersProduct supplierProduct)
        {
            try
            {
                unitOfWork.SuppliersProductRepository.Update(supplierProduct);
                unitOfWork.Save();
                return Json(new { success = true, successMessage = "Successfully Edited" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { success = false, errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
