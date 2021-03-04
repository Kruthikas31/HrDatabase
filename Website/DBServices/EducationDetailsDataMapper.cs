using JARVIS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JARVIS.DBServices
{
    public class EducationDetailsDataMapper : IDataMapper<EducationDetails, EducationDetails>
    {
        private SqlConnection connection;
        public EducationDetailsDataMapper(SqlConnection connection)
        {
            this.connection = connection;
        }
        public int InsertInfo(EducationDetails item)
        {
            using (connection)
            {
                try
                {
                    this.connection.Open();
                    //string cmd = "INSERT INTO EmployeeDetails (id,firstName, middleName, lastName, dob, gender, bloodGroup, phoneNumber, email, emergencyNumber, PAN, aadhar, maritalStatus, UAN, nationality, ) VALUES (1,'" + item.firstName + "','" + item.middleName + "','" + item.lastName + "','"+ item.dob + "','"+ item.gender+ "','" + item.bloodGroup+ "','"+ item.phoneNumber +"','"+item.email+"','"+item.emergencyNumber+"','" +item.PAN + "','" +item.aadhar + "','" +item.maritalStatus + "','" +item.UAN + "','" +item.nationality+ "'); INSERT INTO BankDetails(id) VALUES (1);INSERT INTO DependentDetails(id) VALUES (1);INSERT INTO EducationDetails(id) VALUES (1);INSERT INTO ExperienceDetails(id) VALUES (1)";
                    string cmd;
                    cmd = "INSERT INTO EducationDetails (id, degree, specialization, institute, yearOfPassing) VALUES (" + item.id + ",'" + item.degree + "','" + item.specialization + "','" + item.institute + "','" + item.yearOfPassing + "')";
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

        public List<EducationDetails> SelectInfo(int id)
        {
            using (connection)
            {
                try
                {
                    this.connection.Open();
                    //string cmd = "INSERT INTO EmployeeDetails (id,firstName, middleName, lastName, dob, gender, bloodGroup, phoneNumber, email, emergencyNumber, PAN, aadhar, maritalStatus, UAN, nationality, ) VALUES (1,'" + item.firstName + "','" + item.middleName + "','" + item.lastName + "','"+ item.dob + "','"+ item.gender+ "','" + item.bloodGroup+ "','"+ item.phoneNumber +"','"+item.email+"','"+item.emergencyNumber+"','" +item.PAN + "','" +item.aadhar + "','" +item.maritalStatus + "','" +item.UAN + "','" +item.nationality+ "'); INSERT INTO BankDetails(id) VALUES (1);INSERT INTO DependentDetails(id) VALUES (1);INSERT INTO EducationDetails(id) VALUES (1);INSERT INTO ExperienceDetails(id) VALUES (1)";


                    string cmd = "SELECT * FROM EducationDetails WHERE id=" + id;
                    List<EducationDetails> educationDetails = new List<EducationDetails>();

                    using (SqlCommand command = new SqlCommand(cmd, this.connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            EducationDetails education = new EducationDetails();
                            education.degree = reader["degree"].ToString();
                            education.institute = reader["institute"].ToString();
                            education.specialization = reader["specialization"].ToString();
                            education.yearOfPassing = reader["yearOfPassing"].ToString();

                            educationDetails.Add(education);
                        }
                    }
                    return educationDetails;
                }
                catch (Exception exception)
                {
                    List<EducationDetails> educationDetails = new List<EducationDetails>();
                    return educationDetails;
                }
            }
        }

        public int UpdateInfo(EducationDetails oldItem, EducationDetails newItem)
        {
            using (connection)
            {
                try
                {
                    this.connection.Open();
                    string cmd;


                    cmd = "UPDATE EducationDetails SET degree='" + newItem.degree + "',specialization='" + newItem.specialization + "',institute='" + newItem.institute + "',yearOfPassing='" + newItem.yearOfPassing + "' WHERE id=" + oldItem.id + " AND degree='" + oldItem.degree + "' AND specialization='" + oldItem.specialization + "' AND institute='" + oldItem.institute + "' AND yearOfPassing='" + oldItem.yearOfPassing + "'";
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