using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JARVIS.Models
{
    public class Admin
    {
        public int id { get; set; }
        public string ADCredential { get; set; }
        public string Role { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }
}