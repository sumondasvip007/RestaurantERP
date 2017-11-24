using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DAL;
using DAL.Repository;
using Restaurant.Utility;

namespace Restaurant.Controllers
{
    public class SupplierInformationController : Controller
    {
        private UnitOfWork unitOfWork =  new UnitOfWork();
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult ShowAllSupplier()
        {
            return View();
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult AddSupplier(tblSupplierInformation supplierInformation)
        {

            try
            {
                supplierInformation.CreatedBy = SessionManger.LoggedInUser(Session);
                //supplierInformation.DateTime = supplierInformation.DateTime = DateTime.Now;
                supplierInformation.RestaurantId = supplierInformation.RestaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());

                supplierInformation.CreatedDateTime = supplierInformation.CreatedDateTime = DateTime.Now;
                supplierInformation.EditedDateTime = supplierInformation.EditedDateTime = null;
                unitOfWork.SuppliersInformationRepository.Insert(supplierInformation);
                unitOfWork.Save();
                return Json(new { success = true, successMessage = "Successfully Submited" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }



        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllSuppier()
        {
            try
            {
                List<tblSupplierInformation> supplierInformations= new List<tblSupplierInformation>();
                var getAllSuppier = unitOfWork.SuppliersInformationRepository.Get();

                foreach (var supplier in getAllSuppier)
                {
                    tblSupplierInformation supplierInformation = new tblSupplierInformation();
                    supplierInformation.SupplierAddress = supplier.SupplierAddress;
                    supplierInformation.SupplierEmail = supplier.SupplierEmail;
                    supplierInformation.SupplierId = supplier.SupplierId;
                    supplierInformation.SupplierName = supplier.SupplierName;
                    supplierInformation.SupplierPhoneNo = supplier.SupplierPhoneNo;
                    supplierInformations.Add(supplierInformation);
                }

                return Json(new { result = supplierInformations, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }
            

        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult DeleteSuppierById(int ?id)
        {

            try
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                var supplier = unitOfWork.SuppliersInformationRepository.GetByID(id);
                unitOfWork.SuppliersInformationRepository.Delete(supplier);
                unitOfWork.Save();
                return Json(new { success = true, successMessage = "Successfully Delete Supplier Information " });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            
        }


        [Authorize]
        [SessionManger.CheckUserSession]
        //get Suppier By Id 
        public JsonResult EditSuppier(int ?id)
        {
           

            try
            {
                UnitOfWork unitOfWork = new UnitOfWork();

                var suppierInformation =
                    unitOfWork.SuppliersInformationRepository.Get()
                        .Where(x => x.SupplierId == id)
                        .Select(a => new tblSupplierInformation()
                        {
                            SupplierId = a.SupplierId,
                            SupplierAddress = a.SupplierAddress,
                            SupplierEmail = a.SupplierEmail,
                            SupplierName = a.SupplierName,
                            SupplierPhoneNo = a.SupplierPhoneNo
                            


                        }).FirstOrDefault();

                return Json(new { result = suppierInformation, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }
            

        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult UpdateSupplierInformation(tblSupplierInformation suplierInformation)
        {

            try
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                unitOfWork.SuppliersInformationRepository.Update(suplierInformation);
                unitOfWork.Save();
                return Json(new { success = true, successMessage = "Successfully Updated Supplier Information " });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
    }
}