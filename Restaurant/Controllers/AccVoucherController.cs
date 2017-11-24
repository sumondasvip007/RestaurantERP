using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using DAL.ViewModel;
using Microsoft.Reporting.WebForms;
using Restaurant.Models.Enam;
using Restaurant.Models.ViewModel;
using Restaurant.Utility;
using VM_Product = Restaurant.Models.ViewModel.VM_Product;

namespace Restaurant.Controllers
{
    public class AccVoucherController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        [SessionManger.CheckUserSession]
        [Authorize]
        public ActionResult AccPaymentVoucher()
        {
            return View();
        }
        
        [SessionManger.CheckUserSession]
        [Authorize]
        public ActionResult AccContraVoucher()
        {
            return View();
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public ActionResult AccJournalVoucher()
        {
            return View();
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetPaymentVoucherNumber()
        {
            try
            {
                var VNumber = unitOfWork.CustomRepository.GetNewVoucherNumber(VoucherPrefix.PaymentVoucher);
                return Json(new { success = true, result = VNumber }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetContraVoucherNumber()
        {
            try
            {
                var VNumber = unitOfWork.CustomRepository.GetNewVoucherNumber(VoucherPrefix.ContraVoucher);
                return Json(new { success = true, result = VNumber }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetJournalVoucherNumber()
        {
            try
            {
                var VNumber = unitOfWork.CustomRepository.GetNewVoucherNumber(VoucherPrefix.JournalVoucher);
                return Json(new { success = true, result = VNumber }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetPaymentLadgerList()
        {
            try
            {
                var LadgerList = unitOfWork.AccLedgerRepository.Get().Select(a => new
                {
                    a.LedgerID,
                    a.LedgerName
                }).ToList();
                return Json(new { success = true, result = LadgerList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetContraLadgerList()
        {
            try
            {
                var LadgerList = unitOfWork.AccLedgerRepository.Get().Select(a => new
                {
                    a.LedgerID,
                    a.LedgerName
                }).ToList();
                return Json(new { success = true, result = LadgerList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetJournalLadgerList()
        {
            try
            {
                var LadgerList = unitOfWork.AccLedgerRepository.Get().Select(a => new
                {
                    a.LedgerID,
                    a.LedgerName
                }).ToList();
                return Json(new { success = true, result = LadgerList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult SavePaymentVoucher(string VNumber, string Narration, IEnumerable<VM_AccVoucher> VoucherDetails )
        {
            try
            {
                var VoucherName = VoucherNameEnum.PaymentVoucher.ToString();
                var VTypeID = (int?)VoucherTypeEnum.PaymentVoucher;
                SaveVoucher(VNumber, Narration, VoucherName, VTypeID, VoucherDetails);
               
                return Json(new { success = true, successMessage = "Payment Voucher Saved Successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveReciveVoucher(string VNumber, string Narration, IEnumerable<VM_AccVoucher> VoucherDetails)
        {
            try
            {
                var VoucherName = VoucherNameEnum.ReceiptVoucher.ToString();
                var VTypeID = (int?)VoucherTypeEnum.ReceiptVoucher;
                SaveVoucher(VNumber, Narration, VoucherName, VTypeID, VoucherDetails);

                return Json(new { success = true, successMessage = "Payment Voucher Saved Successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult SaveContraVoucher(string VNumber, string Narration, IEnumerable<VM_AccVoucher> VoucherDetails)
        {
            try
            {
                var VoucherName = VoucherNameEnum.ContraVoucher.ToString();
                var VTypeID = (int?)VoucherTypeEnum.ContraVoucher;
                SaveVoucher(VNumber, Narration, VoucherName, VTypeID, VoucherDetails);

                return Json(new { success = true, successMessage = "Contra Voucher Saved Successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult SaveJournalVoucher(string VNumber, string Narration, IEnumerable<VM_AccVoucher> VoucherDetails)
        {
            try
            {
                var VoucherName = VoucherNameEnum.JournalVoucher.ToString();
                var VTypeID = (int?)VoucherTypeEnum.JournalVoucher;
                SaveVoucher(VNumber, Narration, VoucherName, VTypeID, VoucherDetails);

                return Json(new { success = true, successMessage = "Journal Voucher Saved Successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetAllPaymentVoucherList()
        {
            try
            {
                var PaymentVoucherEntryList = unitOfWork.AccVoucherEntryRepository.Get().Where(a=>a.VNumber.Contains("PV")).Select(a => new VM_AccVoucher()
                {
                    
                    VoucherID = a.VoucherID,
                    VoucherName = a.VoucherName,
                    Narration = a.Narration,
                    VTypeID = (int) a.VTypeID,
                    VNumber = a.VNumber,
                    TransactionDate = String.Format("{0:D}", a.TransactionDate) 
                }).ToList();
                return Json(new { success = true, result = PaymentVoucherEntryList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetAllContraVoucherList()
        {
            try
            {
                var ContraVoucherEntryList = unitOfWork.AccVoucherEntryRepository.Get().Where(a => a.VNumber.Contains("CV")).Select(a => new VM_AccVoucher()
                {

                    VoucherID = a.VoucherID,
                    VoucherName = a.VoucherName,
                    Narration = a.Narration,
                    VTypeID = (int)a.VTypeID,
                    VNumber = a.VNumber,
                    TransactionDate = String.Format("{0:D}", a.TransactionDate)
                }).ToList();
                return Json(new { success = true, result = ContraVoucherEntryList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetAllJournalVoucherList()
        {
            try
            {
                var JournalVoucherEntryList = unitOfWork.AccVoucherEntryRepository.Get().Where(a => a.VNumber.Contains("JV")).Select(a => new VM_AccVoucher()
                {

                    VoucherID = a.VoucherID,
                    VoucherName = a.VoucherName,
                    Narration = a.Narration,
                    VTypeID = (int)a.VTypeID,
                    VNumber = a.VNumber,
                    TransactionDate = String.Format("{0:D}", a.TransactionDate)
                }).ToList();
                return Json(new { success = true, result = JournalVoucherEntryList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetVoucherDetailsByVoucherID(int? VoucherID)
        {
            try
            {
                var VoucherDetailsList = unitOfWork.AccVoucherDetailsRepository.Get().Where(a => a.VoucherID == VoucherID).Select(s => new VM_AccVoucher()
                {
                    VoucherDetailID = s.VoucherDetailID.ToString(),
                    VoucherID = s.VoucherID,
                    LedgerID = s.LedgerID.ToString(),
                    Debit = (double)s.Debit,
                    Credit = (double)s.Credit,
                    ChequeNumber = s.ChequeNumber,
                    TransactionDate = s.TransactionDate.ToString(),
                    VTypeID = (int)s.VTypeID,
                    OCode = (int) s.OCode,
                    RestaurantId = (int) s.RestaurantId,
                    CreatedBy = s.CreatedBy,
                    CreatedDateTime = s.CreatedDateTime.ToString(),
                    EditedBy = s.EditedBy,
                    EditedDateTime = s.EditedDateTime.ToString()
                }).ToList();

                return Json(new { success = true, result = VoucherDetailsList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            
        }

        public JsonResult GetAllReciveVoucher()
        {
            try
            {
                var ReciveVoucherEntryList =
                    unitOfWork.AccVoucherEntryRepository.Get().Where(a => a.VNumber.Contains("RV")).Select(a => new
                    {
                        a.VoucherID,
                        a.VoucherName,
                        a.Narration,
                        a.VTypeID,
                        a.VNumber,
                        a.TransactionDate
                    }).ToList();
                return Json(new { success = true, result = ReciveVoucherEntryList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new {success = false, errorMessage = ex.Message}, JsonRequestBehavior.AllowGet);
        }
        }


        //public JsonResult GetPaymentVoucherDetailsByVoucherID(int? VoucherID)
                 //{
                 //    try
                 //    {
                 //        var PaymentVoucherDetailsList = unitOfWork.AccVoucherDetailsRepository.Get().Where(a=>a.VoucherID==VoucherID).Select(s => new VM_AccVoucher()
                 //        {
                 //           VoucherDetailID =s.VoucherDetailID.ToString(),
                 //           VoucherID = s.VoucherID,
                 //           LedgerID = s.LedgerID.ToString(),
                 //           Debit = (double) s.Debit,
                 //           Credit = (double) s.Credit,
                 //           ChequeNumber = s.ChequeNumber,
                 //           TransactionDate = (DateTime) s.TransactionDate,
                 //           VTypeID = (int) s.VTypeID
                 //        }).ToList();
                 //        foreach (var accVoucher in PaymentVoucherDetailsList)
                 //        {
                 //            if (accVoucher.Debit > 0)
                 //            {
                 //                accVoucher.DrCrButton = "Dr";
                 //                accVoucher.DrTextBox = false;
                 //                accVoucher.CrTextBox = true;
                 //                //accVoucher.Credit = "";
                 //            }
                 //            else
                 //            {
                 //                accVoucher.DrCrButton = "Cr";
                 //                accVoucher.DrTextBox = true;
                 //                accVoucher.CrTextBox = false;
                 //            }
                 //        }
                 //        return Json(new { success = true, result = PaymentVoucherDetailsList }, JsonRequestBehavior.AllowGet);
                 //    }
                 //    catch (Exception ex)
                 //    {
                 //        return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
                 //    }
                 //}
        
             [
             SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult UpdatePaymentVoucher(acc_VoucherEntry PaymentVoucherEntry, IEnumerable<VM_AccVoucher> PaymentVoucherDetails)
        {
            
            try
            {
                UpdateVoucher(PaymentVoucherEntry, PaymentVoucherDetails);
                return Json(new { success = true, successMessage = "Payment Voucher Edited Successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {

                return Json(new { success = false, errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }

             public JsonResult UpdateReciveVoucher(acc_VoucherEntry ReciveVoucherEntry, IEnumerable<VM_AccVoucher> ReciveVoucherDetails)
             {

                 try
                 {
                     UpdateVoucher(ReciveVoucherEntry, ReciveVoucherDetails);
                     return Json(new { success = true, successMessage = "Recive Voucher Edited Successfully." }, JsonRequestBehavior.AllowGet);
                 }
                 catch (Exception exception)
                 {

                     return Json(new { success = false, errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
                 }
             }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult UpdateContraVoucher(acc_VoucherEntry ContraVoucherEntry, IEnumerable<VM_AccVoucher> ContraVoucherDetails)
        {

            try
            {
                UpdateVoucher(ContraVoucherEntry, ContraVoucherDetails);
                return Json(new { success = true, successMessage = "Contra Voucher Edited Successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {

                return Json(new { success = false, errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult UpdateJournalVoucher(acc_VoucherEntry JournalVoucherEntry, IEnumerable<VM_AccVoucher> JournalVoucherDetails)
        {

            try
            {
                UpdateVoucher(JournalVoucherEntry, JournalVoucherDetails);
                return Json(new { success = true, successMessage = "Journal Voucher Edited Successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {

                return Json(new { success = false, errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        private void SaveVoucher(string VNumber, string Narration, string VoucherName, int? VTypeID, IEnumerable<VM_AccVoucher> VoucherDetails)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                acc_VoucherEntry voucherEntry = new acc_VoucherEntry();
                voucherEntry.VoucherName = VoucherName;
                voucherEntry.TransactionDate = System.DateTime.Now;
                voucherEntry.Narration = Narration;
                voucherEntry.VTypeID = VTypeID;
                voucherEntry.VNumber = VNumber;
                voucherEntry.OCode = 1;
                voucherEntry.RestaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                voucherEntry.CreatedBy = SessionManger.LoggedInUser(Session);
                voucherEntry.CreatedDateTime = DateTime.Now;
                voucherEntry.EditedBy = null;
                voucherEntry.EditedDateTime = null;
                unitOfWork.AccVoucherEntryRepository.Insert(voucherEntry);
                unitOfWork.Save();

                foreach (VM_AccVoucher voucherDetail in VoucherDetails)
                {
                    acc_VoucherDetail accVoucherDetail = new acc_VoucherDetail();
                    accVoucherDetail.VoucherDetailID = Guid.NewGuid();
                    accVoucherDetail.VoucherID = voucherEntry.VoucherID;
                    accVoucherDetail.LedgerID = Guid.Parse(voucherDetail.LedgerID);
                    accVoucherDetail.Debit = (decimal?)voucherDetail.Debit;
                    accVoucherDetail.Credit = (decimal?)voucherDetail.Credit;
                    accVoucherDetail.ChequeNumber = voucherDetail.ChequeNumber;
                    accVoucherDetail.TransactionDate = System.DateTime.Now;
                    accVoucherDetail.VTypeID = VTypeID;
                    accVoucherDetail.OCode = 1;
                    accVoucherDetail.RestaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                    accVoucherDetail.CreatedBy = SessionManger.LoggedInUser(Session);
                    accVoucherDetail.CreatedDateTime = DateTime.Now;
                    accVoucherDetail.EditedBy = null;
                    accVoucherDetail.EditedDateTime = null;
                    unitOfWork.AccVoucherDetailsRepository.Insert(accVoucherDetail);
                    unitOfWork.Save();
                }
                scope.Complete();
            }
        }

        private void UpdateVoucher(acc_VoucherEntry voucherEntry, IEnumerable<VM_AccVoucher> voucherDetails )
        {
            using (TransactionScope scope = new TransactionScope())
            {
                acc_VoucherEntry aVoucherEntry = unitOfWork.AccVoucherEntryRepository.GetByID(voucherEntry.VoucherID);
                aVoucherEntry.Narration = voucherEntry.Narration;
                aVoucherEntry.EditedBy = SessionManger.LoggedInUser(Session);
                aVoucherEntry.EditedDateTime = DateTime.Now;
                unitOfWork.Save();

                var vd =
                    unitOfWork.AccVoucherDetailsRepository.Get().Where(a => a.VoucherID==voucherEntry.VoucherID);
                foreach (var detail in vd)
                {
                    unitOfWork.AccVoucherDetailsRepository.Delete(detail.VoucherDetailID);
                }
                

                foreach (VM_AccVoucher voucherDetail in voucherDetails)
                {
                    acc_VoucherDetail accVoucherDetail = new acc_VoucherDetail();
                    
                    accVoucherDetail.VoucherDetailID = Guid.NewGuid();
                    accVoucherDetail.VoucherID = aVoucherEntry.VoucherID;
                    accVoucherDetail.LedgerID = Guid.Parse(voucherDetail.LedgerID);
                    accVoucherDetail.Debit = (decimal?)voucherDetail.Debit;
                    accVoucherDetail.Credit = (decimal?)voucherDetail.Credit;
                    accVoucherDetail.ChequeNumber = voucherDetail.ChequeNumber;
                    accVoucherDetail.TransactionDate = aVoucherEntry.TransactionDate;
                    accVoucherDetail.VTypeID = aVoucherEntry.VTypeID;
                    accVoucherDetail.OCode = 1;
                   
                    if (voucherDetail.CreatedBy == "" || voucherDetail.CreatedBy==null)
                    {
                        accVoucherDetail.RestaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                        accVoucherDetail.CreatedBy = SessionManger.LoggedInUser(Session);
                        accVoucherDetail.CreatedDateTime = DateTime.Now;
                    }
                    else
                    {
                        accVoucherDetail.RestaurantId = aVoucherEntry.RestaurantId;
                        accVoucherDetail.CreatedBy = aVoucherEntry.CreatedBy;
                        accVoucherDetail.CreatedDateTime = Convert.ToDateTime(aVoucherEntry.CreatedDateTime);
                    }
                    accVoucherDetail.EditedBy = SessionManger.LoggedInUser(Session);
                    accVoucherDetail.EditedDateTime = DateTime.Now;
                    unitOfWork.AccVoucherDetailsRepository.Insert(accVoucherDetail);
                    unitOfWork.Save();
                }
                scope.Complete();
            }
        }

        public ActionResult ViewPdfReportForVoucher(int? VoucherID)
        {
            
            try
            {
                var VoucherDetailsList = unitOfWork.AccVoucherDetailsRepository.Get().Where(a => a.VoucherID == VoucherID).Select(s => new VM_AccVoucher()
                {
                    VoucherDetailID = s.VoucherDetailID.ToString(),
                    VoucherID = s.VoucherID,
                    LedgerID = s.LedgerID.ToString(),
                    LedgerName = s.acc_Ledger.LedgerName,
                    Debit = (double)s.Debit,
                    Credit = (double)s.Credit,
                    ChequeNumber = s.ChequeNumber,
                    TransactionDate = s.TransactionDate.ToString(),
                    VTypeID = (int)s.VTypeID,
                    OCode = (int)s.OCode,
                    RestaurantId = (int)s.RestaurantId,
                    CreatedBy = s.CreatedBy,
                    CreatedDateTime = s.CreatedDateTime.ToString(),
                    EditedBy = s.EditedBy,
                    EditedDateTime = s.EditedDateTime.ToString()
                }).ToList();
                string VNumber = unitOfWork.AccVoucherEntryRepository.GetByID(VoucherID).VNumber;
                string VoucherName = unitOfWork.AccVoucherEntryRepository.GetByID(VoucherID).VoucherName;
                string transactionDate =
                    unitOfWork.AccVoucherEntryRepository.GetByID(VoucherID).TransactionDate.ToString();
                int restaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString()); ;
                string restaurantName = unitOfWork.RestaurantRepository.GetByID(restaurantId).Name;
                string restaurantAddress = unitOfWork.RestaurantRepository.GetByID(restaurantId).Address;

                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reports/VoucherReport.rdlc");
                localReport.SetParameters(new ReportParameter("VNumber", VNumber));
                localReport.SetParameters(new ReportParameter("VoucherName", VoucherName));
                localReport.SetParameters(new ReportParameter("TransactionDate", transactionDate));
                localReport.SetParameters(new ReportParameter("RestaurantName", restaurantName));
                localReport.SetParameters(new ReportParameter("RestaurantAddress", restaurantAddress));
                ReportDataSource reportDataSource = new ReportDataSource("VoucherDataSet", VoucherDetailsList);

                localReport.DataSources.Add(reportDataSource);
                string reportType = "pdf";
                string mimeType;
                string encoding;
                string fileNameExtension;
                //The DeviceInfo settings should be changed based on the reportType
                //http://msdn.microsoft.com/en-us/library/ms155397.aspx
                string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                //Render the report
                renderedBytes = localReport.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);
                return File(renderedBytes, mimeType);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}