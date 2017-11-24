using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Models.ViewModel
{
    public class VM_MainStoreToProductionHouseReport
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int ProductionHouseId { get; set; }
        public string ProductionHouseName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }  
    }
}