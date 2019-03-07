using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public class DataLayer //Data layer class that allows other classes to read from. Validates users and security. Also grabs data set information to list within grid views and data sets.
    {
        string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; //Connects to database through ConnectionString in the Web.Config file
        SqlConnection con;
        SqlCommand com;
        SqlDataReader myReader; //Provides a way of reading a forward-only stream of rows from a SQL Server database. This class cannot be inherited.
        SqlDataAdapter adapter; //Represents a set of data commands and a database connection that are used to fill the DataSet and update a SQL Server database. This class cannot be inherited.
        DataSet ds = new DataSet(); //Creates data set


        //Method that validates user for login security
        public string validateUser(string username, string password)
        {
            try
            {
                con = new SqlConnection(constr);
                con.Open(); //Opens connection
                com = new SqlCommand(); //Creates new SqlCommand
                com.Connection = con;
                com.CommandText = "SELECT * FROM register WHERE Username = '" + username + "' and Password = '" + password + "' "; //SQL command to select username and password from Register database


                using (myReader = com.ExecuteReader()) //Sends the CommandText to the Connection and builds a SqlDataReader.
                {
                    while (myReader.Read())
                    {
                        if (myReader["Password"].ToString() == password)
                        {
                            string returnVal = myReader["Security"].ToString();
                            con.Close();
                            return returnVal;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString()); //Lists exception if user encounters an error
            }
            con.Close(); //Closes connection
            return "";
        }

        //Method that takes information from data set and lists within gridview (if method is called)
        public DataSet grabDataSet(string sqlString)
        {
            DataSet dataSet = new DataSet();

            try
            {
                con = new SqlConnection(constr);
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                com.CommandText = sqlString;
                adapter = new SqlDataAdapter(com);
                adapter.Fill(dataSet);

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                com.Connection.Close();
            }
            return dataSet;
        }

        // Handles creation of new login accounts, returns true if successful
        public bool addUser(string userid, string password, string security)
        {
            try
            {
                con = new SqlConnection(constr);
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "INSERT INTO login([userid], [password], [security]) VALUES ('" + userid + "', '" + password + "', '" + security + "')";


            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                con.Close();
            }
            return false;
        }

        // Method check for if the user exists
        public bool IsUserExists(string userid)
        {
            bool Result = false;
            try
            {
                con = new SqlConnection(constr);
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "SELECT * FROM login WHERE [userid] = '" + userid + "'";

                using (myReader = com.ExecuteReader())
                {
                    while (myReader.Read())
                    {
                        Result = true;
                    }
                }
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            return Result;

        }

        // Handles updating user login accounts
        public bool updateUser(string userid, string password, string security)
        {
            try
            {
                con = new SqlConnection(constr);
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "UPDATE login SET [password] = '" + password + "', [security] = '" + security + "' WHERE [userid] = '" + userid + "'";
                com.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                con.Close();
            }

            return false;
        }

        // Handles deleting user login accounts
        public bool deleteUser(string userid)
        {
            try
            {
                con = new SqlConnection(constr);
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "DELETE FROM login WHERE [userid] = '" + userid + "'";
                com.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                con.Close();
            }
            return false;
        }

        // Handles creation of new staff requests
        public bool addRequest(string userid, string staff, string location, string worktype, string salary)
        {
            try
            {
                con = new SqlConnection(constr);
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "INSERT INTO requests([userid], [staff], [location], [worktype], [salary], [status]) VALUES ('" + userid + "', '" + staff + "', '" + location + "', '" + worktype + "', '" + salary + "', 'Not yet reviewed')";
                com.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());

            }
            finally
            {
                con.Close();
            }

            return false;
        }

        // Handles grabbing staff requests by requestid and sends back the info in a dictionary
        public Dictionary<string, string> getRequest(string requestid, string userid)
        {
            Dictionary<string, string> myDict = new Dictionary<string, string>();
            try
            {
                con = new SqlConnection(constr);
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "SELECT * FROM requests WHERE [requestid] = " + requestid;
                com.ExecuteNonQuery();

                using (myReader = com.ExecuteReader())
                {
                    while (myReader.Read())
                    {
                        if (myReader["userid"].ToString() == userid)
                        {
                            myDict["staff"] = myReader["staff"].ToString();
                            myDict["location"] = myReader["location"].ToString();
                            myDict["worktype"] = myReader["worktype"].ToString();
                            myDict["salary"] = myReader["salary"].ToString();
                            myDict["status"] = myReader["status"].ToString();
                            con.Close();
                            return myDict;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());

            }
            return null;
        }

        // Handles updating staff requests
        public bool updateRequest(string requestid, string staff, string location, string worktype, string salary, string status)
        {
            try
            {
                con = new SqlConnection(constr);
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "UPDATE request SET [staff] = '" + staff + "', [location] = '" + location + "', [worktype] = '" + worktype + "', [salary] = '" + salary + "', [status] = '" + status + "' WHERE requestid = " + requestid;
                com.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                con.Close();
            }
            return false;
        }

        // Handles deleting user login accounts
        public bool deleteRequest(string requestid)
        {
            try
            {
                con = new SqlConnection(constr);
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "DELETE FROM requests WHERE [requestid] = " + requestid;
                com.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                con.Close();
            }
            return false;
        }

        // Handles creation of staff info
        public bool addStaff(string userid, string full_name, string degree, string experience, string salary, string street, string city, string state, string zipcode)
        {
            try
            {
                con = new SqlConnection(constr);
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "INSERT INTO staff([userid], [full_name], [degree], [experience], [salary], [street], [city], [state], [zipcode]) VALUES ('" + userid + "', '" + full_name + "', '" + degree + "', '" + experience + "', '" + salary + "', '" + street + "', '" + city + "', '" + state + "', '" + zipcode + "')";
                com.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());

            }
            finally
            {
                con.Close();
            }
            return false;
        }

        // Handles grabbing staff info and sends back the info in a dictionary
        public Dictionary<string, string> getStaff(string userid)
        {
            Dictionary<string, string> myDict = new Dictionary<string, string>();
            try
            {
                con = new SqlConnection(constr);
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "SELECT * FROM staff WHERE [userid] = '" + userid + "'";

                using (myReader = com.ExecuteReader())
                {
                    while (myReader.Read())
                    {
                        myDict.Add("full_name", myReader["full_name"].ToString());
                        myDict.Add("degree", myReader["degree"].ToString());
                        myDict.Add("experience", myReader["experience"].ToString());
                        myDict.Add("salary", myReader["salary"].ToString());
                        myDict.Add("street", myReader["street"].ToString());
                        myDict.Add("city", myReader["city"].ToString());
                        myDict.Add("state", myReader["state"].ToString());
                        myDict.Add("zipcode", myReader["zipcode"].ToString());
                    }
                }
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            con.Close();
            return myDict;

        }

        // Handles updating staff info
        public bool updateStaff(string userid, string full_name, string degree, string experience, string salary, string street, string city, string state, string zipcode)
        {
            try
            {
                con = new SqlConnection(constr);
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "UPDATE staff SET [full_name] = '" + full_name + "', [degree] = '" + degree + "', [experience] = '" + experience + "', [salary] = '" + salary + "', [street] = '" + street + "', [city] = '" + city + "', [state] = '" + state + "', [zipcode] = '" + zipcode + "' WHERE userid = '" + userid + "'";
                com.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                con.Close();
            }
            return false;
        }

        // Handles deleting staff info
        public bool deleteStaff(string userid)
        {
            try
            {
                con = new SqlConnection(constr);
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "DELETE FROM staff WHERE [userid] = '" + userid + "'";
                com.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                con.Close();
            }
            return false;

        }

        // Grabs the specific sql data and returns a dataset with the found information
        public string grabRequestNumber(string userid)
        {
            try
            {
                con = new SqlConnection(constr);
                con.Open();
                com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "SELECT max(requestid) AS [last] FROM requests WHERE userid = '" + userid + "'";

                using (myReader = com.ExecuteReader())
                {
                    while (myReader.Read())
                    {
                        return myReader["last"].ToString();
                    }
                }
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
            finally
            {
                com.Connection.Close();
            }
            return "-1";
        }
    }
}