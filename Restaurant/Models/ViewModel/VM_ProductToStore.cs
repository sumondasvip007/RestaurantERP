using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Models.ViewModel
{
    public class VM_ProductToStore
    {
        public int ProductTransferId { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        //public int Quantity { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public double UnitPrice { get; set; }
        public bool isIn { get; set; }
        public bool isOut { get; set; }
        public int ProductTypeId { get; set; }
        public double OpeningBalance { get; set; }
       
    }
}