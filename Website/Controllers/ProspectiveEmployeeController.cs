using JARVIS.DBServices;
using JARVIS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace JARVIS.Controllers
{
    public class ProspectiveEmployeeController : Controller
    {
        // GET: ProspectiveEmployee

        [HttpGet]
        public ActionResult Token(string token)
        {
            Session["token"] = token;
            return RedirectToAction("BasicInformation");
        }
        [HttpGet]
        public ActionResult Invite()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Invite(BasicInformation prospectiveEmployee,Salary salary)
        {
            log4net.ILog Log = log4net.LogManager.GetLogger(typeof(ProspectiveEmployeeController));
            try
             {
              
                Log.Info("Prospective controller !!!");
                prospectiveEmployee.prospectiveEmpGuid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                Log.Debug("Guid token:" + prospectiveEmployee.prospectiveEmpGuid);
                string MailText = "";
                string FilePath;
                StreamReader str;
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("myemailaddress@gmail.com");
                mail.To.Add(prospectiveEmployee.email);
                Log.Debug("To Mail Added");
                mail.Subject = "HR from Starmark Software Pvt. Ltd. requests you to sign Joining Documents";
                mail.IsBodyHtml = true;
                string emailTemplatePath = Server.MapPath("~/Views/EmailTemplates/Invite.cshtml");
                str = new StreamReader(emailTemplatePath);
                Log.Debug("Email Template Path Set");
                MailText = str.ReadToEnd();
                str.Close();
                mail.IsBodyHtml = true;
                MailText = MailText.Replace("%name%", prospectiveEmployee.firstName);
                MailText = MailText.Replace("%date%", prospectiveEmployee.joiningDate);
                MailText = MailText.Replace("%time%", prospectiveEmployee.reportingTime);
                MailText = MailText.Replace("%token%", prospectiveEmployee.prospectiveEmpGuid);
                MailText = MailText.Replace("%basicSalary%", salary.basicSalary);
                MailText = MailText.Replace("%nightShiftAllowances%", salary.nightShiftAllowances);
                MailText = MailText.Replace("%houseRentAllowances%", salary.houseRentAllowances);
                MailText = MailText.Replace("%travelAllowances%", salary.travelAllowances);
                MailText = MailText.Replace("%specialAllowances%", salary.specialAllowances);
                MailText = MailText.Replace("%companyPF%", salary.companyPF);
                MailText = MailText.Replace("%medicalInsurance%", salary.medicalInsurance);
                MailText = MailText.Replace("%ESI%", salary.ESI);
                MailText = MailText.Replace("%monthlyCTC%", salary.monthlyCTC);
                MailText = MailText.Replace("%monthlyGross%", salary.monthlyGrossSalary);
                MailText = MailText.Replace("%monthlyIncentive%", salary.monthlyIncentive);
                MailText = MailText.Replace("%annualCTC%", salary.annualCTC);
                MailText = MailText.Replace("%subtotal%", salary.subTotal);
                MailText = MailText.Replace("%gratuity%", salary.gratuity);

                mail.Body = MailText;
                SmtpServer.Port = 587;
                Log.Debug("Port Set to "+ SmtpServer.Port);
                SmtpServer.Credentials = new System.Net.NetworkCredential("hr.hrportal2021@gmail.com", "hrportal");
                Log.Debug("From Mail Set to" + SmtpServer.Credentials);
                SmtpServer.EnableSsl = true;
                Log.Debug("SSL Enabled: " + SmtpServer.EnableSsl);
                SmtpServer.Send(mail);
                Log.Debug("Mail Sent");
                
            }
            catch (Exception e)
            {
                Log.Debug("Exception: " + e);
            }
            
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                IDataMapper<BasicInformation, int> mapper = new BasicInformationDataMapper(connection);
                try
                {
                    //var httpContent = new
                    //{
                    //    toEmail = prospectiveEmployee.email,
                    //    subject = "HR from Starmark Software Pvt. Ltd. requests you to sign Joining Documents",
                    //    name = prospectiveEmployee.firstName,
                    //    joiningDate = prospectiveEmployee.joiningDate,
                    //    time = prospectiveEmployee.reportingTime,
                    //    action = "invite",
                    //    token = prospectiveEmployee.prospectiveEmpGuid
                    //};
                    //HttpResponseMessage httpResponseMessage = new HttpClient().PostAsync("https://localhost:44388/api/mail", httpContent, new JsonMediaTypeFormatter()).Result;

                    //if (httpResponseMessage.IsSuccessStatusCode && httpResponseMessage != null)
                    //{

                    //    int rowsAffected = mapper.InsertInfo(prospectiveEmployee);
                    //    return RedirectToAction("Tracking", "Tracking");

                    //    // if (Session["token"] == null)
                    //    //   return RedirectToAction("Index");
                    //}

                    //else
                    //{

                    //    return RedirectToAction("Tracking", "Tracking", "Error");
                    //}

                
                    int rowsAffected = mapper.InsertInfo(prospectiveEmployee);
                    Log.Debug("Insert Successful. Rows Affected: "+rowsAffected);
                    return RedirectToAction("Tracking", "Tracking");

                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine("exception null"+e);
                    Log.Debug("Exception: " + e);
                    return RedirectToAction("Tracking", "Tracking");
                }

              
            }

        }
        [HttpGet]
        public ActionResult BasicInformation(string token="")
        {
            token = (string)Session["token"];
            token = token.Replace(" ", "+");
            BasicInfoDependent basicInfoDependent = new BasicInfoDependent();
            ViewBag.token = token;
            int id = 0;
            BasicInformationDataMapper basicInformationMapper;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                basicInformationMapper = new BasicInformationDataMapper(connection);
                id = basicInformationMapper.SelectId(token);

            }
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                basicInformationMapper = new BasicInformationDataMapper(connection);
                basicInfoDependent.basicInformation = basicInformationMapper.SelectInfo(id)[0];
            }
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {

                IDataMapper<DependentDetails, DependentDetails> dependentDetailsMapper = new DependentDetailsDataMapper(connection);
                basicInfoDependent.dependentDetails = dependentDetailsMapper.SelectInfo(id);
            }
            return View(basicInfoDependent);

        }

        

        [HttpPost]
        public ActionResult BasicInformation(BasicInformation prospectiveEmployeeBasicInfo, string save, string saveAndNext, string cancel)
        {
            Session["data"] = prospectiveEmployeeBasicInfo.firstName;
            prospectiveEmployeeBasicInfo.prospectiveEmpGuid = prospectiveEmployeeBasicInfo.prospectiveEmpGuid.Replace(" ", "+");
            int id = 0;
            BasicInformationDataMapper mapper;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                mapper = new BasicInformationDataMapper(connection);
                id = mapper.SelectId(prospectiveEmployeeBasicInfo.prospectiveEmpGuid);

            }
            if (save == "save")
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    mapper = new BasicInformationDataMapper(connection);
                    int rowsAffected = mapper.UpdateInfo(prospectiveEmployeeBasicInfo, id);
                    //  return RedirectToAction("BasicInformation", new { token = prospectiveEmployeeBasicInfo.prospectiveEmpGuid });
                    return RedirectToAction("BasicInformation");
                }
            }
            if (saveAndNext == "saveAndNext")
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    mapper = new BasicInformationDataMapper(connection);
                    int rowsAffected = mapper.UpdateInfo(prospectiveEmployeeBasicInfo, id);
                    //return RedirectToAction("EducationExperience", new { token = prospectiveEmployeeBasicInfo.prospectiveEmpGuid });
                    return RedirectToAction("EducationExperience");
                }
            }
            return View("Failure");

        }
        [HttpGet]
        public ActionResult EducationExperience(string token="")
        {
            token = (string)Session["token"];
            token = token.Replace(" ", "+");
            ViewBag.token = token;
            EducationExperience educationExperience = new EducationExperience();
            int id = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                BasicInformationDataMapper mapper = new BasicInformationDataMapper(connection);
                id = mapper.SelectId(token);

            }
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                IDataMapper<EducationDetails, EducationDetails> educationMapper = new EducationDetailsDataMapper(connection);

                educationExperience.educationDetails = educationMapper.SelectInfo(id);

            }
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                IDataMapper<ExperienceDetails, ExperienceDetails> experienceMapper = new ExperienceDetailsDataMapper(connection);
                educationExperience.experienceDetails = experienceMapper.SelectInfo(id);
            }

            return View(educationExperience);


        }

        [HttpPost]
        public void DependentDetails(DependentDetails dependent, string token="")
        {
            token = (string)Session["token"];
            token = token.Replace(" ", "+");
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                BasicInformationDataMapper mapper = new BasicInformationDataMapper(connection);
                dependent.id = mapper.SelectId(token);

            }
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {

                IDataMapper<DependentDetails, DependentDetails> mapper = new DependentDetailsDataMapper(connection);
                int rowsAffected;
                rowsAffected = mapper.InsertInfo(dependent);

            }

        }
        [HttpPut]
        public void DependentDetails(DependentDetails[] dependent, string token="")
        {
            token = (string)Session["token"];
            token = token.Replace(" ", "+");
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                BasicInformationDataMapper mapper = new BasicInformationDataMapper(connection);
                dependent[0].id = mapper.SelectId(token);

            }
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                IDataMapper<DependentDetails, DependentDetails> mapper = new DependentDetailsDataMapper(connection);
                int rowsAffected;
                rowsAffected = mapper.UpdateInfo(dependent[0], dependent[1]);

            }

        }

        [HttpPost]
        public void EducationDetails(EducationDetails education, string token="")
        {
            token = (string)Session["token"];
            token = token.Replace(" ", "+");
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                BasicInformationDataMapper mapper = new BasicInformationDataMapper(connection);
                education.id = mapper.SelectId(token);

            }
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                IDataMapper<EducationDetails, EducationDetails> mapper = new EducationDetailsDataMapper(connection);
                int rowsAffected;
                rowsAffected = mapper.InsertInfo(education);
            }

        }
        [HttpPut]
        public void EducationDetails(EducationDetails[] education, string token="")
        {
            token = (string)Session["token"];
            token = token.Replace(" ", "+");
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                BasicInformationDataMapper mapper = new BasicInformationDataMapper(connection);
                education[0].id = mapper.SelectId(token);

            }
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                IDataMapper<EducationDetails, EducationDetails> mapper = new EducationDetailsDataMapper(connection);
                int rowsAffected;
                rowsAffected = mapper.UpdateInfo(education[0], education[1]);
            }

        }

        [HttpPost]
        public void ExperienceDetails(ExperienceDetails experience, string token="")
        {
            token = (string)Session["token"];
            token = token.Replace(" ", "+");
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                BasicInformationDataMapper mapper = new BasicInformationDataMapper(connection);
                experience.id = mapper.SelectId(token);

            }
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                IDataMapper<ExperienceDetails, ExperienceDetails> mapper = new ExperienceDetailsDataMapper(connection);
                int rowsAffected;
                rowsAffected = mapper.InsertInfo(experience);
            }
        }


        [HttpPut]
        public void ExperienceDetails(ExperienceDetails[] experience, string token="")
        {
            token = (string)Session["token"];
            token = token.Replace(" ", "+");
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                BasicInformationDataMapper mapper = new BasicInformationDataMapper(connection);
                experience[0].id = mapper.SelectId(token);
            }
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                IDataMapper<ExperienceDetails, ExperienceDetails> mapper = new ExperienceDetailsDataMapper(connection);
                int rowsAffected;
                rowsAffected = mapper.UpdateInfo(experience[0], experience[1]);
            }
        }

        [HttpGet]
        public ActionResult StatusUpdate(string token="")
        {
            token = (string)Session["token"];
            token = token.Replace(" ", "+");
            int id = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                BasicInformationDataMapper mapper = new BasicInformationDataMapper(connection);
                id = mapper.SelectId(token);
            }
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                BasicInformationDataMapper mapper = new BasicInformationDataMapper(connection);

                int rowsAffected;
                rowsAffected = mapper.UpdateStatus(id);
                return View("Success");
            }
        }
    }
}