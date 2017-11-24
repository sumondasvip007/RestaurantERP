using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Models.ViewModel
{
    public class VM_AccVoucher
    {
        public int VoucherID { get; set; }
        public string VoucherName { get; set; }
        public string TransactionDate { get; set; }
        public string Narration { get; set; }
        
        public int VTypeID { get; set; }
        public string VNumber { get; set; }
        public int OCode { get; set; }
        public string ActionName { get; set; }
        public int RestaurantId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDateTime { get; set; }
        public string EditedBy { get; set; }
        public string EditedDateTime { get; set; }
        public string VoucherDetailID { get; set; }
        public string LedgerID { get; set; }
        public string LedgerName { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public string ChequeNumber { get; set; }
        public string DrCrButton { get; set; }
        public bool DrTextBox { get; set; }
        public bool CrTextBox { get; set; }
    }
}