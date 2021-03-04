using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JARVIS.Models
{
    public class BasicInformation
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public DateTime? dob { get; set; }
        public string gender { get; set; }
        public string bloodGroup { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string emergencyNumber { get; set; }
        public string PAN { get; set; }
        public string ESI { get; set; }
        public string UAN { get; set; }
        public string aadhar { get; set; }
        public string maritalStatus { get; set; }
        public string passportNumber { get; set; }
        public DateTime passportExpiryDate { get; set; }
        public string nationality { get; set; }
        public string streetName { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string pincode { get; set; }
        public string country { get; set; }
        public BankDetails bankDetails { get; set; }
        public string level { get; set; }
        public string pSameAsc { get; set; }
        public string pStreetName { get; set; }
        public string pCity { get; set; }
        public string pState { get; set; }
        public string pPincode { get; set; }
        public string pCountry { get; set; }
        public string role { get; set; }
        public string ctc { get; set; }
        public string joiningDate { get; set; }
        public string approvedManager { get; set; }
        public string reportingManager { get; set; }
        public string reportingTime { get; set; }
        public string prospectiveEmpGuid { get; set; }
        public string monthlyGross { get; set; }
        public string monthlyIncentive { get; set; }
        public string nightShiftAllowance { get; set; }

    }
}