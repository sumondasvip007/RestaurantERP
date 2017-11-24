using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using DAL.ViewModel;
using Restaurant.Models.ViewModel;
using Restaurant.Utility;

namespace Restaurant.Controllers
{
    public class GroupAndShiftMappingController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: GroupAndShiftMapping

        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult AddGroupAndShiftMapping()
        {
            return View();
        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllGroup()
        {
            try
            {

                var groupList = (from a in unitOfWork.GroupForShiftRepository.Get()
                                 select new VM_GroupForShift()
                                 {

                                     GroupId = a.GroupId,
                                     GroupName = a.GroupName

                                 }).ToList();
                return Json(new { success = true, result = groupList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllShift()
        {
            try
            {

                var shiftList = (from a in unitOfWork.ShiftRepository.Get()
                                 select new VM_Shift()
                                 {

                                     ShiftId = a.ShiftId,
                                     ShiftName = a.ShiftName

                                 }).ToList();
                return Json(new { success = true, result = shiftList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult AddGroupAndShiftMapping(VM_GroupAndShiftMapping groupAndShiftMapping, DateTime fromDate)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var date = DateTime.Parse(fromDate.ToString());
                    var dateInfo = string.Format("{0:yyyy-MM-dd}", date);
                    var inputDbDate =
                        unitOfWork.GroupAndShiftMappingRepository.Get().Where(a => a.TransferDate.Value.Date == Convert.ToDateTime(dateInfo)).Select(x => x.TransferDate).ToList();

                    if (!inputDbDate.Any())
                    {
                        if (groupAndShiftMapping.ShiftId == 1)
                        {
                            if (groupAndShiftMapping.GroupId == 1)
                            {
                                tblGroupAndShiftMapping aGroupAndShiftMapping = new tblGroupAndShiftMapping();

                                aGroupAndShiftMapping.TransferDate = fromDate;
                                aGroupAndShiftMapping.Day = "A";
                                aGroupAndShiftMapping.Night = "B";
                                aGroupAndShiftMapping.RestaurantId =
                                    Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                                aGroupAndShiftMapping.CreatedBy = SessionManger.LoggedInUser(Session);
                                aGroupAndShiftMapping.CreatedDateTime = DateTime.Now;
                                aGroupAndShiftMapping.EditedBy = null;
                                aGroupAndShiftMapping.EditedDateTime = null;
                                unitOfWork.GroupAndShiftMappingRepository.Insert(aGroupAndShiftMapping);
                                unitOfWork.Save();

                                return
                                    Json(new { success = true, successMessage = "Group and Shift Mapping successfully" },
                                        JsonRequestBehavior.AllowGet);
                            }

                            else
                            {
                                tblGroupAndShiftMapping aGroupAndShiftMapping = new tblGroupAndShiftMapping();

                                aGroupAndShiftMapping.TransferDate = fromDate;
                                aGroupAndShiftMapping.Day = "B";
                                aGroupAndShiftMapping.Night = "A";
                                aGroupAndShiftMapping.RestaurantId =
                                    Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                                aGroupAndShiftMapping.CreatedBy = SessionManger.LoggedInUser(Session);
                                aGroupAndShiftMapping.CreatedDateTime = DateTime.Now;
                                aGroupAndShiftMapping.EditedBy = null;
                                aGroupAndShiftMapping.EditedDateTime = null;
                                unitOfWork.GroupAndShiftMappingRepository.Insert(aGroupAndShiftMapping);
                                unitOfWork.Save();

                                return
                                    Json(new { success = true, successMessage = "Group and Shift Mapping successfully" },
                                        JsonRequestBehavior.AllowGet);
                            }

                        }

                        else
                        {
                            if (groupAndShiftMapping.GroupId == 1)
                            {
                                tblGroupAndShiftMapping aGroupAndShiftMapping = new tblGroupAndShiftMapping();

                                aGroupAndShiftMapping.TransferDate = fromDate;
                                aGroupAndShiftMapping.Day = "B";
                                aGroupAndShiftMapping.Night = "A";
                                aGroupAndShiftMapping.RestaurantId =
                                    Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                                aGroupAndShiftMapping.CreatedBy = SessionManger.LoggedInUser(Session);
                                aGroupAndShiftMapping.CreatedDateTime = DateTime.Now;
                                aGroupAndShiftMapping.EditedBy = null;
                                aGroupAndShiftMapping.EditedDateTime = null;
                                unitOfWork.GroupAndShiftMappingRepository.Insert(aGroupAndShiftMapping);
                                unitOfWork.Save();

                                return
                                    Json(new { success = true, successMessage = "Group and Shift Mapping successfully" },
                                        JsonRequestBehavior.AllowGet);
                            }

                            else
                            {
                                tblGroupAndShiftMapping aGroupAndShiftMapping = new tblGroupAndShiftMapping();

                                aGroupAndShiftMapping.TransferDate = fromDate;
                                aGroupAndShiftMapping.Day = "A";
                                aGroupAndShiftMapping.Night = "B";
                                aGroupAndShiftMapping.RestaurantId =
                                    Int32.Parse(SessionManger.RestaurantOfLoggedInUser(Session).ToString());
                                aGroupAndShiftMapping.CreatedBy = SessionManger.LoggedInUser(Session);
                                aGroupAndShiftMapping.CreatedDateTime = DateTime.Now;
                                aGroupAndShiftMapping.EditedBy = null;
                                aGroupAndShiftMapping.EditedDateTime = null;
                                unitOfWork.GroupAndShiftMappingRepository.Insert(aGroupAndShiftMapping);
                                unitOfWork.Save();

                                return
                                    Json(new { success = true, successMessage = "Group and Shift Mapping successfully" },
                                        JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    else
                    {
                        return Json(new { success = false, errorMessage = "Group and Shift Mapping Already done in this Date " }, JsonRequestBehavior.AllowGet);
                    }



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

        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult ViewGroupAndShiftMappingList()
        {
            return View();
        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllGroupAndShiftMapping()
        {
            try
            {

                var groupAndShiftMappingList = (from a in unitOfWork.GroupAndShiftMappingRepository.Get()
                                                select new VM_GroupAndShiftMapping()
                                                {
                                                    GroupMappingId = a.GroupMappingId,
                                                    Date = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(a.TransferDate.ToString())),
                                                    Day = a.Day,
                                                    Night = a.Night

                                                }).ToList();
                return Json(new { success = true, result = groupAndShiftMappingList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult DeleteGroupAndShiftMapping(int? id)
        {
            try
            {
                tblGroupAndShiftMapping groupAndShiftMapping = unitOfWork.GroupAndShiftMappingRepository.GetByID(id);
                if (groupAndShiftMapping == null)
                {
                    return Json(new { success = false, errorMessage = "Delete Failed" }, JsonRequestBehavior.AllowGet);
                }

                unitOfWork.GroupAndShiftMappingRepository.Delete(groupAndShiftMapping);
                unitOfWork.Save();
                return Json(new { success = true, message = " Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult UpdateGroupAndShiftMapping(VM_GroupAndShiftMapping groupAndShiftMapping)
        {
           
           
           
            try
            {

                tblGroupAndShiftMapping bGroupAndShiftMapping = unitOfWork.GroupAndShiftMappingRepository.GetByID(groupAndShiftMapping.GroupMappingId);
                if (bGroupAndShiftMapping == null)
                {
                    return Json(new { success = false, errorMessage = "Edit Failed" }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    if (groupAndShiftMapping.ShiftId == 1)
                    {
                        if (groupAndShiftMapping.GroupId == 1)
                        {
                            tblGroupAndShiftMapping aGroupAndShiftMapping = unitOfWork.GroupAndShiftMappingRepository.GetByID(groupAndShiftMapping.GroupMappingId);
                          
                            aGroupAndShiftMapping.GroupMappingId = groupAndShiftMapping.GroupMappingId;
                            //aGroupAndShiftMapping.TransferDate = groupAndShiftMapping.TransferDate;
                            aGroupAndShiftMapping.Day = "A";
                            aGroupAndShiftMapping.Night = "B";
                            aGroupAndShiftMapping.EditedBy = SessionManger.LoggedInUser(Session);
                            aGroupAndShiftMapping.EditedDateTime = DateTime.Now;
                            unitOfWork.GroupAndShiftMappingRepository.Update(aGroupAndShiftMapping);
                            unitOfWork.Save();
                            return Json(new { success = true, successMessage = "Group and Shift Mapping edited successfully" }, JsonRequestBehavior.AllowGet);
                        }

                        else
                        {
                            tblGroupAndShiftMapping aGroupAndShiftMapping = unitOfWork.GroupAndShiftMappingRepository.GetByID(groupAndShiftMapping.GroupMappingId);
                           
                            aGroupAndShiftMapping.GroupMappingId = groupAndShiftMapping.GroupMappingId;
                            //aGroupAndShiftMapping.TransferDate = groupAndShiftMapping.TransferDate;
                            aGroupAndShiftMapping.Day = "B";
                            aGroupAndShiftMapping.Night = "A";
                            aGroupAndShiftMapping.EditedBy = SessionManger.LoggedInUser(Session);
                            aGroupAndShiftMapping.EditedDateTime = DateTime.Now;
                            unitOfWork.GroupAndShiftMappingRepository.Update(aGroupAndShiftMapping);
                            unitOfWork.Save();
                            return Json(new { success = true, successMessage = "Group and Shift Mapping edited successfully" }, JsonRequestBehavior.AllowGet);
                        }

                    }

                    else
                    {
                        if (groupAndShiftMapping.GroupId == 1)
                        {
                            tblGroupAndShiftMapping aGroupAndShiftMapping = unitOfWork.GroupAndShiftMappingRepository.GetByID(groupAndShiftMapping.GroupMappingId);

                            aGroupAndShiftMapping.GroupMappingId = groupAndShiftMapping.GroupMappingId;
                            //aGroupAndShiftMapping.TransferDate = groupAndShiftMapping.TransferDate;
                            aGroupAndShiftMapping.Day = "B";
                            aGroupAndShiftMapping.Night = "A";
                            aGroupAndShiftMapping.EditedBy = SessionManger.LoggedInUser(Session);
                            aGroupAndShiftMapping.EditedDateTime = DateTime.Now;
                            unitOfWork.GroupAndShiftMappingRepository.Update(aGroupAndShiftMapping);
                            unitOfWork.Save();
                            return Json(new { success = true, successMessage = "Group and Shift Mapping edited successfully" }, JsonRequestBehavior.AllowGet);
                        }

                        else
                        {
                            tblGroupAndShiftMapping aGroupAndShiftMapping = unitOfWork.GroupAndShiftMappingRepository.GetByID(groupAndShiftMapping.GroupMappingId);

                            aGroupAndShiftMapping.GroupMappingId = groupAndShiftMapping.GroupMappingId;
                            //aGroupAndShiftMapping.TransferDate = groupAndShiftMapping.TransferDate;
                            aGroupAndShiftMapping.Day = "A";
                            aGroupAndShiftMapping.Night = "B";
                            aGroupAndShiftMapping.EditedBy = SessionManger.LoggedInUser(Session);
                            aGroupAndShiftMapping.EditedDateTime = DateTime.Now;
                            unitOfWork.GroupAndShiftMappingRepository.Update(aGroupAndShiftMapping);
                            unitOfWork.Save();
                            return Json(new { success = true, successMessage = "Group and Shift Mapping edited successfully" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }

               
            }
            catch (Exception exception)
            {

                return Json(new { success = false, errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}