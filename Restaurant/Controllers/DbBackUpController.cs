using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using DAL.Repository;
using Restaurant.Utility;

namespace Restaurant.Controllers
{
    public class DbBackUpController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: DbBackUp
        public ActionResult DbBackUp()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        [SessionManger.CheckUserSessionAttribute]
        //[ValidateAntiForgeryToken]
        public JsonResult GetDbBackUpPath()
        {
            try
            {
                var path = WebConfigurationManager.AppSettings["DbBackupPath"];
                return Json(new { success = true, result = path }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult BackupDatabase()
        {
            try
            {
                var message="";
                var path = WebConfigurationManager.AppSettings["DbBackupPath"];
                 var result = unitOfWork.CustomRepository.sp_BackUpDatabase(path);
                if (result == 0)
                {
                    message = "Database Backup Failed";
                }
                else
                {
                    message = "Database Backup Successfully";
                }
                return Json(new { success = true, result = message }, JsonRequestBehavior.AllowGet);
           
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}