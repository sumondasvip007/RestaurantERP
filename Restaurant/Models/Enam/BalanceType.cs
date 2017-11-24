using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Models.Enam
{
 [Flags]
    public enum BalanceType
    {
        // The flag for SunRoof is 2.
        Credit = 0x02,
        // The flag for Spoiler is 1.
        Debit = 0x01,
        

    }
}
