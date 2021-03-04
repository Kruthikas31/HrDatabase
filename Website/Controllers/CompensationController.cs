using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JARVIS.Controllers
{
    public class CompensationController : Controller
    {
        // GET: Compensation
        public ActionResult BankDetails()
        {
            return View("BankDetails");
        }
        public ActionResult Payslip()
        {
            return View("Payslip");
        }
        public ActionResult ITDeclaration()
        {
            return View("ITDeclaration");
        }
    }
}