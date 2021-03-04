using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JARVIS.Models
{
    public class Salary
    {
        public string basicSalary { get; set; }
        public string houseRentAllowances { get; set; }
        public string travelAllowances { get; set; }
        public string nightShiftAllowances { get; set; }
        public string specialAllowances { get; set; }
        public string monthlyGrossSalary { get; set; }
        public string companyPF { get; set; }
        public string ESI { get; set; }
        public string monthlyIncentive { get; set; }
        public string medicalInsurance { get; set; }
        public string gratuity { get; set; }
        public string subTotal { get; set; }
        public string monthlyCTC { get; set; }
        public string annualCTC { get; set; }
    }
}