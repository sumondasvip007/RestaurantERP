using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using DAL;
using DAL.Repository;
using DAL.ViewModel;
using Restaurant.Models.ViewModel;

namespace Restaurant.Controllers
{
    public class ReciveVoucherController:Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
       

        public ActionResult ReciveVoucher()
        {
            return View();
        }

        public JsonResult GetVoucherId()
        {
            try
            {
                var voucherNumber = unitOfWork.CustomRepository.GetNewVoucherNumber(VoucherPrefix.ReceiptVoucher);

                return Json(new { success = true, result = voucherNumber }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {

                return Json(new { success = false, errorMessage = exception.Message });
            }
        }

        public JsonResult SaveVoucher(VM_AccVoucher vmAccVoucher)
        {
            try
            {
               AccVoucherController accVoucherController = new AccVoucherController();


                return Json(new { success = false, successMessage = ""});
            }
            catch (Exception exception)
            {
                return Json(new { success = false, errorMessage = exception.Message });
            }
        }
        public JsonResult UpdateReciveVoucher(acc_VoucherEntry voucherEntry, IEnumerable<acc_VoucherDetail> VoucherDetails)
        {

            try
            {





                
              

                return Json(new { success = true, successMessage = "Recive Voucher Edited Successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {

                return Json(new { success = false, errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public void UpdateVoucher(acc_VoucherEntry voucherEntry, IEnumerable<acc_VoucherDetail> VoucherDetails)
        {




            
        }



    }
}