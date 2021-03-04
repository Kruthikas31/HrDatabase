using JARVIS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JARVIS.DBServices
{
    public class BasicInformationDataMapper : IDataMapper<BasicInformation, int>
    {
        private SqlConnection connection;
        log4net.ILog Log = log4net.LogManager.GetLogger(typeof(BasicInformationDataMapper));
            
        public BasicInformationDataMapper(SqlConnection connection)
        {
            this.connection = connection;
            Log.Info("Basic Information Data Mapper !!!");
            Log.Debug("DB Connection Established");
        }

        public int SelectId(string token)
        {
            //SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = GenericClassForDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"
            using (connection)
            {
                try
                {
                    this.connection.Open();
                    int id = 0;
                    string selectCmd = "SELECT id FROM EmployeeDetails WHERE prospectiveEmpGuid='" + token + "'";
                    using (SqlCommand command = new SqlCommand(selectCmd, this.connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            id = (int)reader["id"];
                        }
                        reader.Close();
                       
                        return id;
                    }
                    this.connection.Close();
                   
                }
                catch (Exception exception)
                {
                    Log.Debug("Select ID Exception: "+exception);
                    return 0;
                }

            }

        }
        public int UpdateInfo(BasicInformation item, int id)
        {
            //SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = GenericClassForDB; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"
            using (connection)
            {
                try
                {
                    this.connection.Open();
                    //string cmd = "INSERT INTO EmployeeDetails (id,firstName, middleName, lastName, dob, gender, bloodGroup, phoneNumber, email, emergencyNumber, PAN, aadhar, maritalStatus, UAN, nationality, ) VALUES (1,'" + item.firstName + "','" + item.middleName + "','" + item.lastName + "','"+ item.dob + "','"+ item.gender+ "','" + item.bloodGroup+ "','"+ item.phoneNumber +"','"+item.email+"','"+item.emergencyNumber+"','" +item.PAN + "','" +item.aadhar + "','" +item.maritalStatus + "','" +item.UAN + "','" +item.nationality+ "'); INSERT INTO BankDetails(id) VALUES (1);INSERT INTO DependentDetails(id) VALUES (1);INSERT INTO EducationDetails(id) VALUES (1);INSERT INTO ExperienceDetails(id) VALUES (1)";
                    string status = "";
                    string selectCmd = "SELECT status FROM EmployeeDetails WHERE id=" + id;
                    using (SqlCommand command = new SqlCommand(selectCmd, this.connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            status = reader["status"].ToString();
                        }
                        reader.Close();
                    }
                    string updateCmd = "";
                    if (status == "Invited")
                    {
                        updateCmd = "UPDATE EmployeeDetails SET firstName='" + item.firstName + "',middleName='" + item.middleName + "',lastName='" + item.lastName + "',dob='" + item.dob + "',gender='" + item.gender + "',bloodGroup='" + item.bloodGroup + "',phoneNumber='" + item.phoneNumber + "',email='" + item.email + "',emergencyNumber='" + item.emergencyNumber + "',PAN='" + item.PAN + "',aadhar='" + item.aadhar + "',maritalStatus='" + item.maritalStatus + "',UAN='" + item.UAN + "',nationality='" + item.nationality + "',passportNumber='" + item.passportNumber + "',passportExpiryDate='" + item.passportExpiryDate + "',status='Opened'  WHERE id=" + id + "; UPDATE BankDetails SET accNumber='" + item.bankDetails.accNumber + "',branchDetails='" + item.bankDetails.branchDetails + "',ifsc='" + item.bankDetails.ifsc + "' WHERE id=" + id+";";
                    }
                    else
                    {
                        updateCmd = "UPDATE EmployeeDetails SET firstName='" + item.firstName + "',middleName='" + item.middleName + "',lastName='" + item.lastName + "',dob='" + item.dob + "',gender='" + item.gender + "',bloodGroup='" + item.bloodGroup + "',phoneNumber='" + item.phoneNumber + "',email='" + item.email + "',emergencyNumber='" + item.emergencyNumber + "',PAN='" + item.PAN + "',aadhar='" + item.aadhar + "',maritalStatus='" + item.maritalStatus + "',UAN='" + item.UAN + "',nationality='" + item.nationality + "',passportNumber='" + item.passportNumber + "',passportExpiryDate='" + item.passportExpiryDate + "' WHERE id=" + id + "; UPDATE BankDetails SET accNumber='" + item.bankDetails.accNumber + "',branchDetails='" + item.bankDetails.branchDetails + "',ifsc='" + item.bankDetails.ifsc + "' WHERE id=" + id;
                    }
                    //        cmd = "UPDATE EmployeeDetails SET firstName='" + item.firstName + "',middleName='" + item.middleName + "',lastName='" + item.lastName + "',dob='" + item.dob + "',gender='" + item.gender + "',bloodGroup='" + item.bloodGroup + "',phoneNumber='" + item.phoneNumber + "',email='" + item.email + "',emergencyNumber='" + item.emergencyNumber + "',PAN='" + item.PAN + "',aadhar='" + item.aadhar + "',maritalStatus='" + item.maritalStatus + "',UAN='" + item.UAN + "',nationality='" + item.nationality + "',passportNumber='"+item.passportNumber+"',passportExpiryDate='"+item.passportExpiryDate+"' WHERE id=1;UPDATE BankDetails SET accNumber='"+item.accNumber+"',branchDetails='"+item.branchDetails+ "' WHERE id=1;";
                    if (item.pSameAsc == "on")
                        updateCmd += "UPDATE AddressDetails SET addressType='c/p', streetName='" + item.streetName + "',city='" + item.city + "',state='" + item.state + "',pincode='" + item.pincode + "',country='" + item.country + "' WHERE id=" + id;
                    else
                        updateCmd += "UPDATE AddressDetails SET addressType='current', streetName='" + item.streetName + "',city='" + item.city + "',state='" + item.state + "',pincode='" + item.pincode + "',country='" + item.country + "' WHERE id=" + id + "; INSERT INTO AddressDetails (id, addressType, streetName, city, state, pincode, country) VALUES (" + id + ",'permanent','" + item.pStreetName + "','" + item.pCity + "','" + item.pState + "','" + item.pPincode + "','" + item.pCountry + "');";


                    using (SqlCommand command = new SqlCommand(updateCmd, this.connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected;
                    }
                    this.connection.Close();
                }
                catch (Exception exception)
                {
                    Log.Debug("Update Info Exception: " + exception);
                    return 0;
                }

            }
        }

        public int InsertInfo(BasicInformation item)
        {
            using (connection)
            {
                try
                {

                    this.connection.Open();
                    //string cmd = "INSERT INTO EmployeeDetails (id,firstName, middleName, lastName, dob, gender, bloodGroup, phoneNumber, email, emergencyNumber, PAN, aadhar, maritalStatus, UAN, nationality, ) VALUES (1,'" + item.firstName + "','" + item.middleName + "','" + item.lastName + "','"+ item.dob + "','"+ item.gender+ "','" + item.bloodGroup+ "','"+ item.phoneNumber +"','"+item.email+"','"+item.emergencyNumber+"','" +item.PAN + "','" +item.aadhar + "','" +item.maritalStatus + "','" +item.UAN + "','" +item.nationality+ "'); INSERT INTO BankDetails(id) VALUES (1);INSERT INTO DependentDetails(id) VALUES (1);INSERT INTO EducationDetails(id) VALUES (1);INSERT INTO ExperienceDetails(id) VALUES (1)";
                    string cmd;

                    int rowsAffected = 0;
                    int id = 0;
                    int nightShift = 0;
                    if (item.nightShiftAllowance == "on")
                        nightShift = 1;
                    else
                        nightShift = 0;

                    string insertCmd = "INSERT INTO EmployeeDetails ( prospectiveEmpGuid, firstName, email, role, ctc, joiningDate, approvedManager, reportingManager, status, monthlyGross, monthlyIncentive,  nightShift) VALUES ('" + item.prospectiveEmpGuid + "','" + item.firstName + "','" + item.email + "','" + item.role + "','" + item.ctc + "','" + item.joiningDate + "','" + item.approvedManager + "','" + item.reportingManager + "','Invited','" + item.monthlyGross + "','" + item.monthlyIncentive + "','" + nightShift + "');";


                    string selectCmd = "SELECT id FROM EmployeeDetails WHERE prospectiveEmpGuid='"+item.prospectiveEmpGuid+"'";//else
                    //     cmd = "INSERT INTO EmployeeDetails (id, firstName,email, role, ctc, joiningDate, approvedManager, reportingManager) VALUES (1,'" + item.fullName + "','" + item.email + "','" + item.role + "','" + item.ctc + "','" + item.joiningDate + "','" + item.approvedManager + "','" + item.reportingManager + "');INSERT INTO BankDetails(id) VALUES (1);INSERT INTO AddressDetails(id) VALUES (1)";

                    using (SqlCommand command = new SqlCommand(insertCmd, this.connection))
                    {
                        rowsAffected += command.ExecuteNonQuery();
                    }
                    using (SqlCommand command = new SqlCommand(selectCmd, this.connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            id = (int)reader["id"];
                        }
                        reader.Close();
                    }
                    string insertCmd1 = "INSERT INTO BankDetails(id) VALUES (" + id + "); INSERT INTO AddressDetails(id) VALUES(" + id + ")";
                    using (SqlCommand command = new SqlCommand(insertCmd1, this.connection))
                    {
                        rowsAffected += command.ExecuteNonQuery();
                    }
                    return rowsAffected;
                    this.connection.Close();
                }
                catch (Exception exception)
                {
                    Log.Debug("Insert Info Exception: " + exception);
                    return 0;
                }
            }
        }

        public List<BasicInformation> SelectInfo(int id)
        {
            using (connection)
            {
                try
                {
                    this.connection.Open();
                    //string cmd = "INSERT INTO EmployeeDetails (id,firstName, middleName, lastName, dob, gender, bloodGroup, phoneNumber, email, emergencyNumber, PAN, aadhar, maritalStatus, UAN, nationality, ) VALUES (1,'" + item.firstName + "','" + item.middleName + "','" + item.lastName + "','"+ item.dob + "','"+ item.gender+ "','" + item.bloodGroup+ "','"+ item.phoneNumber +"','"+item.email+"','"+item.emergencyNumber+"','" +item.PAN + "','" +item.aadhar + "','" +item.maritalStatus + "','" +item.UAN + "','" +item.nationality+ "'); INSERT INTO BankDetails(id) VALUES (1);INSERT INTO DependentDetails(id) VALUES (1);INSERT INTO EducationDetails(id) VALUES (1);INSERT INTO ExperienceDetails(id) VALUES (1)";
                    string cmd;

                    // if (item.fresherOrExperienced == "experienced")
                    cmd = "SELECT * FROM EmployeeDetails WHERE id='" + id + "'";
                    //else
                    //     cmd = "INSERT INTO EmployeeDetails (id, firstName,email, role, ctc, joiningDate, approvedManager, reportingManager) VALUES (1,'" + item.fullName + "','" + item.email + "','" + item.role + "','" + item.ctc + "','" + item.joiningDate + "','" + item.approvedManager + "','" + item.reportingManager + "');INSERT INTO BankDetails(id) VALUES (1);INSERT INTO AddressDetails(id) VALUES (1)";
                    List<BasicInformation> basicInformation = new List<BasicInformation>();
                    BasicInformation item = new BasicInformation();
                    using (SqlCommand command = new SqlCommand(cmd, this.connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            item.id = (int)reader["id"];
                            item.firstName = reader["firstName"].ToString();
                            item.middleName = reader["middleName"].ToString();
                          
                            item.lastName = reader["lastName"].ToString();
                            item.dob = reader["dob"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["dob"];
                            //    =(DateTime) reader["dob"];
                            item.gender = reader["gender"].ToString();
                            item.bloodGroup = reader["bloodGroup"].ToString();
                            item.maritalStatus = reader["maritalStatus"].ToString();
                            item.nationality = reader["nationality"].ToString();
                            item.aadhar = reader["aadhar"].ToString();
                            item.PAN = reader["PAN"].ToString();
                            item.passportNumber = reader["passportNumber"].ToString();
                            item.passportExpiryDate = reader["passportExpiryDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["passportExpiryDate"];
                            //(DateTime) reader["passportExpiryDate"];
                            item.ESI = reader["ESI"].ToString();
                            item.UAN = reader["UAN"].ToString();
                            item.phoneNumber = reader["phoneNumber"].ToString();
                            item.emergencyNumber = reader["emergencyNumber"].ToString();
                            item.email = reader["email"].ToString();
                            item.joiningDate = reader["joiningDate"].ToString();
                            item.ctc = reader["ctc"].ToString();
                            item.approvedManager = reader["approvedManager"].ToString();
                            item.reportingManager = reader["reportingManager"].ToString();
                            item.role = reader["role"].ToString();
                            basicInformation.Add(item);
                        }
                        reader.Close();
                    }
                    cmd = "SELECT * FROM BankDetails WHERE id=" + item.id;
                    using (SqlCommand command = new SqlCommand(cmd, this.connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        item.bankDetails = new BankDetails();
                        while (reader.Read())
                        {

                            item.bankDetails.accNumber = reader["accNumber"].ToString();
                            item.bankDetails.name = item.firstName + " " + item.middleName + " " + item.lastName;
                            item.bankDetails.ifsc = reader["ifsc"].ToString();
                            item.bankDetails.branchDetails = reader["branchDetails"].ToString();
                        }
                        reader.Close();
                    }
                    cmd = "SELECT * FROM AddressDetails WHERE id=" + item.id;
                    using (SqlCommand command = new SqlCommand(cmd, this.connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            if (reader["addressType"].ToString() == "c/p")
                            {
                                item.pSameAsc = "on";
                                item.streetName = reader["streetName"].ToString();
                                item.city = reader["city"].ToString();
                                item.state = reader["state"].ToString();
                                item.pincode = reader["pincode"].ToString();
                                item.country = reader["country"].ToString();
                            }
                            else if (reader["addressType"].ToString() == "current")
                            {
                                item.pSameAsc = "off";
                                item.streetName = reader["streetName"].ToString();
                                item.city = reader["city"].ToString();
                                item.state = reader["state"].ToString();
                                item.pincode = reader["pincode"].ToString();
                                item.country = reader["country"].ToString();

                            }
                            else if (reader["addressType"].ToString() == "permanent")
                            {
                                item.pSameAsc = "off";
                                item.pStreetName = reader["streetName"].ToString();
                                item.pCity = reader["city"].ToString();
                                item.pState = reader["state"].ToString();
                                item.pPincode = reader["pincode"].ToString();
                                item.pCountry = reader["country"].ToString();

                            }
                        }
                        reader.Close();
                    }
                    this.connection.Close();
                    return basicInformation;

                }

                catch (Exception exception)
                {
                    Log.Debug("Select Info Exception: " + exception);
                    List<BasicInformation> item = new List<BasicInformation>();
                    return item;
                }
            }
        }

        public int UpdateStatus(int id)
        {
            //string cmd = "INSERT INTO EmployeeDetails (id,firstName, middleName, lastName, dob, gender, bloodGroup, phoneNumber, email, emergencyNumber, PAN, aadhar, maritalStatus, UAN, nationality, ) VALUES (1,'" + item.firstName + "','" + item.middleName + "','" + item.lastName + "','"+ item.dob + "','"+ item.gender+ "','" + item.bloodGroup+ "','"+ item.phoneNumber +"','"+item.email+"','"+item.emergencyNumber+"','" +item.PAN + "','" +item.aadhar + "','" +item.maritalStatus + "','" +item.UAN + "','" +item.nationality+ "'); INSERT INTO BankDetails(id) VALUES (1);INSERT INTO DependentDetails(id) VALUES (1);INSERT INTO EducationDetails(id) VALUES (1);INSERT INTO ExperienceDetails(id) VALUES (1)";
            try
            {
                string status = "";
                string selectCmd = "SELECT status FROM EmployeeDetails WHERE id=" + id;
                this.connection.Open();
                using (SqlCommand command = new SqlCommand(selectCmd, this.connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        status = reader["status"].ToString();
                    }
                    reader.Close();
                }
                if (status == "Opened")
                {
                    status = "Uploaded";

                }
                else if (status == "Uploaded")
                {
                    status = "Validated";
                }
                string cmd = "UPDATE EmployeeDetails SET status='" + status + "' WHERE id=" + id;
                using (SqlCommand command = new SqlCommand(cmd, this.connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;
                }
                this.connection.Close();
            }
            catch(Exception exception)
            {
                Log.Debug("Update Status Exception: " + exception);
                return 0;
            }
          
        }
    }
}