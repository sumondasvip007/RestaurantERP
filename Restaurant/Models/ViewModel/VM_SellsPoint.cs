using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Models.ViewModel
{
    public class VM_SellsPoint
    {
        public int SellsPointId { get; set; }
        public string SellsPointName { get; set; }
        public int SellsPointStoreId { get; set; }
        public string SellsPointStoreName { get; set; }

    }
}