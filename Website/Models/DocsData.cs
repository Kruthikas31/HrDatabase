using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace uploadDocsPrepopulate.Models
{
    public class DocsData
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
    }
}
