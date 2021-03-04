using JARVIS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JARVIS.Controllers
{
    public class TrackingController : Controller
    {
        // GET: onboard
        public ActionResult Index()
        {
            return RedirectToAction("Tracking");
        }

        public ActionResult Tracking()
        {
            SqlConnection con = null;
            try
            {
                List<TrackStatus> objs = new List<TrackStatus>();
                //con = new SqlConnection("Data Source=AA160917-BLR;Initial Catalog=c#_connect;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                con = new SqlConnection("Data Source=192.168.168.118;Initial Catalog=Cepheus_HRPortal_QC;Persist Security Info=True;User ID=cepheus_qc;Password=cepheus_qc@123");
                string sql_procedure = "select * from EmployeeDetails";
                SqlCommand cm = new SqlCommand(sql_procedure, con);
                con.Open();
                SqlDataReader sdr = cm.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        objs.Add(new TrackStatus { email = sdr["email"].ToString(), status = sdr["status"].ToString(), fname = sdr["firstName"].ToString(), mname = sdr["middleName"].ToString(), lname = sdr["lastName"].ToString(), id = Convert.ToInt32(sdr["id"]), joinning_date = (DateTime)sdr["joiningDate"], onboard = sdr["onboardDate"].ToString(), comments = sdr["comments"].ToString() });
                    }
                    return View(objs);
                }
                else
                {
                    ViewBag.Message = String.Format("The user name or password is incorrect");
                    return View(objs);
                }
            }
            catch (Exception e)
            {
                return View("Index");
            }
            finally
            {
                con.Close();
            }


        }

    }
}
