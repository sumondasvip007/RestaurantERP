using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
   public  class VM_Chart
    {
       public string Day { get; set; }       
       public string DayOfWeek { get; set; }       
       public int? DayForMonth { get; set; }      
       public decimal Value { get; set; }
    }
}
