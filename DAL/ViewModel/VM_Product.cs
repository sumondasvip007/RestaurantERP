﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
   public  class VM_Product
    {
        public int ProductId { get; set; }
        public int Serial { get; set; }
        public string ProductName { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Unit { get; set; }
        public decimal Quantity { get; set; }
        public decimal AvilableQuatity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal ProductionCost { get; set; }
        public decimal TotalProductionCost { get; set; }
        public int RestaurantId { get; set; }
        public string DateTime { get; set; }
        public decimal? OpeningProduct { get; set; }
        public decimal InProduct { get; set; }
        public decimal TotalProduct { get; set; }
        public decimal SellProduct { get; set; }
        public decimal ClosingProduct { get; set; }

        //AvailableQuantity ForProduct Sold
        public decimal AvailableQuantity { get; set; }
       
    }
}