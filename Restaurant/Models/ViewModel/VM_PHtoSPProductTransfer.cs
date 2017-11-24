using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Models.ViewModel
{
    public class VM_PHtoSPProductTransfer
    {
        //public int SPId { get; set; }
        public int SL { get; set; }
        public string RestorentName { get; set; }
        public string RestorentAddress { get; set; }
        public int ProductId { get; set; }
        public string ProductName { set; get; }
        public string Unit { get; set; }
        public double Quantity { get; set; }
        public string SellsPointName { get; set; }
        //public int AvaliableQuatity { get; set; }
        public decimal AvaliableQuatity { get; set; }
        public string ProductionHouseStoreName { get; set; }
        public bool IsSelected { get; set; }
    }
}