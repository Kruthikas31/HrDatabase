using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JARVIS.Models
{
    public class ExperienceDetails
    {
        public int id { get; set; }
        public string companyName { get; set; }
        public string role { get; set; }
        public string pastSalary { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public string yearsOfExperience { get; set; }
    }
}