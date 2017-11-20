using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using DataLayer.Utilities;
using DataLayer.Interfaces;

namespace DataLayer
{
    public class UserDataBase:IUserDataBase
    {

        static MySqlConnection Conn;
           
        static string ConnetionString = "server=localhost;user id=root;password=root;persistsecurityinfo=True;database=kaushik";
            
        static UserDataBase() //static constructor
        {
                Conn = new MySqlConnection(ConnetionString);
               
                
                
        }


        public bool GetValidUser(int adminid)
        { //checks for Valid user- admin begin 
            try
            {
                CMSLogger.SetProperties("AuthToken", adminid.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, this.ToString());
                CMSLogger.log.Info("getValidUserInvoked");
                Conn.Open();
                MySqlCommand cmd;
                DataTable dataTable = new DataTable();
                cmd = new MySqlCommand("getValidUser", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@c_adminid", adminid);
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dataTable);
                }
                if (dataTable.Rows.Count == 1)
                {
                    Conn.Close();
                    return true;
                }
                else
                {
                    Conn.Close();
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                CMSLogger.SetProperties("AuthToken", adminid.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, this.ToString());
                CMSLogger.log.Error(ex.Message, ex);
                Conn.Close();
                throw;
            }


        } //checks for Valid user- admin end

        public UserDataObject VerifyUser(string mail){

            
            DataTable dataTable = new DataTable();
            UserDataObject obj = new UserDataObject();
            
            try
            {
                CMSLogger.SetProperties("AuthToken",mail, System.Reflection.MethodBase.GetCurrentMethod().Name, this.ToString());
                CMSLogger.log.Info("VerifyUserInvoked");
                Conn.Open();
                MySqlCommand cmd;
                cmd = new MySqlCommand("verifyuser", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mail", mail);
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dataTable);
                }

                //to fetch secret
                if (dataTable.Rows.Count != 0)
                {
                    obj.Adminid = (int)dataTable.Rows[0].ItemArray[0];
                    obj.UserName = (string)dataTable.Rows[0].ItemArray[1].ToString();
                    obj.Emailid = (string)dataTable.Rows[0].ItemArray[2].ToString();
                    obj.Password = (string)dataTable.Rows[0].ItemArray[3].ToString();
                }
                else
                { 
                    obj.Password = "null";
                }




                Conn.Close();
            }
            catch(Exception ex)
            {
                CMSLogger.SetProperties("AuthToken", mail, System.Reflection.MethodBase.GetCurrentMethod().Name, this.ToString());
                CMSLogger.log.Error(ex.Message, ex);
                Conn.Close();
                throw;

            }




            return obj;
        }


        public int Register(UserDataObject userObj)
        {//SET DETAILS BEGINS
            int result = 0;
            try
            {
                CMSLogger.SetProperties("AuthToken", "unassigned" , System.Reflection.MethodBase.GetCurrentMethod().Name, this.ToString());
                CMSLogger.log.Info("registerInvoked");

                Conn.Open();
                MySqlCommand cmd;
                cmd = new MySqlCommand("registeruser", Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", userObj.UserName);
                cmd.Parameters.AddWithValue("@dob", userObj.Dob);
                cmd.Parameters.AddWithValue("@gender", userObj.Gender);
                cmd.Parameters.AddWithValue("@phone", userObj.Phone);
                cmd.Parameters.AddWithValue("@address", userObj.Address);
                cmd.Parameters.AddWithValue("@city", userObj.City);
                cmd.Parameters.AddWithValue("@emailid", userObj.Emailid);
                cmd.Parameters.AddWithValue("@password", userObj.Password);
                result = cmd.ExecuteNonQuery();
                
                Console.WriteLine("num of rows affected" + result);

                Conn.Close();
            }
            catch (Exception ex)
            {
                CMSLogger.SetProperties("AuthToken", "unassigned"  , System.Reflection.MethodBase.GetCurrentMethod().Name, this.ToString());
                CMSLogger.log.Error(ex.Message, ex);
                Conn.Close();
                throw;

            }
            return result;
        }//SET DETAILS END
    }
}
