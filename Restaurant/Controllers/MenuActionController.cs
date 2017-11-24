using System;
using System.Linq;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using System.Data.Entity.Validation;
using System.Diagnostics;
using Restaurant.Utility;

namespace Restaurant.Controllers
{
    public class MenuActionController : Controller
    {
        // GET: MenuAction
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult AddAction()
        {
            return View();
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult ViewAction()
        {
            return View();
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult EditAction()
        {
            return View();
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult UpdateAction(tblAction data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UnitOfWork unitOfWork = new UnitOfWork();
                   
                    unitOfWork.ActionRepository.Update(data);
                    unitOfWork.Save();
                    return Json(new { success = true, successMessage = "Action Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, errorMessage ="Please Fill all fields" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Model is not Valid" }, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult DeleteAction(string id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UnitOfWork unitOfWork = new UnitOfWork();
                    unitOfWork.ActionRepository.Delete(int.Parse(id));
                    unitOfWork.Save();
                    return Json(new { success = true, successMessage = "Action Deleted Successfully" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(new { success = true, successMessage = "Action Can not be deleted" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Model is not Valid" }, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult ViewAllAction()
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                var action = unitOfWork.ActionRepository.Get().ToList();
                 var data = action.Select(s => new
                    {
                        s.id,
                        s.name,
                        s.display_name,
                        module = s.tblModule.module_name,
                        moduleId = s.tblModule.module_id,
                        s.is_in_menu,
                        s.is_view
                    }).ToList();


                 return Json(new { success = true, result = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetActionById(string id)
        {
            int actionId = int.Parse(id);
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                var ac = unitOfWork.ActionRepository.Get().Where(a => a.id == actionId)
                    .Select(s => new
                    {
                        s.id,
                        s.name,
                        s.display_name,
                        ui_module_id = s.tblModule.module_id,

                        s.is_in_menu,
                        s.url,
                        s.is_view
                    }).FirstOrDefault();
                return Json(new { success = true, result = ac }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult AddActionToDb(tblAction data)
        {
            if (ModelState.IsValid)
            {

            try
            {
                //data.url = GenerateURL(data.name);
                UnitOfWork unitOfWork = new UnitOfWork();
                unitOfWork.ActionRepository.Insert(data);
                unitOfWork.Save();
                return Json(new {success = true, successMessage = "Action Saved Successfully"},
                    JsonRequestBehavior.AllowGet);
            }

            catch (DbEntityValidationException ex)
            {
                string st = "";
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        st += validationError.ErrorMessage;
                        //Trace.TraceInformation("Property: {0} Error: {1}",
                        //validationError.PropertyName,
                        //validationError.ErrorMessage);
                        //Trace.TraceInformation("Property: {0} Error: {1}",
                        //validationError.PropertyName,
                        // validationError.ErrorMessage);
                    }
                }

                return Json(new {success = false, errorMessage = st}, JsonRequestBehavior.AllowGet);
            }
        }
            else
            {
                return Json(new { success = false, errorMessage = "Please fill required fields" }, JsonRequestBehavior.AllowGet);
            }
    }

        [HttpGet]
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetModules()
        {
            try
            {
                UnitOfWork unitOfWork = new UnitOfWork();
                var modules = unitOfWork.ModuleRepository.Get()
                    .OrderBy(a => a.module_id)
                    .Select(s => new
                {
                    s.module_id,
                    s.module_name
                }).ToList();
                return Json(new { success = true, result = modules }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { success = false, errorMessage = "Action Can not be deleted" }, JsonRequestBehavior.AllowGet);
            }
        }

        private string GenerateURL(string actionName)
        {
            string url = actionName.Replace(" ", "");
            return url = "#/" + url;
        }
    }
}