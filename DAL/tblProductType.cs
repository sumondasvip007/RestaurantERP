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
    
    public partial class tblProductType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblProductType()
        {
            this.tblProductInformations = new HashSet<tblProductInformation>();
        }
    
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public Nullable<int> RestaurantId { get; set; }
        public string CreatedBy { get; set; }
        public string EditedBy { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public Nullable<System.DateTime> EditedDateTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblProductInformation> tblProductInformations { get; set; }
    }
}