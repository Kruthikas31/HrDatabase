using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JARVIS.Models
{
    public class EducationDetails
    {
        public int id { get; set; }
        public string degree { get; set; }
        public string specialization { get; set; }
        public string institute { get; set; }
        public string yearOfPassing { get; set; }
    }
}