using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JARVIS.Models
{
    public class email
    {
        public string comments { get; set; }
        public string toEmail { get; set; }
        public string subject { get; set; }
        public string name { get; set; }
        public string joiningDate { get; set; }
        public string reportingTime { get; set; }
        public string action { get; set; }
        public string token { get; set; }
    }
}