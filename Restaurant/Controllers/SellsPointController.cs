using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using Restaurant.Models.ViewModel;
using Restaurant.Utility;

namespace Restaurant.Controllers
{
    public class SellsPointController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: SellsPoint
        [SessionManger.CheckUserSession]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult SellsPointAdd()
        {
            return View();
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetStoreInformation()
        {
            try
            {
                IEnumerable<VM_StoreInformation> StoreList = (from a in unitOfWork.StoreRepository.Get().Where(a => a.IsSellsPointStore == true && a.SellsPointStoreId==null)
                    select new VM_StoreInformation()
                    {
                        StoreId = a.store_id,
                        StoreName = a.store_name,
                    }).ToList();
                return Json(new { success = true, result = StoreList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult SaveSellsPoint(VM_SellsPoint aSellsPoint)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tblSellsPoint sellsPoint=new tblSellsPoint();
                    sellsPoint.SellsPointName = aSellsPoint.SellsPointName;
                    sellsPoint.SellsPointStoreId = aSellsPoint.SellsPointStoreId;
                    sellsPoint.RestaurantId = Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                    sellsPoint.CreatedBy = SessionManger.LoggedInUser(Session);
                    sellsPoint.CreatedDateTime = DateTime.Now;
                    sellsPoint.EditedBy = null;
                    sellsPoint.EditedDateTime = null;
                    unitOfWork.SellsPointRepository.Insert(sellsPoint);
                    unitOfWork.Save();

                    tblStoreInformation aStoreInformation =
                        unitOfWork.StoreRepository.GetByID(sellsPoint.SellsPointStoreId);
                    aStoreInformation.SellsPointStoreId = sellsPoint.SellsPointId;
                    unitOfWork.StoreRepository.Update(aStoreInformation);
                    unitOfWork.Save();
                    return Json(new { success = true, successMessage = "Sells point added successfully" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Model is not Valid" }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult SellsPointDetails()
        {
            return View();
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetAllSellsPoint()
        {
            try
            {
                var sellsPointList = (from a in unitOfWork.SellsPointRepository.Get()
                                   select new VM_SellsPoint()
                                   {
                                       SellsPointId = a.SellsPointId,
                                       SellsPointName = a.SellsPointName,
                                       SellsPointStoreId = a.SellsPointStoreId,
                                       SellsPointStoreName = a.tblStoreInformation.store_name
                                   }).ToList();
                return Json(new { success = true, result = sellsPointList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult DeleteSellsPoint(int? id)
        {
            try
            {
                tblSellsPoint sellsPoint = unitOfWork.SellsPointRepository.GetByID(id);
                if (sellsPoint == null)
                {
                    return Json(new { success = false, errorMessage = "Sells Point Delete Failed" }, JsonRequestBehavior.AllowGet);
                }
                tblStoreInformation aStoreInformation = unitOfWork.StoreRepository.GetByID(sellsPoint.SellsPointStoreId);
                aStoreInformation.SellsPointStoreId = null;
                unitOfWork.StoreRepository.Update(aStoreInformation);
                unitOfWork.SellsPointRepository.Delete(sellsPoint);
                unitOfWork.Save();
                return Json(new { success = true, message = "Sells Point Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult UpdateSellsPoint(VM_SellsPoint aSellsPoint)
        {
            tblSellsPoint sellsPoint = unitOfWork.SellsPointRepository.GetByID(aSellsPoint.SellsPointId);
            sellsPoint.SellsPointId = aSellsPoint.SellsPointId;
            sellsPoint.SellsPointName = aSellsPoint.SellsPointName;
            sellsPoint.SellsPointStoreId = aSellsPoint.SellsPointStoreId;
            sellsPoint.EditedBy = SessionManger.LoggedInUser(Session);
            sellsPoint.EditedDateTime = DateTime.Now;
            try
            {
                //Update previous StoreInformation SellsPointStoreId
                tblStoreInformation bStoreInformation =
                    unitOfWork.StoreRepository.Get().Where(a => a.SellsPointStoreId == sellsPoint.SellsPointId).FirstOrDefault();
                bStoreInformation.SellsPointStoreId = null;
                unitOfWork.StoreRepository.Update(bStoreInformation);

                //Update Present StoreInformation SellsPointStoreId
                tblStoreInformation aStoreInformation =
                       unitOfWork.StoreRepository.GetByID(sellsPoint.SellsPointStoreId);
                aStoreInformation.SellsPointStoreId = sellsPoint.SellsPointId;
                unitOfWork.StoreRepository.Update(aStoreInformation);

                //Update SellsPointRepository
                unitOfWork.SellsPointRepository.Update(sellsPoint);
                unitOfWork.Save();
                return Json(new { success = true, successMessage = "Sells Point Edited Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {

                return Json(new { success = false, errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}