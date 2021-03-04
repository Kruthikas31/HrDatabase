using JARVIS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JARVIS.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Users()
        {
            var model = new List<Admin>();
            try
            {
                SqlConnection con = null;


                //con = new SqlConnection("Data Source=AA160917-BLR;Initial Catalog=c#_connect;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                con = new SqlConnection("Data Source=192.168.168.118;Initial Catalog=Cepheus_HRPortal_QC;Persist Security Info=True;User ID=cepheus_qc;Password=cepheus_qc@123");
                string sql_procedure = "select * from Admin";
                SqlCommand cm = new SqlCommand(sql_procedure, con);
                con.Open();
                SqlDataReader sdr = cm.ExecuteReader();

                while (sdr.Read())
                {
                    var role = new Admin();
                    //objs.Add(new Admin_role { Emp_id = (int)sdr["Emp_id"], AD_Credential = sdr["AD_Credential"].ToString(), Role = sdr["Role"].ToString() });
                    role.id = (int)sdr["id"];
                    role.ADCredential = sdr["ADCredential"].ToString();
                    role.Role = sdr["Role"].ToString();
                    model.Add(role);
                }


            }
            catch (Exception e)
            {
                var ErrorString = e.Message;
            }
            return View(model);

        }

        [HttpPost]
        public void Insert(Admin item)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=192.168.168.118;Initial Catalog=Cepheus_HRPortal_QC;Persist Security Info=True;User ID=cepheus_qc;Password=cepheus_qc@123");
                con.Open();
                string q = "Admininsert";
                SqlCommand com = new SqlCommand(q, con);
                com.CommandType = CommandType.StoredProcedure;
                SqlParameter param;
                param = com.Parameters.Add("@id", SqlDbType.Int, 10);
                param.Value = item.id;
                param = com.Parameters.Add("@ADcredential", SqlDbType.VarChar, 30);
                param.Value = item.ADCredential;
                param = com.Parameters.Add("@Role", SqlDbType.VarChar, 20);
                param.Value = item.Role;
                //param = com.Parameters.Add("@Createdon", SqlDbType.DateTime, 20);
                // param.Value = item.CreatedOn;
                //param = com.Parameters.Add("@Password", SqlDbType.VarChar, 40);
                //param.Value = add.Password;
                int rowsAffected = com.ExecuteNonQuery();
                ViewBag.Message = String.Format("Job Role Added Successfully ");
                con.Close();




                //PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "ad.starmarkit.com");
                //UserPrincipal userPrinciple = UserPrincipal.FindByIdentity(ctx, item.ADCredential);
                //var email = userPrinciple.EmailAddress;

                ////----------------------------------
                //string MailText = "";
                //string FilePath;
                //StreamReader str;
                //MailMessage mail = new MailMessage();
                //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                //mail.From = new MailAddress("myemailaddress@gmail.com");
                ////string emailtem = "sreesvrk@gmail.com";
                ////mail.To.Add(email);
                //mail.Subject = "Update from Starmark";
                //mail.IsBodyHtml = true;

                //string emailTemplatePath = Server.MapPath("~/Views/EmailTemplates/RoleInvite.cshtml");
                //str = new StreamReader(emailTemplatePath);



                //MailText = str.ReadToEnd();
                //str.Close();
                //mail.IsBodyHtml = true;
                //MailText = MailText.Replace("%name%", item.ADCredential);
                //MailText = MailText.Replace("%Role%", item.Role);


                //mail.Body = MailText;
                //SmtpServer.Port = 587;
                //SmtpServer.Credentials = new System.Net.NetworkCredential("sagar.rangashamaiah@starmarkit.com", "Sachin@1998");
                //SmtpServer.EnableSsl = true;

                //SmtpServer.Send(mail);
                // --------------------------------

            }
            catch (Exception e)
            {
                var ErrorString = e.Message;
            }

        }

        [HttpPost]
        public void Update(Admin New)
        {
            try
            {
                SqlConnection con = new SqlConnection("Data Source=192.168.168.118;Initial Catalog=Cepheus_HRPortal_QC;Persist Security Info=True;User ID=cepheus_qc;Password=cepheus_qc@123");
                con.Open();
                string q = "Adminupdate";
                SqlCommand com = new SqlCommand(q, con);
                com.CommandType = CommandType.StoredProcedure;
                SqlParameter param;
                param = com.Parameters.Add("@id", SqlDbType.Int, 10);
                param.Value = New.id;
                param = com.Parameters.Add("@ADCredential", SqlDbType.VarChar, 30);
                param.Value = New.ADCredential;
                param = com.Parameters.Add("@Role", SqlDbType.VarChar, 20);
                param.Value = New.Role;
                //param = com.Parameters.Add("@Password", SqlDbType.VarChar, 40);
                //param.Value = add.Password;
                int rowsAffected = com.ExecuteNonQuery();

                con.Close();



                //----------------------------------
                //string MailText = "";
                //string FilePath;
                //StreamReader str;
                //MailMessage mail = new MailMessage();
                //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                //mail.From = new MailAddress("myemailaddress@gmail.com");
                //string emailtem = "sreesvrk@gmail.com";
                //mail.To.Add(emailtem);
                //mail.Subject = "Update from Starmark";
                //mail.IsBodyHtml = true;

                //string emailTemplatePath = Server.MapPath("~/Views/EmailTemplates/RoleInvite.cshtml");
                //str = new StreamReader(emailTemplatePath);



                //MailText = str.ReadToEnd();
                //str.Close();
                //mail.IsBodyHtml = true;
                //MailText = MailText.Replace("%name%", New.ADCredential);
                //MailText = MailText.Replace("%Role%", New.Role);



                //mail.Body = MailText;
                //SmtpServer.Port = 587;
                //SmtpServer.Credentials = new System.Net.NetworkCredential("hr.hrportal2021@gmail.com", "hrportal");
                //SmtpServer.EnableSsl = true;

                //SmtpServer.Send(mail);
                //--------------------------------

            }


            catch (Exception e)
            {
                var ErrorString = e.Message;
            }
        }
        [HttpPost]
        public void Delete(string id)
        {
            try
            {
                string[] idsplit = id.Split(',');
                SqlConnection con = new SqlConnection("Data Source=192.168.168.118;Initial Catalog=Cepheus_HRPortal_QC;Persist Security Info=True;User ID=cepheus_qc;Password=cepheus_qc@123");
                con.Open();
                string q = "Admindelete";
                SqlCommand com = new SqlCommand(q, con);
                com.CommandType = CommandType.StoredProcedure;
                SqlParameter param;
                param = com.Parameters.Add("@id", SqlDbType.Int, 10);
                foreach (var i in idsplit)
                {
                    int a = Int32.Parse(i);
                    param.Value = a;
                    int row = com.ExecuteNonQuery();
                }
                ViewBag.delete = String.Format("Row Deleted Successfully ");
                con.Close();

            }
            catch (Exception e)
            {
                var ErrorString = e.Message;
            }
        }

    }
}