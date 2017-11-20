using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using log4net;
//using System.Configuration;
using System.Web.Configuration;
using DataLayer.Interfaces;
using DataLayer.CustomData;
using DataLayer.Utilities;


namespace DataLayer
{
   
    public class SQlCustomerDataBase:ICustomerDataBase
    {
       

        static MySqlConnection cnn;
           
        static string connetionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            
        static SQlCustomerDataBase() //static constructor
        {
                cnn = new MySqlConnection(connetionString);
               
        }

        public List<CustomerDataObject> GetDetailsAll(Retrieve_message message)//string searchparam,string searchvalue,int adminid,int start_index,int end_index,string sortparam)
        { //get detailsAll begin

            CMSLogger.SetProperties("AuthToken", message.Adminid.ToString(),System.Reflection.MethodBase.GetCurrentMethod().Name,this.ToString());
            CMSLogger.log.Info("GetDetailsALlInvoked");

            CustomerDataObject[] obj=new CustomerDataObject[100];// = new DataBase[50]; //warning 100 manually coded here
            MySqlCommand cmd;
            DataTable dataTable = new DataTable();
            List<CustomerDataObject> customer=new List<CustomerDataObject>();         
 
            try
            {
                if(message.Adminid==9999)
                cmd = new MySqlCommand("dummygetDetailsAll", cnn);
                else
                    cmd = new MySqlCommand("getDetailsAll", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@c_searchparam", message.Searchparam);
                cmd.Parameters.AddWithValue("@c_searchvalue", message.Searchvalue);
                cmd.Parameters.AddWithValue("@c_adminid", message.Adminid);
                cmd.Parameters.AddWithValue("@c_start_index", message.Start_index);
                cmd.Parameters.AddWithValue("@c_end_index", message.End_index);
                cmd.Parameters.AddWithValue("@c_sortparam", message.Sortparam);

                //cmd.Parameters.AddWithValue("@c_searchparam", "gender");
                //cmd.Parameters.AddWithValue("@c_searchvalue", "male");
                //cmd.Parameters.AddWithValue("@c_adminid", 9999);
                //cmd.Parameters.AddWithValue("@c_start_index", 1);
                //cmd.Parameters.AddWithValue("@c_end_index", 7);
                //cmd.Parameters.AddWithValue("@c_sortparam", "age");

                //cnn.Close();

                cnn.Open();
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dataTable);
                    cnn.Close();

                }

                //to print the datatable 
                //to build db object
             
                
                int dbobj_count = 0;
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    int count = 0;
                    
                    obj[dbobj_count] = new CustomerDataObject();
                    foreach (var item in dataRow.ItemArray)
                    {
                        Console.WriteLine(item);
                        count++;
                      
                        switch (count)
                        {
                            case 1: obj[dbobj_count].Id = (int)item; Console.WriteLine(obj[dbobj_count].Id); break;
                            case 2: obj[dbobj_count].CustomerName = (string)item; Console.WriteLine(obj[dbobj_count].CustomerName); break;
                            case 3: obj[dbobj_count].Gender = (string)item; break;
                            case 4: obj[dbobj_count].Age = (int)item; break;
                            case 5: obj[dbobj_count].Phone = (long)item; break;
                            case 6: obj[dbobj_count].Address = (string)item; break;
                            case 7: obj[dbobj_count].City = (string)item; break;
                            case 8: obj[dbobj_count].Emailid = (string)item; break;
                            case 9: obj[dbobj_count].Adminid = (int)item; break;
                                 
                        }

                     

                    }
                    
                    customer.Add(obj[dbobj_count]);
                    dbobj_count++;
                }





            }
            catch (Exception ex)
            {
                cnn.Close();
                CMSLogger.SetProperties("AuthToken", message.Adminid.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, this.ToString());
                CMSLogger.log.Error(ex.Message,ex);
                throw ex;

            }
            
