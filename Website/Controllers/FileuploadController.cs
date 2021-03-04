using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JARVIS.Models;
using System.IO;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace JARVIS.Controllers
{
    public class FileuploadController : Controller
    {










        // GET: /FileUpload/  
        [HttpGet]
        public ActionResult Upload()
        {
            List<Employee> files = new List<Employee>();
            // Data Source = AA171246 - BLR; Initial Catalog = usersDb; Integrated Security = True; MultipleActiveResultSets = True; Application Name = EntityFramework
            // Data Source = 192.168.168.118; Initial Catalog = Cepheus_HRPortal_QC; Persist Security Info = True; User ID = cepheus_qc; Password = cepheus_qc@123
            using (SqlConnection con = new SqlConnection("Data Source = 192.168.168.118; Initial Catalog = Cepheus_HRPortal_QC; Persist Security Info = True; User ID = cepheus_qc; Password = cepheus_qc@123"))
            {
                string query = "prepopulatestrprocedure";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.AddWithValue("@token", Session["token"]);

                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {


                                Employee data = new Employee
                                {
                                    ID1 = Convert.ToInt32(sdr["id"]),
                                    name1 = sdr["name"].ToString(),
                                    allfiles1 = sdr["allfiles"].ToString(),
                                    marksheet1 = sdr["marksheet"].ToString(),
                                    aadhar_card1 = sdr["aadharCard"].ToString(),
                                    pAN_card1 = sdr["panCard"].ToString(),
                                    passport1 = sdr["passport"].ToString(),
                                    bank_stmt1 = sdr["bankStmt"].ToString(),
                                    revealing_letter1 = sdr["revealingLetter"].ToString(),
                                    offer_letter1 = sdr["offerLetter"].ToString(),
                                    exp_certificate1 = sdr["expCertificate"].ToString(),
                                    latest_payslip1 = sdr["latestPayslip"].ToString(),
                                    photos1 = sdr["photo"].ToString(),
                                    Comments = sdr["Comments"].ToString(),
                                    CreatedOn = Convert.ToDateTime(sdr["createdOn"])
                                };
                                ViewBag.Message = data;
                            }

                        }
                        else
                        {
                            ViewBag.Message = null;
                        }

                    }
                    con.Close();
                }
            }

            return View();
        }




        public ActionResult Errorpage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(Employee employee, string save, string saveAndNext)
        {
            System.IO.Directory.CreateDirectory(Server.MapPath("~/uploadfiles/" + Session["data"]));
            int id = 0;
            var documents = new uploadDoc()
            {

                Name = (string)Session["data"],
                Allfiles = SaveToPhysicalLocation(employee.Allfiles, false),
                Marksheet = SaveToPhysicalLocation(employee.Marksheet, employee.isActive[0]),
                Aadhar_card = SaveToPhysicalLocation(employee.Aadhar_card, employee.isActive[1]),
                PAN_card = SaveToPhysicalLocation(employee.PAN_card, employee.isActive[2]),
                Passport = SaveToPhysicalLocation(employee.Passport, employee.isActive[3]),
                Bank_stmt = SaveToPhysicalLocation(employee.Bank_stmt, employee.isActive[4]),
                Revealing_letter = SaveToPhysicalLocation(employee.Revealing_letter, employee.isActive[5]),
                Offer_letter = SaveToPhysicalLocation(employee.Offer_letter, employee.isActive[6]),
                Exp_certificate = SaveToPhysicalLocation(employee.Exp_certificate, employee.isActive[7]),
                Latest_payslip = SaveToPhysicalLocation(employee.Latest_payslip, employee.isActive[8]),
                Photo = SaveToPhysicalLocation(employee.Photo, employee.isActive[9]),
                Comments = employee.Comments,
                CreatedOn = DateTime.Now
            };

            string SaveToPhysicalLocation(HttpPostedFileBase file, bool? isActive)
            {

                if (file != null && file.ContentLength > 0 && isActive != true)
                {



                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/uploadfiles/" + Session["data"]), fileName);
                    file.SaveAs(path);

                    return fileName;
                }
                else if (file != null && isActive == true)
                {


                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/uploadfiles/" + Session["data"]), fileName);
                    file.SaveAs(path);

                    return fileName;
                }
                else if (file == null && isActive == true)
                {
                    return "This File is in All File column(MainFile).";
                }
                else
                {
                    return "null";
                }

            }

            //string constr = "Data Source = AA171246 - BLR; Initial Catalog = usersDb; Integrated Security = True";
            using (SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                string fetchId = "fetchidfromemp";
                using (SqlCommand cmd = new SqlCommand(fetchId, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.AddWithValue("@token", Session["token"]);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            id = Convert.ToInt32(sdr["id"]);
                        }
                    }
                    con.Close();
                }
                string query = "uploadDocstrprocedure";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param;
                    param = cmd.Parameters.AddWithValue("@id", id);
                    param = cmd.Parameters.AddWithValue("@Name", Session["data"]);

                    param = cmd.Parameters.AddWithValue("@Allfiles", documents.Allfiles);
                    param = cmd.Parameters.AddWithValue("@Marksheet", documents.Marksheet);
                    param = cmd.Parameters.AddWithValue("@Aadhar_card", documents.Aadhar_card);
                    param = cmd.Parameters.AddWithValue("@PAN_card", documents.PAN_card);
                    param = cmd.Parameters.AddWithValue("@Passport", documents.Passport);
                    param = cmd.Parameters.AddWithValue("@Bank_stmt", documents.Bank_stmt);
                    param = cmd.Parameters.AddWithValue("@Revealing_letter", documents.Revealing_letter);
                    param = cmd.Parameters.AddWithValue("@Offer_letter", documents.Offer_letter);
                    param = cmd.Parameters.AddWithValue("@Exp_certificate", documents.Exp_certificate);
                    param = cmd.Parameters.AddWithValue("@Latest_payslip", documents.Latest_payslip);
                    param = cmd.Parameters.AddWithValue("@Photo", documents.Photo);
                    param = cmd.Parameters.AddWithValue("@Comments", documents.Comments);
                    param = cmd.Parameters.AddWithValue("@CreatedOn", documents.CreatedOn);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }


            return RedirectToAction("Index", "Sign");
        }
        /// <summary>  
        /// Save Posted File in Physical path and return saved path to store in a database  
        /// </summary>  
        /// <param name="file"></param>  
        /// <returns></returns>  



        //    [HttpPost]
        //    public FileResult DownloadFile(int? fileId, string docs)
        //    {
        //        byte[] bytes;
        //        string fileName, contentType;

        //        using (SqlConnection con = new SqlConnection("Data Source=AA171246-BLR;Initial Catalog=usersDb;Integrated Security=True;MultipleActiveResultSets=True"))
        //        {
        //            using (SqlCommand cmd = new SqlCommand())
        //            {
        //                cmd.CommandText = "SELECT ID,Marksheet FROM uploadDocss2 WHERE Id=8";

        //                cmd.Connection = con;
        //                con.Open();
        //                using (SqlDataReader sdr = cmd.ExecuteReader())
        //                {
        //                    sdr.Read();
        //                    bytes = (byte[])sdr["marksheet"];
        //                    contentType = "pdf";
        //                    fileName = "Marksheet.pdf";
        //                }
        //                con.Close();
        //            }
        //        }

        //        return File(bytes, contentType, fileName);
        //    }
        private static List<uploadDoc> GetFiles()
        {

            List<uploadDoc> files = new List<uploadDoc>();

            using (SqlConnection con = new SqlConnection("Data Source=192.168.168.118;Initial Catalog=Cepheus_HRPortal_QC;User ID=cepheus_qc;Password=cepheus_qc@123"))
            {
                string query = "getDocsStrProcedure";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            files.Add(new uploadDoc
                            {
                                ID = Convert.ToInt32(sdr["id"]),
                                Name = sdr["name"].ToString(),
                                Allfiles = sdr["allfiles"].ToString(),
                                Marksheet = sdr["marksheet"].ToString(),
                                Aadhar_card = sdr["aadharCard"].ToString(),
                                PAN_card = sdr["panCard"].ToString(),
                                Passport = sdr["passport"].ToString(),
                                Bank_stmt = sdr["bankStmt"].ToString(),
                                Revealing_letter = sdr["revealingLetter"].ToString(),
                                Offer_letter = sdr["offerLetter"].ToString(),
                                Exp_certificate = sdr["expCertificate"].ToString(),
                                Latest_payslip = sdr["latestPayslip"].ToString(),
                                Photo = sdr["photo"].ToString(),
                                Comments = sdr["Comments"].ToString(),
                                CreatedOn = Convert.ToDateTime(sdr["createdOn"])
                            });
                        }
                    }
                    con.Close();
                }
            }
            return files;
        }
        public ActionResult Hr_Portal()
        {
            return View(GetFiles());
        }
    }

    //[HttpPost]
    //public FileResult DownloadFile(int? fileId)
    //{
    //    byte[] bytes;
    //    string fileName, contentType;
    //    string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
    //    using (SqlConnection con = new SqlConnection(constr))
    //    {
    //        using (SqlCommand cmd = new SqlCommand())
    //        {
    //            cmd.CommandText = "SELECT Name, Data, ContentType FROM tblFiles WHERE Id=@Id";
    //            cmd.Parameters.AddWithValue("@Id", fileId);
    //            cmd.Connection = con;
    //            con.Open();
    //            using (SqlDataReader sdr = cmd.ExecuteReader())
    //            {
    //                sdr.Read();
    //                bytes = (byte[])sdr["Data"];
    //                contentType = sdr["ContentType"].ToString();
    //                fileName = sdr["name"].ToString();
    //            }
    //            con.Close();
    //        }
    //    }

    //    return File(bytes, contentType, fileName);
    //}



}