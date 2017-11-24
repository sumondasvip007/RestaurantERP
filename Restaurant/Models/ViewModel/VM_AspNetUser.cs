using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Models.ViewModel
{
    public class VM_AspNetUser
    {
        public string Id { get; set; }
        public string UserFullName { get; set; }
        public string UserName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}