using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Repository;
using Restaurant.Utility;

namespace Restaurant.Controllers
{
    public class HomeController : Controller
    {
        UnitOfWork unitOfWork=new UnitOfWork();
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult Index()
        {
            return PartialView();
        }
        public ActionResult RedirectToMain()
        {
            return PartialView();
        }
        public ActionResult HomeMaster()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return PartialView();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return PartialView();
        }


        [HttpGet]
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetWeeklyTotalProductSellChart()
        {
            try
            {
                var dayData = unitOfWork.CustomRepository.sp_weeklyTotalProductSellByGraph().Select(a => a.Day).ToList();
                var valueData = unitOfWork.CustomRepository.sp_weeklyTotalProductSellByGraph().Select(a => a.Value).ToList();


                var data = unitOfWork.CustomRepository.sp_weeklyTotalProductSellByGraph().ToList();

                return Json(new { success = true, DayData = dayData, ValueData = valueData }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new {success = false, errorMessage = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

             [HttpGet]
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetMonthlyTotalAmountChartByDayShiftWise()
        {
            try
            {
                var dayData = unitOfWork.CustomRepository.sp_monthlyTotalAmountForDayShift().Select(a => a.DayForMonth).ToList();
                var valueData = unitOfWork.CustomRepository.sp_monthlyTotalAmountForDayShift().Select(a => a.Value).ToList();


                var data = unitOfWork.CustomRepository.sp_monthlyTotalAmountForDayShift().ToList();

                return Json(new { success = true, DayData = dayData, ValueData = valueData }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new {success = false, errorMessage = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

              [HttpGet]
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetMonthlyTotalAmountChartByNightShiftWise()
        {
            try
            {
                var dayData = unitOfWork.CustomRepository.sp_monthlyTotalAmountForNightShift().Select(a => a.DayForMonth).ToList();
                var valueData = unitOfWork.CustomRepository.sp_monthlyTotalAmountForNightShift().Select(a => a.Value).ToList();


                var data = unitOfWork.CustomRepository.sp_monthlyTotalAmountForNightShift().ToList();

                return Json(new { success = true, DayData = dayData, ValueData = valueData }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new {success = false, errorMessage = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

               [HttpGet]
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetMonthlyTotalAmountChart()
        {
            try
            {
                var dayData = unitOfWork.CustomRepository.sp_monthlyTotalAmount().Select(a => a.DayForMonth).ToList();
                var valueData = unitOfWork.CustomRepository.sp_monthlyTotalAmount().Select(a => a.Value).ToList();


                var data = unitOfWork.CustomRepository.sp_monthlyTotalAmount().ToList();

                return Json(new { success = true, DayData = dayData, ValueData = valueData }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new {success = false, errorMessage = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

                  [HttpGet]
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetWeeklyTotalAmountChartByDayShiftWise()
        {
            try
            {
                var dayData = unitOfWork.CustomRepository.sp_weeklyTotalAmountDayShiftWise().Select(a => a.DayOfWeek).ToList();
                var valueData = unitOfWork.CustomRepository.sp_weeklyTotalAmountDayShiftWise().Select(a => a.Value).ToList();


                var data = unitOfWork.CustomRepository.sp_weeklyTotalAmountDayShiftWise().ToList();

                return Json(new { success = true, DayData = dayData, ValueData = valueData }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new {success = false, errorMessage = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

         [HttpGet]
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetWeeklyTotalAmountChartByNightShiftWise()
        {
            try
            {
                var dayData = unitOfWork.CustomRepository.sp_weeklyTotalAmountNightShiftWise().Select(a => a.DayOfWeek).ToList();
                var valueData = unitOfWork.CustomRepository.sp_weeklyTotalAmountNightShiftWise().Select(a => a.Value).ToList();


                var data = unitOfWork.CustomRepository.sp_weeklyTotalAmountNightShiftWise().ToList();

                return Json(new { success = true, DayData = dayData, ValueData = valueData }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new {success = false, errorMessage = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        
               [HttpGet]
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetWeeklyTotalAmountChart()
        {
            try
            {
                var dayData = unitOfWork.CustomRepository.sp_weeklyTotalAmount().Select(a => a.DayOfWeek).ToList();
                var valueData = unitOfWork.CustomRepository.sp_weeklyTotalAmount().Select(a => a.Value).ToList();


                var data = unitOfWork.CustomRepository.sp_monthlyTotalAmount().ToList();

                return Json(new { success = true, DayData = dayData, ValueData = valueData }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new {success = false, errorMessage = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }
        
    }
}