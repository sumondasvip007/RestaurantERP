using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DAL.Repository;
using Restaurant.Utility;

namespace Restaurant.Controllers
{
    public class ChalanReportViewController:Controller
    {
        UnitOfWork unitOfWork =  new UnitOfWork();
        [SessionManger.CheckUserSession]
        [Authorize]
        public ActionResult ChalanReportView()
        {
            return View();
        }
        [HttpPost]
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetAllChalanReportByDate(string fromDate , string toDate)
        {

            fromDate = String.Format("{0:yyyy/MM/dd}", fromDate);
            toDate = String.Format("{0:yyyy/MM/dd}", toDate);


            try
            {
                var allChalanReport = unitOfWork.ChalanReport.Get().Where(a=>a.Date >= Convert.ToDateTime(fromDate) && a.Date <= Convert.ToDateTime(toDate))
                   
                    .Select(a => new
                    {
                    FromStore = GetStoreInformation(a.FromStore),
                    ToStore = unitOfWork.StoreRepository.GetByID(int.Parse(a.ToStore)).store_name,
                    Supplier = GetSupplierInformation(a.Supplier),
                    Date = Convert.ToString(a.Date.Value.ToLongDateString()),
                    ReportName = a.ReportName,
                    chalanNo = a.chalanNo

                }).ToList();
                return Json(new {success = true, result = allChalanReport}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new {success = false, result = exception.Message});
            }
        }

        private string GetStoreInformation(string storeId)
        {
            if (storeId != null)
            {
                return unitOfWork.StoreRepository.GetByID(Convert.ToInt32(storeId)).store_name;
            }
            return "";
        }

        private string GetSupplierInformation(string supplierId)
        {
            if (supplierId != null)
            {
                return unitOfWork.SuppliersInformationRepository.GetByID(Convert.ToInt32(supplierId)).SupplierName;
            }
            return "";
        }





    }
}