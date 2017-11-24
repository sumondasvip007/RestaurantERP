using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Models.ViewModel
{
    public class VM_AddLedger
    {
        public string LedgerName { get; set; }
        public string LedgerCode { get; set; }
        public int GroupID { get; set; }
        public string BalanceType { get; set; }
        public int InitialBalance { get; set; }
        public string Comment { get; set; }
      
     
    }
}