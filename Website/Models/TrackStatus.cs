using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JARVIS.Models
{
    public class TrackStatus
    {
        public string email { get; set; }
        public string status { get; set; }
        public string fname { get; set; }
        public string mname { get; set; }
        public string lname { get; set; }
        public int id { get; set; }
        public int[] count { get; set; }
        public string onboard { get; set; }
        public DateTime joinning_date;
        public string comments { get; set; }
        public string token { get; set; }

    }
}