            return customer;
        } //get detailsAll end

        public int DeleteDetails(Retrieve_message message)
        { //DELETE details begin

            //log
            CMSLogger.SetProperties("AuthToken", message.Adminid.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, this.ToString());
            CMSLogger.log.Info("DeleteDetailsInvoked");

            int result=0;
            try
            {
                MySqlCommand cmd;
                cnn.Open();
                cmd = new MySqlCommand("deleteDetails", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@c_id", message.id);
                cmd.Parameters.AddWithValue("@c_adminid", message.Adminid);
                Console.WriteLine("\nConnection Open: record Deleted ! ");
                result = cmd.ExecuteNonQuery();
                Console.WriteLine("num of rows affected" + result);
                cnn.Close();
            }
            catch (Exception ex)
            {

                cnn.Close();
                CMSLogger.SetProperties("AuthToken", message.Adminid.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, this.ToString());
                CMSLogger.log.Error(ex.Message, ex);
                throw ex;

            }
            return result;
        }//END OF DELETE DETAILS

        public int SetDetails(CustomerDataObject customerobj)
        {//SET DETAILS BEGINS
            CMSLogger.SetProperties("AuthToken", customerobj.Adminid.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, this.ToString());
            CMSLogger.log.Info("setDetailsInvoked");

            int result = 0;
            try
            {

                MySqlCommand cmd;
                cnn.Open();
                cmd = new MySqlCommand("setDetails", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", customerobj.CustomerName);
                cmd.Parameters.AddWithValue("@age", customerobj.Age);
                cmd.Parameters.AddWithValue("@gender", customerobj.Gender);
                cmd.Parameters.AddWithValue("@phone", customerobj.Phone);
                cmd.Parameters.AddWithValue("@address", customerobj.Address);
                cmd.Parameters.AddWithValue("@city", customerobj.City);
                cmd.Parameters.AddWithValue("@emailid", customerobj.Emailid);
                cmd.Parameters.AddWithValue("@adminid", customerobj.Adminid);
                result=cmd.ExecuteNonQuery();
                Console.WriteLine("num of rows affected" + result);
                cnn.Close();

            }
            catch (Exception ex)
            {
                CMSLogger.SetProperties("AuthToken", customerobj.Adminid.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, this.ToString());
                CMSLogger.log.Error(ex.Message, ex);

                cnn.Close();
                throw ;
                

            }
            return result;
        }//SET DETAILS ENDS
        
        public int UpdateDetails(CustomerDataObject customerobj)
        {//update begin
            CMSLogger.SetProperties("AuthToken", customerobj.Adminid.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, this.ToString());
            CMSLogger.log.Info("updateDetailsInvoked");

            int result=0;
            try
            {

                MySqlCommand cmd;
                cnn.Open();
                cmd = new MySqlCommand("updateDetails", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@c_adminid", customerobj.Adminid);
                cmd.Parameters.AddWithValue("@c_id", customerobj.Id);
                cmd.Parameters.AddWithValue("@c_name", customerobj.CustomerName);
                cmd.Parameters.AddWithValue("@c_age", customerobj.Age);
                cmd.Parameters.AddWithValue("@c_gender", customerobj.Gender);
                cmd.Parameters.AddWithValue("@c_phone", customerobj.Phone);
                cmd.Parameters.AddWithValue("@c_address", customerobj.Address);
                cmd.Parameters.AddWithValue("@c_city", customerobj.City);
                cmd.Parameters.AddWithValue("@c_emailid", customerobj.Emailid);
                //cmd.Parameters.AddWithValue("@adminid", obj.adminid);
                result=cmd.ExecuteNonQuery();
                Console.WriteLine("update:num of rows affected" + result);
                cnn.Close();

            }
            catch (Exception ex)
            {
                CMSLogger.SetProperties("AuthToken", customerobj.Adminid.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, this.ToString());
                CMSLogger.log.Error(ex.Message, ex);
                cnn.Close();
                throw;

            }

            return result;
        }//update end
       
    }
}
