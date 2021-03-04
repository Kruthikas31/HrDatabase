using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JARVIS.Models
{
    public class BankDetails
    {
        public string accNumber { get; set; }
        public string name { get; set; }
        public string ifsc { get; set; }
        public string branchDetails { get; set; }
    }
}