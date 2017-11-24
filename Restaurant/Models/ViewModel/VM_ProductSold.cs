using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

namespace Restaurant.Models.ViewModel
{
    public class VM_ProductSold
    {
        public int ProductId { get; set; }

        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string ProductName { get; set; }

        public decimal Quantity { get; set; }

        public string Unit { get; set; }

        public decimal ?UnitPrice { get; set; }

        public decimal ?AvailableQuantity { get; set; }


    }
}