using JARVIS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JARVIS.DBServices
{
    public class DependentDetailsDataMapper : IDataMapper<DependentDetails, DependentDetails>
    {
        private SqlConnection connection;
        public DependentDetailsDataMapper(SqlConnection connection)
        {
            this.connection = connection;
        }
        public int InsertInfo(DependentDetails item)
        {
            using (connection)
            {
                try
                {
                    this.connection.Open();
                    //string cmd = "INSERT INTO EmployeeDetails (id,firstName, middleName, lastName, dob, gender, bloodGroup, phoneNumber, email, emergencyNumber, PAN, aadhar, maritalStatus, UAN, nationality, ) VALUES (1,'" + item.firstName + "','" + item.middleName + "','" + item.lastName + "','"+ item.dob + "','"+ item.gender+ "','" + item.bloodGroup+ "','"+ item.phoneNumber +"','"+item.email+"','"+item.emergencyNumber+"','" +item.PAN + "','" +item.aadhar + "','" +item.maritalStatus + "','" +item.UAN + "','" +item.nationality+ "'); INSERT INTO BankDetails(id) VALUES (1);INSERT INTO DependentDetails(id) VALUES (1);INSERT INTO EducationDetails(id) VALUES (1);INSERT INTO ExperienceDetails(id) VALUES (1)";
                    string cmd;
                    cmd = "INSERT INTO DependentDetails (id, depName,relation, dob, gender) VALUES (" + item.id + ",'" + item.depName + "','" + item.relation + "','" + item.dob + "','" + item.gender + "')";
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
        public int UpdateInfo(DependentDetails oldItem, DependentDetails newItem)
        {
            using (connection)
            {
                try
                {
                    this.connection.Open();
                    string cmd;

                    var format = "d";
                    //newDob = DateTime.Parse(newItem.dob, CultureInfo.cCulture);

                    //oldDob = DateTime.Parse(oldItem.dob, CultureInfo.cCulture);
                    cmd = "UPDATE DependentDetails SET depName='" + newItem.depName + "',relation='" + newItem.relation + "',dob='" + newItem.dob.ToString("yyyy/MM/dd") + "',gender='" + newItem.gender + "' WHERE id=" + oldItem.id + " AND depName='" + oldItem.depName + "' AND relation='" + oldItem.relation + "' AND dob='" + oldItem.dob.ToString("yyyy/MM/dd") + "' AND gender='" + oldItem.gender + "'";
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



        public List<DependentDetails> SelectInfo(int id)
        {
            using (connection)
            {
                try
                {
                    this.connection.Open();
                    //string cmd = "INSERT INTO EmployeeDetails (id,firstName, middleName, lastName, dob, gender, bloodGroup, phoneNumber, email, emergencyNumber, PAN, aadhar, maritalStatus, UAN, nationality, ) VALUES (1,'" + item.firstName + "','" + item.middleName + "','" + item.lastName + "','"+ item.dob + "','"+ item.gender+ "','" + item.bloodGroup+ "','"+ item.phoneNumber +"','"+item.email+"','"+item.emergencyNumber+"','" +item.PAN + "','" +item.aadhar + "','" +item.maritalStatus + "','" +item.UAN + "','" +item.nationality+ "'); INSERT INTO BankDetails(id) VALUES (1);INSERT INTO DependentDetails(id) VALUES (1);INSERT INTO EducationDetails(id) VALUES (1);INSERT INTO ExperienceDetails(id) VALUES (1)";


                    string cmd = "SELECT * FROM DependentDetails WHERE id=" + id;

                    List<DependentDetails> dependentDetails = new List<DependentDetails>();

                    using (SqlCommand command = new SqlCommand(cmd, this.connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            DependentDetails dependent = new DependentDetails();
                            dependent.depName = reader["depName"].ToString();
                            dependent.relation = reader["relation"].ToString();
                            dependent.gender = reader["gender"].ToString();
                            dependent.dob = (DateTime)reader["dob"];
                            dependentDetails.Add(dependent);
                        }
                    }
                    return dependentDetails;
                }
                catch (Exception exception)
                {
                    List<DependentDetails> dependentDetails = new List<DependentDetails>();
                    return dependentDetails;
                }
            }
        }
    }
}