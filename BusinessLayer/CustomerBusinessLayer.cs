using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.CustomData;
using System.Web;
using CMSException;
using DataLayer.Utilities;
using DataLayer.Interfaces;


namespace BusinessLayer
{
    public class CustomerBusinessLayer
    {
        static int StorageToggler=Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["StorageToggle"]);

        static CustomerBusinessLayer()
        {
            //code to retrieve Storage toggler from config to be added

        }

        private ICustomerDataBase Toggle()
        {
            if (StorageToggler == 0)
                return new SQlCustomerDataBase();
            else
                return new JsonCustomerDataBase(); 

        }

        //retrieve ALL OR RETRIEVE ALL BY ADMINID
        public List<CustomerDataObject> RetrieveAll(Retrieve_message msg,string apiKey)
        {

            ICustomerDataBase customer = Toggle();
            List<CustomerDataObject> customerlist=new List<CustomerDataObject>();
            try
            {
                //auth
                if (Authenticator.ApiKeyAuthenticator(apiKey) == false)
                    throw new Exception("wrongkey");
                //parse api key
                msg.Adminid = int.Parse(apiKey);
                  
               // default all records if no searchparam mentioned
                if (msg.Adminid == 1234 && (msg.Searchparam == null || msg.Searchvalue == null))
                {
                    msg.Adminid = 9999;
                }

                if(msg.Searchparam==null || msg.Searchvalue== null)
                {
                    msg.Searchparam = "adminid";
                    msg.Searchvalue = msg.Adminid.ToString();
                }
                
                if (msg.Sortparam == null)
                    msg.Sortparam = "id";
                if (msg.Start_index < 0)
                    msg.Start_index = 1;
                if (msg.End_index <=0)
                    msg.End_index = 100;



                customerlist = customer.GetDetailsAll(msg);//.Searchparam,msg.Searchvalue,msg.Adminid, msg.Start_index, msg.End_index, msg.Sortparam);
                
            }
            catch (Exception e)
            {
                
                if (e.Message.Contains(msg.Searchparam))
                {
                    
                   UnknownFilterException ufex= new UnknownFilterException("Unknown Search Parameter");
                    throw ufex;
                }
                else if (e.Message.Contains(msg.Sortparam))
                {
                    UnknownFilterException ufex = new UnknownFilterException("Unknown Sort Parameter");
                    throw ufex;
                }

                else

                throw;
            }
            return customerlist;


        }

        //UPDATE BY ID
        public Result Update(CustomerDataObject customerobj, string apiKey)
        {
            ICustomerDataBase customer = Toggle();
            Result result_msg = new Result();
            try
            {
                
                if (Authenticator.ApiKeyAuthenticator(apiKey) == false)
                    throw new Exception("wrongkey");
                if (Validator.IsValidEmail(customerobj.Emailid) == false)
                    throw new CMSException.FormatException("Not a Valid Email");
                if (Validator.IsValidPhone(customerobj.Phone) == false)
                    throw new CMSException.FormatException("Not a Valid PhoneNumber");
                customerobj.Adminid = int.Parse(apiKey);
                int ResultStatus = customer.UpdateDetails(customerobj);
                if (ResultStatus == 0)
                {
                    result_msg.Status = "Failure";
                    if (customerobj.Id <= 0)
                        result_msg.Message = "Customer ID Required";
                    else
                        result_msg.Message = "No Such Customer Found";
                }
                else
                {
                    result_msg.Status = "Success";
                    result_msg.Message = "Data Updated Successfully";
                }
               
            }
            catch(Exception e)
            {
                if (e.Message.Contains(customerobj.Emailid))
                {
                    DuplicateEmailException dupex = new DuplicateEmailException("Email ID already Taken");
                    throw dupex;
                }
                else if(e.Message.Contains(customerobj.Phone.ToString()))
                {
                    DuplicatePhoneException dupex = new DuplicatePhoneException("Phone Number already Taken");
                    throw dupex;

                }
                else
                       
                throw e;
            }
            return result_msg;
        }

        //CREATE NEW CUSTOMER
        public Result Create(CustomerDataObject customerobj, string apiKey)
        {
            ICustomerDataBase customer = Toggle();
            Result result_msg = new Result();
            try
            {
                
                if (Authenticator.ApiKeyAuthenticator(apiKey) == false)
                    throw new Exception("wrongkey");
                customerobj.Adminid = int.Parse(apiKey);

                int ResultStatus = customer.SetDetails(customerobj);

                if (ResultStatus == 0)
                    result_msg.Status = "Failure";
                else
                {
                    result_msg.Status = "Success";
                    result_msg.Message = "New Customer Created Successfully";
                }
                
            }
            catch(Exception e)
            {
                if (e.Message.Contains(customerobj.Emailid))
                {
                    DuplicateEmailException dupex = new DuplicateEmailException("Email ID already Taken");
                    throw dupex;
                }
                else if (e.Message.Contains(customerobj.Phone.ToString()))
                {
                    DuplicatePhoneException dupex = new DuplicatePhoneException("Phone Number already Taken");
                    throw dupex;

                }
                else


                    throw e;
            }
            return result_msg;
        }

        //DELETE CUSTOMER BY ID
        public Result Delete(Retrieve_message msg, string apiKey)
        {
            ICustomerDataBase customer = Toggle();
            Result result_msg = new Result();
            try
            {
                
                if (Authenticator.ApiKeyAuthenticator(apiKey) == false)
                    throw new Exception("wrongkey");
                msg.Adminid = int.Parse(apiKey);
                
               

                int ResultStatus = customer.DeleteDetails(msg);
                if (ResultStatus == 0) //this is not always unauthorized so change code
                {
                    string message = "You do not own the customer";
                    CMSLogger.SetProperties("AuthToken", msg.Adminid.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, this.ToString());
                    CMSLogger.log.Info(message);
                    throw new AuthorizationException(message);
                   
                }// 
                else
                {
                    result_msg.Status = "Success";
                    result_msg.Message = "Customer Deleted Successfully";
                }

            }
            catch(Exception ex)
            {

                throw ;
            }
            return result_msg;
        }
        

    }
}
