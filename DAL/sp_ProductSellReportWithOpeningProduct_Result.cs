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
    
    public partial class sp_ProductSellReportWithOpeningProduct_Result
    {
        public Nullable<int> ProductId { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> OpeningQuantity { get; set; }
        public Nullable<decimal> InProductQuantitySelectDate { get; set; }
        public Nullable<decimal> SellProductQuantitySelectDate { get; set; }
    }
}