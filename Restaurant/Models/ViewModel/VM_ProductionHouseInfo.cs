using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Models.ViewModel
{
    public class VM_ProductionHouseInfo
    {

        public int ProductionHouseId { get; set; }
        public string ProductionHouseName { get; set; }
        public VM_MainStore MainStore { get; set; }
        public VM_OwnStore OwnStore { get; set; }
        public int OldMainStore{ get; set; }
        public int OldOwnStore { get; set; }
        public int NewMainStore { get; set; }
        public int NewOwnStore { get; set; }
        

        public VM_ProductionHouseInfo()
        {
            MainStore  = new VM_MainStore();
            OwnStore = new VM_OwnStore();
        }

    }

 
}