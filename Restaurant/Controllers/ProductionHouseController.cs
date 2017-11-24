using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using Restaurant.Models.ViewModel;
using System.Transactions;
using Restaurant.Utility;

namespace Restaurant.Controllers
{
    public class ProductionHouseController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: ProductionHouse
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult ShowProductionHouse()
        {
            return View();
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetSellsHouseStore()
        {
            try
            {
                var productionHouseInfoList = new List<tblStoreInformation>();

                var mainStore = (from a in unitOfWork.StoreRepository.Get() where a.is_mainStore == true select a).ToList();


                foreach (var a in mainStore)
                {
                    var store = new tblStoreInformation();
                    store.store_name = a.store_name;
                    store.store_id = a.store_id;
                    productionHouseInfoList.Add(store);
                }

                return Json(new { result = productionHouseInfoList, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetALLOwnStoreWhichCanBeAdded()
        {
            try
            {
                var productionHouseInfoList = new List<tblStoreInformation>();

                var mainStore = (from a in unitOfWork.StoreRepository.Get() where a.isProductionHouseStore == true && a.ProductionHouseId==null select a).ToList();


                foreach (var a in mainStore)
                {
                    var store = new tblStoreInformation();
                    store.store_name = a.store_name;
                    store.store_id = a.store_id;
                    productionHouseInfoList.Add(store);
                }
                return Json(new { result = productionHouseInfoList, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllOwnStore()
        {
            try
            {
                var productionHouseInfoList = new List<tblStoreInformation>();

                var mainStore = (from a in unitOfWork.StoreRepository.Get() where a.isProductionHouseStore == true select a).ToList();
                foreach (var a in mainStore)
                {
                    var store = new tblStoreInformation();
                    store.store_name = a.store_name;
                    store.store_id = a.store_id;
                    productionHouseInfoList.Add(store);
                }
                return Json(new { result = productionHouseInfoList, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult AddProductionHouse(tblProductionHouseInformation productionHosueInformation)
        {
            try
            {

                UnitOfWork unitOfWork = new UnitOfWork();
                //save production house 
                unitOfWork.ProductionHouseInformationRepository.Insert(productionHosueInformation);
                unitOfWork.Save();
                //get data from store house by this production id
                var lastestProductionHouseInfo =
                    (from a in unitOfWork.ProductionHouseInformationRepository.Get()
                     orderby a.ProductionHouseId descending
                     select a).FirstOrDefault();

                //############ OWN STORE PARENT STORE HOUSE ID  SAVE   ####################
                var storeInfoByOwnStoreId = unitOfWork.StoreRepository.GetByID(productionHosueInformation.OwnStore);

                storeInfoByOwnStoreId.ParentStoreId = lastestProductionHouseInfo.MainStore;
                storeInfoByOwnStoreId.ProductionHouseId = lastestProductionHouseInfo.ProductionHouseId;

                unitOfWork.StoreRepository.Update(storeInfoByOwnStoreId);

                unitOfWork.Save();

                return Json(new { success = true, successMessage = "Successfully Added production House" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllProductionHouse()
        {
            try
            {

                //##EMETY  LIST  VM_ProductionHouseInfo
                var productionHouseInfoList = new List<VM_ProductionHouseInfo>();

                //### GET ALL PRODUCT HOUSE INFO #####
                var productionHouseList = (from a in unitOfWork.ProductionHouseInformationRepository.Get() select a).ToList();


                foreach (var productionHouse in productionHouseList)
                {
                    //###  SET PRODUCT HOUSE INFO TO VM_ProductionHouseInfo 
                    var newProductionHouse = new VM_ProductionHouseInfo(); 
                    newProductionHouse.ProductionHouseId = productionHouse.ProductionHouseId; 
                    newProductionHouse.ProductionHouseName = productionHouse.ProductionHouseName;
                    //###  GET A MAIN STORE NAME FROM MAIN STORE ID  
                 
                    var storeNameByMainStore = unitOfWork.StoreRepository.GetByID(productionHouse.MainStore);

                    if (storeNameByMainStore != null)
                    {
                        newProductionHouse.MainStore.Name = storeNameByMainStore.store_name;
                    }
                   //###  GET A OWN STORE NAME FROM OWN STORE ID 
                    
                    var storeNameByOwnStore = unitOfWork.StoreRepository.GetByID(productionHouse.OwnStore);

                    if (storeNameByOwnStore != null)
                    {
                        newProductionHouse.OwnStore.Name = storeNameByOwnStore.store_name;
                    }
              
                  //## INSERT newProductionHouse TO  EMETY LIST(productionHouseInfoList) 
                   productionHouseInfoList.Add(newProductionHouse);
                }


                return Json(new { result = productionHouseInfoList, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetPrudctionHouseInformationById(int? id)
        {
            try
            {
                VM_ProductionHouseInfo productionHouseInformation = new VM_ProductionHouseInfo();
               
        
                //## GET Production House By Id #######
                var aProductionHouseInfo = unitOfWork.ProductionHouseInformationRepository.GetByID(id);


                //## GET Production House By Id #######
                productionHouseInformation.ProductionHouseId = aProductionHouseInfo.ProductionHouseId;
                productionHouseInformation.ProductionHouseName = aProductionHouseInfo.ProductionHouseName;
                productionHouseInformation.OwnStore.Id = aProductionHouseInfo.OwnStore;
                productionHouseInformation.OwnStore.Name =
                    unitOfWork.StoreRepository.GetByID(aProductionHouseInfo.OwnStore).store_name;
 
                productionHouseInformation.MainStore.Id = aProductionHouseInfo.MainStore;

                //## Get Store Info By OwnStore 
                var getStoreByOwnStore = unitOfWork.StoreRepository.GetByID(aProductionHouseInfo.OwnStore);
                //## Null the exsit value 
            
                unitOfWork.StoreRepository.Update(getStoreByOwnStore);
                unitOfWork.Save();
                
                           
                return Json(new {result = productionHouseInformation, success = true}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new {errorMessage = exception.Message}, JsonRequestBehavior.AllowGet);
            }

        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult EditProductionHouse(VM_ProductionHouseInfo productionHouseInformation)
        {
            try
            {
                //1. Get Store Old Store  
                var getOldStore = unitOfWork.StoreRepository.GetByID(productionHouseInformation.OldOwnStore);
                //2. Null The Exist Value
                getOldStore.ParentStoreId = null;
                getOldStore.ProductionHouseId = null;
                unitOfWork.StoreRepository.Update(getOldStore);
                //3.Give The updated from the form
                var getNewStore = unitOfWork.StoreRepository.GetByID(productionHouseInformation.NewOwnStore);
                getNewStore.ParentStoreId = productionHouseInformation.NewMainStore;
                getNewStore.ProductionHouseId = productionHouseInformation.ProductionHouseId;
                unitOfWork.StoreRepository.Update(getNewStore);   
                //4.Update production House inormation 
                var newProductionHouseInfo =
                    unitOfWork.ProductionHouseInformationRepository.GetByID(productionHouseInformation.ProductionHouseId);
                newProductionHouseInfo.ProductionHouseName = productionHouseInformation.ProductionHouseName;
                newProductionHouseInfo.OwnStore = productionHouseInformation.NewOwnStore;
                newProductionHouseInfo.MainStore = productionHouseInformation.NewMainStore;
                unitOfWork.ProductionHouseInformationRepository.Update(newProductionHouseInfo); 
                unitOfWork.Save();
                return Json(new { success = true, successMessage = "Successfully Edited prodution House" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            

        }
       




    }
}