using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Repository;
using Restaurant.Utility;

namespace Restaurant.Controllers
{
    public class UserPermissionsController : Controller
    {
        UnitOfWork unitOfWork =  new UnitOfWork();
        [Authorize]
        [SessionManger.CheckUserSession]
        // GET: UserPermissions
        public ActionResult Index()
        {
            return View();
        }

        /*
        public JsonResult GetAllAspNetUsers()
        {
            try
            {
                var userList = unitOfWork.AspNetUserRepository.Get();

                return Json(new {success = true, result = userList}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new {success = false, errorMessage = ex.Message}, JsonRequestBehavior.AllowGet);
            }

        
        }
         */
    }
}