using System;
using System.Linq;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using Restaurant.Utility;

namespace Restaurant.Controllers
{
    public class StoreController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Sotore Information
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult Store()
        {
            return View();
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult AddStore(tblStoreInformation _store)
        {
            try
            {
                unitOfWork.StoreRepository.Insert(_store);
                unitOfWork.Save();
                return Json(new { success = true, successMessage = " Successfully Added" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllStore()
        {
            try
            {
                var storeList = unitOfWork.StoreRepository.Get()
                    .Select(s=>new
                    {
                        s.is_mainStore,
                        s.IsActive,
                        s.store_id,
                        s.store_name
                    })
                    .ToList();
                return Json(new { result = storeList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { result = exception.Message });
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetStoreById(int? id)
        {
            try
            {
                var store = unitOfWork.StoreRepository.Get().Where(a=>a.store_id == id).Select(s=>new
                {
                    s.store_id,
                    s.store_name,
                    s.is_mainStore,
                    s.IsActive,
                    s.isProductionHouseStore,
                    s.IsSellsPointStore
                }).FirstOrDefault();

                return Json(new { result = store }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { result = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult DeleteStorebyId(int? id)
        {
            try
            {
                unitOfWork.StoreRepository.Delete(id);
                unitOfWork.Save();
                return Json(new { success = true, successMessage = " Successfully Deleted" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { success = false, errorMessage = "You can't delete this item!" }, JsonRequestBehavior.AllowGet);

            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult EditStoreById(tblStoreInformation store)
        {
            try
            {   




                
                unitOfWork.StoreRepository.Update(store);
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