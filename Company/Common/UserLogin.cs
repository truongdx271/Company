using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Common
{
    public class UserLogin
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string Name { set; get; }
        public int? Role { get; set; }
    }
}