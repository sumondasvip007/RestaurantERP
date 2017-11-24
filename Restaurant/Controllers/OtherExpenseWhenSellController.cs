using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using DAL.ViewModel;
using Restaurant.Models.ViewModel;
using Restaurant.Utility;

namespace Restaurant.Controllers
{
    public class OtherExpenseWhenSellController : Controller
    {
        UnitOfWork unitOfWork=new UnitOfWork();
        // GET: OtherExpenseWhenSell
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult AddOtherExpenseWhenSell()
        {
            return View();
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllSellsPoint()
        {
            try
            {
              
                var sellsPointList = (from a in unitOfWork.StoreRepository.Get().Where(a => a.IsSellsPointStore == true)
                                      select new tblStoreInformation()
                                      {
                                          store_id = a.store_id,
                                          store_name = a.store_name
                                      }).ToList();



                return Json(new { success = true, result = sellsPointList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllGroup()
        {
            try
            {

                var groupList = (from a in unitOfWork.GroupForShiftRepository.Get()
                                 select new VM_GroupForShift()
                                 {

                                     GroupId = a.GroupId,
                                     GroupName = a.GroupName

                                 }).ToList();
                return Json(new { success = true, result = groupList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllShift()
        {
            try
            {

                var shiftList = (from a in unitOfWork.ShiftRepository.Get()
                                 select new VM_Shift()
                                 {

                                     ShiftId = a.ShiftId,
                                     ShiftName = a.ShiftName

                                 }).ToList();
                return Json(new { success = true, result = shiftList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]
        //public JsonResult AddOtherExpenseWhenSell(VM_OtherExpense otherExpense, DateTime fromDate)
        public JsonResult AddOtherExpenseWhenSell(VM_OtherExpense otherExpense)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tblOtherExpense aOtherExpense = new tblOtherExpense();
                    aOtherExpense.StoreId = otherExpense.StoreId;
                    //aOtherExpense.GroupId = otherExpense.GroupId;
                    aOtherExpense.ShiftId = otherExpense.ShiftId;
                    //aOtherExpense.TransferDate = fromDate;
                    aOtherExpense.TransferDate = DateTime.Now;
                    aOtherExpense.Less = otherExpense.Less;
                    aOtherExpense.Due = otherExpense.Due;
                    aOtherExpense.Compliment = otherExpense.Compliment;
                    aOtherExpense.Damage = otherExpense.Damage;
                    aOtherExpense.RestaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                    aOtherExpense.CreatedBy = SessionManger.LoggedInUser(Session);
                    aOtherExpense.CreatedDateTime = DateTime.Now;
                    aOtherExpense.EditedBy = null;
                    aOtherExpense.EditedDateTime = null;
                    unitOfWork.OtherExpenseRepository.Insert(aOtherExpense);
                    unitOfWork.Save();

                    return Json(new { success = true, successMessage = "Other Expense added successfully" },JsonRequestBehavior.AllowGet);
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
        
    }
}