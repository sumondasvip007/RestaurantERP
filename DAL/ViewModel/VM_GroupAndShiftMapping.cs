using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
   public class VM_GroupAndShiftMapping
    {
        public int GroupMappingId { get; set; }
        public DateTime TransferDate { get; set; }
        public string Date { get; set; }
        public string Day { get; set; }
        public string Night { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int ShiftId { get; set; }
        public string ShiftName { get; set; }
    }
}
