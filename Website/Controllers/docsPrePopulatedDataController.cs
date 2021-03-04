using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JARVIS.Models;

namespace uploadDocsPrepopulate.Controllers
{
    public class docsPrePopulatedDataController : Controller
    {
        // GET: docsPrePopulatedData

        public ActionResult upload()
        {
            return View(GetFiles());
        }
        private static List<uploadDoc> GetFiles()
        {

            List<uploadDoc> files = new List<uploadDoc>();
            // Data Source = AA171246 - BLR; Initial Catalog = usersDb; Integrated Security = True; MultipleActiveResultSets = True; Application Name = EntityFramework
            // Data Source = 192.168.168.118; Initial Catalog = Cepheus_HRPortal_QC; Persist Security Info = True; User ID = cepheus_qc; Password = cepheus_qc@123
            using (SqlConnection con = new SqlConnection("Data Source = 192.168.168.118; Initial Catalog = Cepheus_HRPortal_QC; Persist Security Info = True; User ID = cepheus_qc; Password = cepheus_qc@123"))
            {
                string query = "prepopulatestrprocedure";
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
                                ID = Convert.ToInt32(sdr["ID"]),
                                Name = sdr["Name"].ToString(),
                                Allfiles = sdr["Allfiles"].ToString(),
                                Marksheet = sdr["Marksheet"].ToString(),
                                Aadhar_card = sdr["Aadhar_card"].ToString(),
                                PAN_card = sdr["PAN_card"].ToString(),
                                Passport = sdr["Passport"].ToString(),
                                Bank_stmt = sdr["Bank_stmt"].ToString(),
                                Revealing_letter = sdr["revealing_letter"].ToString(),
                                Offer_letter = sdr["Offer_letter"].ToString(),
                                Exp_certificate = sdr["Exp_certificate"].ToString(),
                                Latest_payslip = sdr["Latest_payslip"].ToString(),
                                Photo = sdr["photo"].ToString(),
                                Comments = sdr["Comments"].ToString(),
                                CreatedOn = Convert.ToDateTime(sdr["CreatedOn"])
                            });
                        }
                    }
                    con.Close();
                }
            }
            return files;
        }
        [HttpPost]
        public ActionResult upload(uploadDoc data)
        {
            //System.IO.Directory.CreateDirectory(Server.MapPath("~/uploadfiles/" + data.Name));
            //var documents = new uploadDocss3()
            //{

            //    Name = data.Name,
            //    Allfiles = SaveToPhysicalLocation(data.Allfiles),
            //    Marksheet = SaveToPhysicalLocation(data.Marksheet),
            //    Aadhar_card = SaveToPhysicalLocation(data.Aadhar_card),
            //    PAN_card = SaveToPhysicalLocation(data.PAN_card),
            //    Passport = SaveToPhysicalLocation(data.Passport),
            //    Bank_stmt = SaveToPhysicalLocation(data.Bank_stmt),
            //    Revealing_letter = SaveToPhysicalLocation(data.Revealing_letter),
            //    Offer_letter = SaveToPhysicalLocation(data.Offer_letter),
            //    Exp_certificate = SaveToPhysicalLocation(data.Exp_certificate),
            //    Latest_payslip = SaveToPhysicalLocation(data.Latest_payslip),
            //    Photo = SaveToPhysicalLocation(data.Photo),

            //    CreatedOn = DateTime.Now
            //};





            //string SaveToPhysicalLocation(HttpPostedFileBase file)
            //{

            //    if (file != null && file.ContentLength > 0 )
            //    {



            //        var fileName = Path.GetFileName(file.FileName);
            //        var path = Path.Combine(Server.MapPath("~/uploadfiles/" + data.Name), fileName);
            //        file.SaveAs(path);

            //        return fileName;
            //    }
            //    else if (file != null )
            //    {


            //        var fileName = Path.GetFileName(file.FileName);
            //        var path = Path.Combine(Server.MapPath("~/uploadfiles/" + data.Name), fileName);
            //        file.SaveAs(path);

            //        return fileName;
            //    }
            //    else if (file == null )
            //    {
            //        return "This File is in All File column(MainFile).";
            //    }
            //    else
            //    {
            //        return "null";
            //    }

            //}

            //string constr = "Data Source = AA171246 - BLR; Initial Catalog = usersDb; Integrated Security = True";
            //using (SqlConnection con = new SqlConnection("Data Source=AA171246-BLR;Initial Catalog=usersDb;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework"))
            //{
            //    string query = "uploadDocstrprocedure";
            //    using (SqlCommand cmd = new SqlCommand(query, con))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        SqlParameter param;

            //        param = cmd.Parameters.AddWithValue("@Name", documents.Name);
            //        param = cmd.Parameters.AddWithValue("@Allfiles", documents.Allfiles);
            //        param = cmd.Parameters.AddWithValue("@Marksheet", documents.Marksheet);
            //        param = cmd.Parameters.AddWithValue("@Aadhar_card", documents.Aadhar_card);
            //        param = cmd.Parameters.AddWithValue("@PAN_card", documents.PAN_card);
            //        param = cmd.Parameters.AddWithValue("@Passport", documents.Passport);
            //        param = cmd.Parameters.AddWithValue("@Bank_stmt", documents.Bank_stmt);
            //        param = cmd.Parameters.AddWithValue("@Revealing_letter", documents.Revealing_letter);
            //        param = cmd.Parameters.AddWithValue("@Offer_letter", documents.Offer_letter);
            //        param = cmd.Parameters.AddWithValue("@Exp_certificate", documents.Exp_certificate);
            //        param = cmd.Parameters.AddWithValue("@Latest_payslip", documents.Latest_payslip);
            //        param = cmd.Parameters.AddWithValue("@Photo", documents.Photo);
            //        param = cmd.Parameters.AddWithValue("@Comments", documents.Comments);
            //        param = cmd.Parameters.AddWithValue("@CreatedOn", documents.CreatedOn);

            //        con.Open();
            //        cmd.ExecuteNonQuery();
            //        con.Close();
            //    }
            //}

            return View(GetFiles());
        }
    }


}