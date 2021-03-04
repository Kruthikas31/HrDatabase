using JARVIS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace JARVIS.Controllers
{
    public class mailController : ApiController
    {
        public IHttpActionResult Post(email m)
        {
            string MailText = "";
            string FilePath;
            StreamReader str;
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("myemailaddress@gmail.com");
            mail.To.Add(m.toEmail);
            mail.Subject = m.subject;
            mail.IsBodyHtml = true;

            if (m.action == "invite")
            {
                FilePath = "D:/HR PORTAL 1/HRPortal/Website/Views/EmailTemplates/Invite.cshtml";
                str = new StreamReader(FilePath);
                MailText = str.ReadToEnd();
                str.Close();
                mail.IsBodyHtml = true;
                MailText = MailText.Replace("%name%", m.name);
                MailText = MailText.Replace("%date%", m.joiningDate);
                MailText = MailText.Replace("%time%", m.reportingTime);
                MailText = MailText.Replace("%token%", m.token);
            }

            mail.Body = MailText;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("gangaadharsngowdru123@gmail.com", "nsrg##1998");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

            return Ok();




        }

    }
}
