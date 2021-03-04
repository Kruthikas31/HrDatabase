using JARVIS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JARVIS.DBServices
{
    public class ExperienceDetailsDataMapper : IDataMapper<ExperienceDetails, ExperienceDetails>
    {
        private SqlConnection connection;
        public ExperienceDetailsDataMapper(SqlConnection connection)
        {
            this.connection = connection;
        }
        public int InsertInfo(ExperienceDetails item)
        {
            using (connection)
            {
                try
                {
                    this.connection.Open();
                    //string cmd = "INSERT INTO EmployeeDetails (id,firstName, middleName, lastName, dob, gender, bloodGroup, phoneNumber, email, emergencyNumber, PAN, aadhar, maritalStatus, UAN, nationality, ) VALUES (1,'" + item.firstName + "','" + item.middleName + "','" + item.lastName + "','"+ item.dob + "','"+ item.gender+ "','" + item.bloodGroup+ "','"+ item.phoneNumber +"','"+item.email+"','"+item.emergencyNumber+"','" +item.PAN + "','" +item.aadhar + "','" +item.maritalStatus + "','" +item.UAN + "','" +item.nationality+ "'); INSERT INTO BankDetails(id) VALUES (1);INSERT INTO DependentDetails(id) VALUES (1);INSERT INTO EducationDetails(id) VALUES (1);INSERT INTO ExperienceDetails(id) VALUES (1)";
                    string cmd;
                    cmd = "INSERT INTO ExperienceDetails (id, companyName, role, pastSalary, fromDate, toDate) VALUES (" + item.id + ",'" + item.companyName + "','" + item.role + "','" + item.pastSalary + "','" + item.fromDate.ToString("yyyy/MM/dd") + "','" + item.toDate.ToString("yyyy/MM/dd") + "')";
                    using (SqlCommand command = new SqlCommand(cmd, this.connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected;
                    }
                }
                catch (Exception exception)
                {
                    return 0;
                }
            }
        }

        public List<ExperienceDetails> SelectInfo(int id)
        {
            using (connection)
            {
                try
                {
                    this.connection.Open();
                    //string cmd = "INSERT INTO EmployeeDetails (id,firstName, middleName, lastName, dob, gender, bloodGroup, phoneNumber, email, emergencyNumber, PAN, aadhar, maritalStatus, UAN, nationality, ) VALUES (1,'" + item.firstName + "','" + item.middleName + "','" + item.lastName + "','"+ item.dob + "','"+ item.gender+ "','" + item.bloodGroup+ "','"+ item.phoneNumber +"','"+item.email+"','"+item.emergencyNumber+"','" +item.PAN + "','" +item.aadhar + "','" +item.maritalStatus + "','" +item.UAN + "','" +item.nationality+ "'); INSERT INTO BankDetails(id) VALUES (1);INSERT INTO DependentDetails(id) VALUES (1);INSERT INTO EducationDetails(id) VALUES (1);INSERT INTO ExperienceDetails(id) VALUES (1)";

                    string cmd = "SELECT * FROM ExperienceDetails WHERE id=" + id;
                    List<ExperienceDetails> experienceDetails = new List<ExperienceDetails>();

                    using (SqlCommand command = new SqlCommand(cmd, this.connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            ExperienceDetails experience = new ExperienceDetails();
                            experience.companyName = reader["companyName"].ToString();
                            experience.role = reader["role"].ToString();
                            experience.pastSalary = reader["pastSalary"].ToString();
                            experience.fromDate = (DateTime)reader["fromDate"];
                            experience.toDate = (DateTime)reader["toDate"];
                            experienceDetails.Add(experience);
                        }
                    }
                    return experienceDetails;
                }
                catch (Exception exception)
                {
                    List<ExperienceDetails> experienceDetails = new List<ExperienceDetails>();
                    return experienceDetails;
                }
            }
        }

        public int UpdateInfo(ExperienceDetails oldItem, ExperienceDetails newItem)
        {
            using (connection)
            {
                try
                {
                    this.connection.Open();
                    string cmd;


                    cmd = "UPDATE ExperienceDetails SET companyName='" + newItem.companyName + "',role='" + newItem.role + "',pastSalary='" + newItem.pastSalary + "',fromDate='" + newItem.fromDate.ToString("yyyy/MM/dd") + "',toDate='" + newItem.toDate.ToString("yyyy/MM/dd") + "' WHERE id=" + oldItem.id + " AND companyName='" + oldItem.companyName + "' AND role='" + oldItem.role + "' AND pastSalary='" + oldItem.pastSalary + "' AND fromDate='" + oldItem.fromDate.ToString("yyyy/MM/dd") + "' AND toDate='" + oldItem.toDate.ToString("yyyy/MM/dd") + "'";
                    using (SqlCommand command = new SqlCommand(cmd, this.connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected;
                    }
                }
                catch (Exception exception)
                {
                    return 0;
                }
            }
        }

    }
}