using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

namespace Restaurant.Models.ViewModel
{
    public class Vm_ProductTransfetToProductionHouse
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int? MainStoreId { get; set; }
        public string MainStoreName { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Unit { get; set; }
        public tblProductInformation ProductInformation { get; set; }
        //public int? Quantity { get; set; }
        public decimal? Quantity { get; set; }
        //public int AvailableQuatity { get; set; }
        public decimal AvailableQuatity { get; set; }
        //public int? MainStoreQuantity { get; set; }
        public decimal? MainStoreQuantity { get; set; }
        public int ProductIndex { get; set; }
        public int SuppierId { get; set; }

        public Vm_ProductTransfetToProductionHouse()
        {
            ProductInformation =  new tblProductInformation();
        }


    }
}