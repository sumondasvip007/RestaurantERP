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
    public class SupplierProductMappingController : Controller
    {
        // GET: SupplierProductMapping
        UnitOfWork unitOfWork = new UnitOfWork();

        // GET: SupplierProductMapping
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult Mapping()
        {
            return View();
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult GetSupplierList()
        {
            try
            {
                var supplierList = unitOfWork.SuppliersInformationRepository.Get()
                    .Select(s => new
                    {
                        s.SupplierId,
                        s.SupplierName
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
        public JsonResult GetPurchaseProductList(int id)
        {

            try
            {
                var purchaseProductList = unitOfWork.ProductRepository.Get()
                    .Where(x => x.ProductTypeId == Convert.ToInt32(ProductType.PurchaseTypeProduct) || x.ProductTypeId == Convert.ToInt32(ProductType.PurchaseAndSellTypeProduct))         //Purchase == 1 && sell == 2 
                    .Select(s => new VM_ProductList
                    {
                        ProductId = s.ProductId,
                        ProductName = s.ProductName,
                        IsSelected = s.tblSuppliersProducts.Any(a => a.SupplierId == id && a.ProductId ==s.ProductId),
                        SupplierId = id
                    })
                    .ToList();
                return Json(new { success = true, result = purchaseProductList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { success = false, result = exception.Message });
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult AddSupplierProductList(List<VM_ProductList> product)
        {
            try
            {
                var supplierId = product.Select(s => s.SupplierId).FirstOrDefault();
                DeleteSupplierProductList(supplierId);
                foreach (VM_ProductList vmProductList in product)
                {
                    if (vmProductList.IsSelected)
                    {
                        tblSuppliersProduct tblSuppliersProduct = new tblSuppliersProduct();
                        tblSuppliersProduct.ProductId = vmProductList.ProductId;
                        tblSuppliersProduct.SupplierId = vmProductList.SupplierId;
                        unitOfWork.SuppliersProductRepository.Insert(tblSuppliersProduct);
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
                List<tblSuppliersProduct> list =
                    unitOfWork.SuppliersProductRepository.Get().Where(s => s.SupplierId == id).ToList();
                if (list.Count > 0)
                {
                    foreach (var product in list)
                    {
                        unitOfWork.SuppliersProductRepository.Delete(product);
                    }
                    unitOfWork.Save();
                }

            }
            catch (Exception exception)
            {
               
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