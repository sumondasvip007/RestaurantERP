using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using Restaurant.Utility;

namespace Restaurant.Controllers
{
    public class SPMappingController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        
        // GET: SupplierProductMapping
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult SPMapping()
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
                return Json(new { result = supplierList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { result = exception.Message });
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetPurchaseProductList()
        {

            try
            {
                var purchaseProductList = unitOfWork.SuppliersProductRepository.Get()
                    .Where(x => x.tblProductInformation.tblProductType.ProductTypeName == "Purchase")
                    .Select(s => new
                    {
                        s.ProductId,
                        s.tblProductInformation.ProductName
                    })
                    .ToList();
                return Json(new { result = purchaseProductList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { result = exception.Message });
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult AddSupplierProductList(tblSuppliersProduct supplierProduct)
        {
            try
            {
                unitOfWork.SuppliersProductRepository.Insert(supplierProduct);
                unitOfWork.Save();
                return Json(new { success = true, successMessage = " Successfully Added" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult DeleteSupplierProductList(int? id)
        {
            try
            {
                unitOfWork.SuppliersProductRepository.Delete(id);
                unitOfWork.Save();
                return Json(new { success = true, successMessage = " Successfully Deleted" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { success = false, errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);

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