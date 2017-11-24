using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Repository;
using Restaurant.Models.ViewModel;

namespace Restaurant.Controllers
{
    public class ProductionHouseProductTransferStatusController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: ProductionHouseProductTransferStatus
        public ActionResult ProductionHoseProductTransferStatus()
        {
            return View();
        }

        public JsonResult GetProductionHouseInformation()
        {
            try
            {
                IEnumerable<VM_StoreInformation> ProductionHouseList = (from a in unitOfWork.StoreRepository.Get().Where(a => a.isProductionHouseStore == true)
                    select new VM_StoreInformation()
                    {
                        StoreId = a.store_id,
                        StoreName = a.store_name,
                    }).ToList();
                return Json(new { success = true, result = ProductionHouseList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SearchProductTransactionList(int productionHouseStoreId, DateTime fromDate, DateTime toDate)
        {

            try
            {
                IEnumerable<VM_Product> productList = (from a in unitOfWork.ProductTransferRepository.Get().Where(a => a.StoreId == productionHouseStoreId && a.isOut == true && a.CreatedDateTime >= fromDate && a.CreatedDateTime <= toDate)
                                                       select new VM_Product()
                                                                        {
                                                                            ProductId = (int)a.ProductId,
                                                                            ProductName = a.tblProductInformation.ProductName,
                                                                            ProductTypeName = a.tblProductInformation.tblProductType.ProductTypeName,
                                                                            Quantity = (int)a.Quantity,
                                                                            DateTime = a.CreatedDateTime.ToString()
                                                                        }).ToList();
                if (productList.Any())
                {
                    return Json(new { success = true, result = productList }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, errorMessage = "No product transition found." }, JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}