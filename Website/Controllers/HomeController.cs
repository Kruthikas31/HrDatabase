using JARVIS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace JARVIS.Controllers
{
    class ForLogRead
    {
        public static string readLogFile()
        {
            string glog=null;
            try
            {
               
                StreamReader sr = new StreamReader(@"\\192.168.168.28\cepheusweb\Website-IIS\Cepheus_HRPortal_QC\jarvislog.log");
                glog = sr.ReadToEnd();
                sr.Close();

            }
            catch(Exception e)
            {

            }
    
            
            return glog;

        }
    }
    public class HomeController : Controller
    {
        //url/home/logs
        // To maintain log this view is added 
        public ActionResult logs()
        {
            string gg=null;
            try
            {
               gg = ForLogRead.readLogFile();

            }
            catch(Exception e)
            {

            }
            return View(gg);
        }


        // TO write log create Ilog object
        log4net.ILog Log = log4net.LogManager.GetLogger(typeof(HomeController));

        public ActionResult Index()
        {
            //Log.Info("Sagar home:" + 12);
            return View();
        }
        [HttpPost]

        public ActionResult Index(Model user)
        {
          //  Log.Info("Sagar home:" + 12);
            bool validation;
            string name = user.UserName;
            string password = user.Password;
            try
            {
                LdapConnection ldc = new LdapConnection(new LdapDirectoryIdentifier((string)null, false, false));
                NetworkCredential nc = new NetworkCredential(name, password, "ad.starmarkit.com");
                ldc.Credential = nc;
                ldc.AuthType = AuthType.Negotiate;
                ldc.Bind(nc); // user has authenticated at this point, as the credentials were used to login to the dc.
                validation = true;
                PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "ad.starmarkit.com");
                UserPrincipal userPrinciple = UserPrincipal.FindByIdentity(ctx, name);
                user.FullName = userPrinciple.DisplayName;
                Session["data"] = userPrinciple.GivenName;

                //     string emailId = userPrinciple.EmailAddress ;
                user.EmailId = userPrinciple.EmailAddress;
            }
            catch (LdapException)
            {
                validation = false;
            }
            if (!validation)
            {
                ViewBag.error = String.Format("Invalid User name or Password");
                return View("Index");
            }
            else                    //Valid User
            {

                Session["EmailId"] = user.EmailId;
                Session["UserName"] = user.FullName;
                SqlConnection con = new SqlConnection("Data Source=192.168.168.118;Initial Catalog=Cepheus_HRPortal_QC;Persist Security Info=True;User ID=cepheus_qc;Password=cepheus_qc@123");
                //SqlCommand cmd = new SqlCommand("select Role from AdminPortal where AD_credentials='" + name + "'", con);
                con.Open();
                string Role = "JobRole";
                SqlCommand com = new SqlCommand(Role, con);
                com.CommandType = CommandType.StoredProcedure;
                SqlParameter param;
                param = com.Parameters.Add("@ADcredential", SqlDbType.VarChar, 30);
                param.Value = name;
                SqlDataReader rdr = com.ExecuteReader();
                Session["token"] = rdr;
                // While Loop Read the first column from each row of result set which is employee name   
                while (rdr.Read())
                {
                    var job = rdr.GetString(0);
                    Session["role"] = job;
                    if (job.Equals("Admin"))
                    {
                        ViewBag.admin = String.Format("this is admin");
                        return View("Welcome");
                    }
                    if (job.Equals("HR"))
                    {
                        ViewBag.hr = String.Format("this is hr");
                        return View("Welcome");
                    }
                }
                rdr.Close();
                con.Close();
                ViewBag.emp = String.Format("this is employee");
                return View("Welcome");
            }
        }
        

        //public ActionResult Admin(Admin add)
        //{
        //    add.CreatedOn = DateTime.Now;
        //    SqlConnection con = new SqlConnection("Data Source=192.168.168.118;Initial Catalog=Cepheus_HRPortal_QC;Persist Security Info=True;User ID=cepheus_qc;Password=cepheus_qc@123");
        //    con.Open();
        //    string q = "Admin_Insert";
        //    SqlCommand com = new SqlCommand(q, con);
        //    com.CommandType = CommandType.StoredProcedure;
        //    SqlParameter param;
        //    param = com.Parameters.Add("@Emp_id", SqlDbType.Int, 10);
        //    param.Value = add.Emp_id;
        //    param = com.Parameters.Add("@AD_credentials", SqlDbType.VarChar, 30);
        //    param.Value = add.AD_Credential;
        //    param = com.Parameters.Add("@Role", SqlDbType.VarChar, 20);
        //    param.Value = add.Role;
        //    param = com.Parameters.Add("@Createdon", SqlDbType.DateTime, 20);
        //    param.Value = add.CreatedOn;
        //    //param = com.Parameters.Add("@Password", SqlDbType.VarChar, 40);
        //    //param.Value = add.Password;
        //    com.ExecuteNonQuery();
        //    ViewBag.Message = String.Format("Job Role Added Successfully ");
        //    con.Close();
        //    return View("Welcome");
        //}

        public ActionResult Welcome()
        {
            if (Session["token"] != null)
            {
                return View();
            }
            
            return View("Index");
        }
        public ActionResult Admin()
        {
            if (Session["token"] != null)
            {
                return View();
            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            //Tracking username
            //Session["name"] =null;

            //FormsAuthentication.SignOut();
            //Session.Clear();
            //Session.RemoveAll();
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            FormsAuthentication.SignOut();
            


            this.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            this.Response.Cache.SetNoStore();

            //This is used to clear all session id .Reset to null
            return RedirectToAction("Index", "Home");
        }
    }
}