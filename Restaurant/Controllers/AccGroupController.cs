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
    public class AccGroupController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: AccGroup
        [SessionManger.CheckUserSession]
        [Authorize]
        public ActionResult AccGroup()
        {
            return View();
        }
        
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetAccNatureInformation()
        {
            try
            {
                IEnumerable<acc_Nature> AccNatureList = unitOfWork.AccNatureRepository.Get().OrderBy(x => x.NatureName).ToList();
                return Json(new { success = true, result = AccNatureList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetAccGroupInformation()
        {
            try
            {
                var AccGroupList = unitOfWork.AccGroupRepository.Get().Select(a=>new 
                {
                    a.GroupID,a.GroupName,a.NatureID,a.GroupCode,a.ParentGroupID,a.OCode
                }).OrderBy(x => x.GroupName).ToList();
                return Json(new { success = true, result = AccGroupList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult SaveAccGroup(acc_Group aAccGroup)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Code
                    aAccGroup.OCode = 1;
                    aAccGroup.RestaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                    aAccGroup.CreatedBy = SessionManger.LoggedInUser(Session);
                    aAccGroup.CreatedDateTime = DateTime.Now;
                    aAccGroup.EditedBy = null;
                    aAccGroup.EditedDateTime = null;
                    unitOfWork.AccGroupRepository.Insert(aAccGroup);
                    unitOfWork.Save();
                    return Json(new { success = true, successMessage = "Account Group added successfully" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Model is not Valid!! Please, Fill up all the fields." }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult UpdateAccGroup(acc_Group aAccGroup)
        {
            
            try
            {
                acc_Group accGroup = unitOfWork.AccGroupRepository.GetByID(aAccGroup.GroupID);
                accGroup.GroupID = aAccGroup.GroupID;
                accGroup.GroupName = aAccGroup.GroupName;
                accGroup.GroupCode = aAccGroup.GroupCode;
                accGroup.NatureID = aAccGroup.NatureID;
                accGroup.ParentGroupID = aAccGroup.ParentGroupID;
                accGroup.EditedBy = SessionManger.LoggedInUser(Session);
                accGroup.EditedDateTime = DateTime.Now;
                unitOfWork.AccGroupRepository.Update(accGroup);
                unitOfWork.Save();
                return Json(new { success = true, successMessage = "Account Group Edited Successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {

                return Json(new { success = false, errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}