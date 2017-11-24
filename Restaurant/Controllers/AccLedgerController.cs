using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DAL;
using DAL.Repository;
using Restaurant.Models.Enam;
using Restaurant.Models.ViewModel;
using Restaurant.Utility;

namespace Restaurant.Controllers
{
    public class AccLedgerController:Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        [SessionManger.CheckUserSession]
        [Authorize]
        public ActionResult AccLedger()
        {
            return View();
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public List<SelectListItem> GetBalanceTypes()
        {

            List<SelectListItem> balanceTypeList = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Cr",
                    Value = Convert.ToInt32(BalanceType.Credit).ToString(),
                    Selected = true
                },
                new SelectListItem()
                {
                    Text = "Dr",
                    Value = Convert.ToInt32(BalanceType.Debit).ToString(),
                    Selected = true
                    
                }
            };


            return balanceTypeList;
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetBalanceTypesJsonResult()
        {
            try
            {
                var balanceTypeList = GetBalanceTypes();
                return Json(new { success = true, result = balanceTypeList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {

                return Json(new { success = false, errorMessage = exception.Message });
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult AddLedger(VM_AddLedger vmLedger)
        {
            try
            {
                acc_Ledger ledger = new acc_Ledger();
                ledger.LedgerID = Guid.NewGuid();
                ledger.LedgerName = vmLedger.LedgerName;
                ledger.LedgerCode = vmLedger.LedgerCode;
                ledger.GroupID = vmLedger.GroupID;
                ledger.InitialBalance = vmLedger.InitialBalance;
                ledger.BalanceType = vmLedger.BalanceType;
                ledger.Comment = vmLedger.Comment;
                ledger.OCode = 1;
                ledger.RestaurantId = Convert.ToInt32(SessionManger.RestaurantOfLoggedInUser(Session));
                ledger.CreatedBy = SessionManger.LoggedInUser(Session);
                ledger.CreatedDateTime = DateTime.Now;
                unitOfWork.AccLedgerRepository.Insert(ledger);
                unitOfWork.Save();
                return Json(new {success = true, successMessage = "Succefully Added Ledger"},
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {

                return Json(new {success = false, errorMessage = exception.Message});
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetAllLedger()
        {
            try
            {
               var result =  unitOfWork.AccLedgerRepository.Get().Select(a => new
                {
                    a.LedgerName,a.LedgerID,a.BalanceType,a.Comment,a.InitialBalance,a.LedgerCode,a.GroupID,a.OCode
                });
                return Json(new {success = true, result = result},
                    JsonRequestBehavior.AllowGet); 
            }
            catch (Exception exception)
            {
               return Json(new {success = false, errorMessage = exception.Message});
            }

            }
        [SessionManger.CheckUserSession]
        [Authorize]
        public acc_Ledger GetLedgerById(String id)
        {
                 acc_Ledger ledger = new acc_Ledger();
            var result = unitOfWork.AccLedgerRepository.Get().Where(a => a.LedgerID == Guid.Parse(id)).FirstOrDefault();
            ledger.BalanceType = result.BalanceType;
            ledger.Comment = result.Comment;
            ledger.GroupID = result.GroupID;
            ledger.InitialBalance = result.InitialBalance;
            ledger.LedgerCode = result.LedgerCode;
           return result;
        }
        [HttpPost]
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetLedgerByIdJsonResult(string id)
        {
            try
            {
                var result = this.GetLedgerById(id);
                return Json(new { success = true, result = result },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { success = false, errorMessage = exception.Message });
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult UpdateLedger(acc_Ledger ledger)
        {
            try
            {
                var dbLedger = unitOfWork.AccLedgerRepository.Get().Where(a => a.LedgerID == ledger.LedgerID).FirstOrDefault();
                dbLedger.LedgerName = ledger.LedgerName;
                dbLedger.BalanceType = ledger.BalanceType;
                dbLedger.Comment = ledger.Comment;
                dbLedger.EditedBy = SessionManger.LoggedInUser(Session);
                dbLedger.EditedDateTime = DateTime.Now;
                dbLedger.GroupID = ledger.GroupID;
                dbLedger.InitialBalance = ledger.InitialBalance;
                unitOfWork.AccLedgerRepository.Update(dbLedger);
                unitOfWork.Save();
                return Json(new { success = true, successMessage = "SuccessFully updated ledger" });
            }
            catch (Exception exception)
            {

                return Json(new { success = false, errorMessage = exception.Message });
            }

        }
    }
       



    }
