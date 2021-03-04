using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JARVIS.Models
{
    public class DependentDetails
    {
        public int id { get; set; }
        public string depName { get; set; }
        public string relation { get; set; }
        public string gender { get; set; }
        public DateTime dob { get; set; }
    }
}