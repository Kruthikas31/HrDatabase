using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace JARVIS.Models
{
    public class Employee
    {
        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public HttpPostedFileBase Allfiles
        {
            get;
            set;
        }
        public HttpPostedFileBase Marksheet
        {
            get;
            set;
        }
        public HttpPostedFileBase Aadhar_card
        {
            get;
            set;
        }
        public HttpPostedFileBase PAN_card
        {
            get;
            set;
        }
        public HttpPostedFileBase Passport
        {
            get;
            set;
        }
        public HttpPostedFileBase Bank_stmt
        {
            get;
            set;
        }
        public HttpPostedFileBase Revealing_letter
        {
            get;
            set;
        }
        public HttpPostedFileBase Offer_letter
        {
            get;
            set;
        }
        public HttpPostedFileBase Exp_certificate
        {
            get;
            set;
        }
        public HttpPostedFileBase Latest_payslip
        {
            get;
            set;
        }
        public HttpPostedFileBase Photo
        {
            get;
            set;
        }
        public string Comments
        {
            get;
            set;
        }
        public Nullable<System.DateTime> CreatedOn { get; set; }

        public bool[] isActive { get; set; }
        //============================================
        public int ID1 { get; set; }
        public string name1 { get; set; }
        public string allfiles1 { get; set; }
        public string marksheet1 { get; set; }
        public string aadhar_card1 { get; set; }
        public string pAN_card1 { get; set; }
        public string passport1 { get; set; }
        public string bank_stmt1 { get; set; }
        public string revealing_letter1 { get; set; }
        public string offer_letter1 { get; set; }
        public string exp_certificate1 { get; set; }
        public string latest_payslip1 { get; set; }
        public string photos1 { get; set; }


        //===========================================




    }
}
