using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Repository;
using Restaurant.Models.ViewModel;
using Restaurant.Utility;

namespace Restaurant.Controllers
{
    public class MainStoreProductTransferStatusController:Controller
    {
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult MainStoreToProductionHouse()
        {
            return View();
        }
        UnitOfWork unitOfWork = new UnitOfWork();

        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetMainStoreToProductionHouseInfo(string fromDate, string toDate, int storeId)
        {
            try
            {

                // convert the date time 
                String[] parts = fromDate.Split('-');

                int fromdate = Convert.ToInt32(parts[0]);
                int frommonth = Convert.ToInt32(parts[1]);
                int fromyear = Convert.ToInt32(parts[2]);


                String[] toparts = toDate.Split('-');

                int todate = Convert.ToInt32(toparts[0]);
                int tomonth = Convert.ToInt32(toparts[1]);
                int toyear = Convert.ToInt32(toparts[2]);


                DateTime fromdt = Convert.ToDateTime(frommonth + "-" + fromdate + "-" + fromyear);
                DateTime todt = Convert.ToDateTime(tomonth + "-" + todate + "-" + toyear);

                //call store procedure 
                var productTranferInfo = unitOfWork.CustomRepository.sp_GetMainStoreToProductionHouseStatus(fromdt, todt,
                    storeId);

                return Json(new { success = true, result = productTranferInfo }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { success = false, result = exception.Message });
            }


            
        }





    }
}