using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Models.ViewModel
{
    public class VM_StoreInformation
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public bool is_mainStore { get; set; }
        public bool isProductionHouseStore { get; set; }
        public bool IsSellsPointStore { get; set; }
    }
}