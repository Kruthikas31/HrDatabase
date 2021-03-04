using JARVIS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace JARVIS.Controllers
{
    public class OnboardController : Controller
    {
        // GET: Onboard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Onboard()
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
                        objs.Add(new TrackStatus { email = sdr["email"].ToString(), status = sdr["status"].ToString(), fname = sdr["firstName"].ToString(), mname = sdr["middleName"].ToString(), lname = sdr["lastName"].ToString(), id = Convert.ToInt32(sdr["id"]), joinning_date = (DateTime)sdr["joiningDate"], onboard = sdr["onboardDate"].ToString(), comments = sdr["comments"].ToString(), token = sdr["prospectiveEmpGuid"].ToString() });
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
        [HttpPost]
        public ActionResult savelist(string approve, string reject, string date_value, string id_for_date, string comments, string email, string name)
        {
            SqlConnection con = null;
            string sql_procedure;
            int i = 0;
            //con = new SqlConnection("Data Source=AA160917-BLR;Initial Catalog=c#_connect;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            con = new SqlConnection("Data Source=192.168.168.118;Initial Catalog=Cepheus_HRPortal_QC;Persist Security Info=True;User ID=cepheus_qc;Password=cepheus_qc@123");
            if (approve != null)
            {
                string[] approve_arr = approve.Split(',');
                string[] comments_arr = comments.Split('\n');
                string[] name_arr = name.Split(',');
                string[] email_arr = email.Split(',');
                string comment = string.Join(" ", comments_arr).Trim();


                foreach (var id in approve_arr)
                {
                    try
                    {
                        if (comment == "")
                        {
                            sql_procedure = "update EmployeeDetails set onboardDate=NULL,status='Approved',comments=NULL where id=" + id + "";
                        }
                        sql_procedure = "update EmployeeDetails set onboardDate=NULL,status='Approved',comments='" + comment + "' where id=" + id + "";
                        SqlCommand cm = new SqlCommand(sql_procedure, con);
                        con.Open();
                        int row = cm.ExecuteNonQuery();
                        con.Close();
                        //---------------------------------------------------------
                        string MailText = "";
                        string FilePath;
                        StreamReader str;
                        MailMessage mail = new MailMessage();
                        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                        mail.From = new MailAddress("myemailaddress@gmail.com");
                        mail.To.Add(email_arr[i]);
                        mail.Subject = "Update from Starmark";
                        mail.IsBodyHtml = true;

                        string emailTemplatePath = Server.MapPath("~/Views/EmailTemplates/Approve.cshtml");
                        str = new StreamReader(emailTemplatePath);



                        MailText = str.ReadToEnd();
                        str.Close();
                        mail.IsBodyHtml = true;
                        MailText = MailText.Replace("%name%", name_arr[i++]);
                        MailText = MailText.Replace("%Comment%", comment);



                        mail.Body = MailText;
                        SmtpServer.Port = 587;
                        SmtpServer.Credentials = new System.Net.NetworkCredential("hr.hrportal2021@gmail.com", "hrportal");
                        SmtpServer.EnableSsl = true;

                        SmtpServer.Send(mail);
                        //--------------------------------------------------

                    }
                    catch (Exception e)
                    {
                        return View("Index");
                    }
                }
                i = 0;
            }
            if (reject != null)
            {
                string[] reject_arr = reject.Split(',');
                string[] comments_arr = comments.Split('\n');
                string comment = string.Join(" ", comments_arr).Trim();
                string[] name_arr = name.Split(',');
                string[] email_arr = email.Split(',');
                foreach (var id in reject_arr)
                {
                    try
                    {
                        if (comment == "")
                        {
                            sql_procedure = "update EmployeeDetails set onboardDate=NULL,status='Rejected',comments=NULL where id=" + id + "";
                        }
                        sql_procedure = "update EmployeeDetails set onboardDate=NULL,status='Rejected',comments='" + comment + "' where id=" + id + "";
                        SqlCommand cm = new SqlCommand(sql_procedure, con);
                        con.Open();
                        int row = cm.ExecuteNonQuery();
                        string MailText = "";
                        string FilePath;
                        StreamReader str;
                        MailMessage mail = new MailMessage();
                        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                        mail.From = new MailAddress("myemailaddress@gmail.com");
                        mail.To.Add(email_arr[i]);
                        mail.Subject = "Update from Starmark";
                        mail.IsBodyHtml = true;


                        string emailTemplatePath = Server.MapPath("~/Views/EmailTemplates/Rejected.cshtml");
                        str = new StreamReader(emailTemplatePath);

                        MailText = str.ReadToEnd();
                        str.Close();
                        mail.IsBodyHtml = true;
                        MailText = MailText.Replace("%name%", name_arr[i++]);
                        MailText = MailText.Replace("%Comment%", comment);



                        mail.Body = MailText;
                        SmtpServer.Port = 587;
                        SmtpServer.Credentials = new System.Net.NetworkCredential("hr.hrportal2021@gmail.com", "hrportal");
                        SmtpServer.EnableSsl = true;

                        SmtpServer.Send(mail);
                        con.Close();
                    }
                    catch (Exception e)
                    {
                        return View("Index");
                    }

                }
                i = 0;
            }
            if (id_for_date != null)
            {
                string[] date_data_arr = date_value.Split(',');
                string[] dateid_arr = id_for_date.Split(',');
                var date = date_data_arr[0];
                var id = dateid_arr[0];
                DateTime date_formate = DateTime.Parse(date);
                //var d = date_formate.ToString("MM/dd/yyyy");
                try
                {
                    sql_procedure = "update EmployeeDetails set onboardDate='" + date_formate.ToString("MM/dd/yyyy") + "' where id=" + id + "";
                    SqlCommand cm = new SqlCommand(sql_procedure, con);
                    con.Open();
                    int row = cm.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception e)
                {
                    return View("Index");
                }
            }
            return RedirectToAction("Contact");
        }
    }
}