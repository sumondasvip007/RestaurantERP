using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Models.Enam
{
    [Flags]
    public enum ProductType
    {
        // The flag for  is 2.
        SellTypeProduct = 0x02,
        // The flag for  is 1.
        PurchaseTypeProduct = 0x01,
        // The flag for  is 3
        PurchaseAndSellTypeProduct = 0x03,



    }
}