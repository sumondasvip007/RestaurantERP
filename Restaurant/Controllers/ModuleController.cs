using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using Restaurant.Utility;

namespace Restaurant.Controllers
{

    public class ModuleController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Module
      [SessionManger.CheckUserSession]
      [Authorize]
        public ActionResult Index()
        {
            return View();
        }
      [SessionManger.CheckUserSession]
      [Authorize]
        public ActionResult Edit()
        {
            return View();
        }
      [SessionManger.CheckUserSession]
      [Authorize]
        public JsonResult AddModule(tblModule module)
        {
            try
            {
                unitOfWork.ModuleRepository.Insert(module);
                unitOfWork.Save();
                return Json(new { success = true, successMessage = " Successfully Added" }, JsonRequestBehavior.AllowGet);        
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
      [SessionManger.CheckUserSession]
      [Authorize]
        public JsonResult GetAllMoudle()
        {
            try
            {
                List<tblModule> newModule = new List<tblModule>();
               var moduleList =  unitOfWork.ModuleRepository.Get().ToList();
                foreach (var module in moduleList)
                {
                  tblModule aModule = new tblModule();

                    aModule.module_name = module.module_name;
                    aModule.module_icon = module.module_icon;
                    aModule.module_order = module.module_order;
                    aModule.module_id = module.module_id;
                    newModule.Add(aModule);
                }

                return Json(new { result = newModule, success  = true}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new {result =  exception.Message});
            }

            
        }
      [SessionManger.CheckUserSession]
      [Authorize]
        public JsonResult GetModuleById(int? id)
        {
            try
            {
              var newModule = new tblModule();
              var module = unitOfWork.ModuleRepository.GetByID(id);
                newModule.module_id = module.module_id;
                newModule.module_icon = module.module_icon;
                newModule.module_order = module.module_order;
                newModule.module_name = module.module_name;

                return Json(new { result = newModule }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception exception)
            {
             return   Json(new {result = exception.Message}, JsonRequestBehavior.AllowGet);
            }


        }
      [SessionManger.CheckUserSession]
      [Authorize]
        public JsonResult DeleteModulebyId(int? id)
        {
            try
            {
                unitOfWork.ModuleRepository.Delete(id);
                unitOfWork.Save();
                return Json(new { success = true, successMessage = " Successfully Deleted" }, JsonRequestBehavior.AllowGet);        
            }
            catch (Exception exception)
            {
                return Json(new { success = false, errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
               
            }
        }
      [SessionManger.CheckUserSession]
      [Authorize]
        public JsonResult EditMoudleById(tblModule module)
        {
            try
            {
                unitOfWork.ModuleRepository.Update(module);
                unitOfWork.Save();
                return Json(new { success = true, successMessage = "Successfully Edited" }, JsonRequestBehavior.AllowGet);        
            }
            catch (Exception exception)
            {

                return Json(new { success = false, errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }

   


    

  
 
}