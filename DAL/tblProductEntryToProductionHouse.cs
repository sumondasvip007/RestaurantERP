//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblProductEntryToProductionHouse
    {
        public int Id { get; set; }
        public int ProductionHouseId { get; set; }
        public int ProductId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string Status { get; set; }
        public string RestuarentId { get; set; }
        public Nullable<int> Unit { get; set; }
        public Nullable<decimal> Quantity { get; set; }
    
        public virtual tblProductInformation tblProductInformation { get; set; }
        public virtual tblProductionHouseInformation tblProductionHouseInformation { get; set; }
    }
}
