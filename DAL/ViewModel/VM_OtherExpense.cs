using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
   public class VM_OtherExpense
    {
        public int ExpenseId { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int ShiftId { get; set; }
        public string ShiftName { get; set; }
        public DateTime TransferDate { get; set; }
        public decimal Less { get; set; }
        public decimal Due { get; set; }

        public decimal Compliment { get; set; }
        public decimal Damage { get; set; }
    }
}
