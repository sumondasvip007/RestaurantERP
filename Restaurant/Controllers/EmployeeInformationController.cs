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
    public class EmployeeInformationController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: EmployeeInformation

        [SessionManger.CheckUserSession]
        [Authorize]
        //[ValidateAntiForgeryToken]
        
        public ActionResult SaveEmployeeInformation()
        {
            return View();
        }
        [HttpPost]
        [SessionManger.CheckUserSession]
        [Authorize] 
        public JsonResult SaveEmployeeInformation(HttpPostedFileBase file, VM_EmployeeInformation aEmployee)
        {

            //string emptyEmail = "undefined";
            var EmailExist = unitOfWork.EmployeeInformationRepository.Get()
              .Where(a => a.EmployeeEmail == aEmployee.EmployeeEmail && a.EmployeeEmail != "undefined" && a.EmployeeEmail != "").ToList();


            if (ModelState.IsValid)
            {
                try
                {
                    if (!EmailExist.Any())
                    {
                        string path = "";
                        if (file != null)
                        {
                            string pic = System.IO.Path.GetFileName(file.FileName);
                            path = System.IO.Path.Combine(
                                Server.MapPath("~/Image"), pic);
                            // file is uploaded
                            file.SaveAs(path);

                            tblEmployeeInformation employee = new tblEmployeeInformation();
                            employee.EmployeeName = aEmployee.EmployeeName;
                            employee.EmployeeAddress = aEmployee.EmployeeAddress;
                            employee.ContactNumber = aEmployee.ContactNumber;
                            employee.EmployeeNid = aEmployee.EmployeeNid;
                            employee.EmployeeEmail = aEmployee.EmployeeEmail;
                            employee.EmployeeImage = "/Image/" + pic;
                            employee.DesignationId = aEmployee.DesignationId;
                            employee.RestaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                            employee.CreatedBy = SessionManger.LoggedInUser(Session);
                            employee.CreatedDateTime = DateTime.Now;
                            employee.EditedBy = null;
                            employee.EditedDateTime = null;
                            unitOfWork.EmployeeInformationRepository.Insert(employee);
                            unitOfWork.Save();
                        }
                        else
                        {
                            tblEmployeeInformation employee = new tblEmployeeInformation();
                            employee.EmployeeName = aEmployee.EmployeeName;
                            employee.EmployeeAddress = aEmployee.EmployeeAddress;
                            employee.ContactNumber = aEmployee.ContactNumber;
                            employee.EmployeeNid = aEmployee.EmployeeNid;
                            employee.EmployeeEmail = aEmployee.EmployeeEmail;
                            employee.EmployeeImage =employee.EmployeeImage;
                            employee.DesignationId = aEmployee.DesignationId;
                            employee.RestaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                            employee.CreatedBy = SessionManger.LoggedInUser(Session);
                            employee.CreatedDateTime = DateTime.Now;
                            employee.EditedBy = null;
                            employee.EditedDateTime = null;
                            unitOfWork.EmployeeInformationRepository.Insert(employee);
                            unitOfWork.Save();
                        }
                        return Json(new { success = true, successMessage = "Employee Information Added Successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, errorMessage = "The email already exist" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Please Fill Up all required field." }, JsonRequestBehavior.AllowGet);
            }
        }


        [SessionManger.CheckUserSession]
        [Authorize]   
        public JsonResult GetAllDesignation()
        {
            try
            {

                var designationList = (from a in unitOfWork.DesignationRepository.Get()
                                       select new VM_Designation()
                                   {

                                       DesignationId = a.DesignationId,
                                       DesignationName = a.DesignationName

                                   }).ToList();
                return Json(new { success = true, result = designationList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [SessionManger.CheckUserSession]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult EmployeeInformationList()
        {
            return View();
        }
        [SessionManger.CheckUserSession]
        [Authorize] 
        public JsonResult GetAllEmployeeInformation()
        {
            try
            {

                var employeeInformationList = (from a in unitOfWork.EmployeeInformationRepository.Get()
                                               select new VM_EmployeeInformation()
                                               {
                                                   EmployeeId = a.EmployeeId,
                                                   EmployeeName = a.EmployeeName,
                                                   EmployeeAddress = a.EmployeeAddress,
                                                   ContactNumber = a.ContactNumber,
                                                   EmployeeNid = a.EmployeeNid,
                                                   EmployeeEmail = a.EmployeeEmail,
                                                   EmployeeImage = a.EmployeeImage,
                                                   DesignationId = a.DesignationId,
                                                   DesignationName = a.tblDesignation.DesignationName

                                               }).ToList();
                return Json(new { success = true, result = employeeInformationList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult DeleteEmployeeInformation(int? id)
        {
            try
            {
                tblEmployeeInformation aEmployee = unitOfWork.EmployeeInformationRepository.GetByID(id);
                if (aEmployee == null)
                {
                    return Json(new { success = false, errorMessage = "Employee Information Delete Failed" }, JsonRequestBehavior.AllowGet);
                }
                unitOfWork.EmployeeInformationRepository.Delete(aEmployee);
                unitOfWork.Save();
                return Json(new { success = true, message = "Employee Information Deleted successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult UpdateEmployeeInformation(HttpPostedFileBase file,VM_EmployeeInformation aEmployee)
        {
            tblEmployeeInformation employeeInformation = unitOfWork.EmployeeInformationRepository.GetByID(aEmployee.EmployeeId);
           

            string path = "";
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                path = System.IO.Path.Combine(
                    Server.MapPath("~/Image"), pic);
                // file is uploaded
                file.SaveAs(path);

                employeeInformation.EmployeeId = aEmployee.EmployeeId;
                employeeInformation.EmployeeName = aEmployee.EmployeeName;
                employeeInformation.EmployeeAddress = aEmployee.EmployeeAddress;
                employeeInformation.ContactNumber = aEmployee.ContactNumber;
                employeeInformation.EmployeeNid = aEmployee.EmployeeNid;
                employeeInformation.EmployeeEmail = aEmployee.EmployeeEmail;
                employeeInformation.EmployeeImage = "/Image/" + pic;
                employeeInformation.DesignationId = aEmployee.DesignationId;
                employeeInformation.EditedBy = SessionManger.LoggedInUser(Session);
                employeeInformation.EditedDateTime = DateTime.Now;

            }

            else
            {
                employeeInformation.EmployeeId = aEmployee.EmployeeId;
                employeeInformation.EmployeeName = aEmployee.EmployeeName;
                employeeInformation.EmployeeAddress = aEmployee.EmployeeAddress;
                employeeInformation.ContactNumber = aEmployee.ContactNumber;
                employeeInformation.EmployeeNid = aEmployee.EmployeeNid;
                employeeInformation.EmployeeEmail = aEmployee.EmployeeEmail;
                employeeInformation.EmployeeImage = employeeInformation.EmployeeImage;
                employeeInformation.DesignationId = aEmployee.DesignationId;
                employeeInformation.EditedBy = SessionManger.LoggedInUser(Session);
                employeeInformation.EditedDateTime = DateTime.Now;
            }
            var EmailExist = unitOfWork.EmployeeInformationRepository.Get()
             .Where(a => a.EmployeeEmail == aEmployee.EmployeeEmail && a.EmployeeId != aEmployee.EmployeeId && a.EmployeeEmail != "undefined" && !string.IsNullOrEmpty(a.EmployeeEmail)).ToList();

            try
            {
                if (!EmailExist.Any())
                {
                    unitOfWork.EmployeeInformationRepository.Update(employeeInformation);
                    unitOfWork.Save();
                    return Json(new { success = true, successMessage = "Successfully Edited" },
                        JsonRequestBehavior.AllowGet);
                }
                else
                {

                    return Json(new { success = false, errorMessage = "This Email already exist." }, JsonRequestBehavior.AllowGet);

                }                
            }
            catch (Exception exception)
            {

                return Json(new { success = false, errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }

            
        }



    }
}