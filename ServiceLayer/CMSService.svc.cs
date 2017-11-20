using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using BusinessLayer;
using DataLayer;
using DataLayer.CustomData;
using DataLayer.Utilities;
using System.Web;
using System.Net;


namespace CMSService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class CMSService : ICustomerService
    {
      
        #region Public Methods      ##
      
        public List<CustomerDataObject> RetrieveAll(Retrieve_message msg,string apiKey)
        {
            BusinessLayer.CustomerBusinessLayer BLObject = new BusinessLayer.CustomerBusinessLayer();
            try
            {  
                return BLObject.RetrieveAll(msg,apiKey);
            }
            catch (Exception e)
            {
                #region catchblock
                if (e.Message == "wrongkey")
                {
                    Result ErrorObj = new Result() { };
                    ErrorObj.Status = "Failure";
                    ErrorObj.ExceptionDetails = "Invalid Api Key";
                    ErrorObj.ExceptionType = "AuthorizationException";
                    throw new WebFaultException<Result>(ErrorObj, System.Net.HttpStatusCode.Unauthorized);
                }
                else
                {
                    Result ErrorObj = new Result();
                    ErrorObj.Status = "Failure";
                    ErrorObj.ExceptionType = e.GetType().ToString().Split('.')[1];
                    ErrorObj.ExceptionDetails = e.Message;
                    throw new WebFaultException<Result>(ErrorObj, System.Net.HttpStatusCode.InternalServerError);
                } 
                #endregion
            }
           
        }

        public Result Delete(Retrieve_message message,string apiKey)
        {
            
            try
            {
            BusinessLayer.CustomerBusinessLayer b=new BusinessLayer.CustomerBusinessLayer();
            return b.Delete(message,apiKey);
             }
            catch(Exception e)
            {
                if (e.Message == "wrongkey")
                {
                    Result ErrorObj = new Result();
                    ErrorObj.Status = "Failure";
                    ErrorObj.ExceptionDetails = "Invalid Api Key"; 
                    ErrorObj.ExceptionType = "AuthorizationException";
                    throw new WebFaultException<Result>(ErrorObj, System.Net.HttpStatusCode.Unauthorized);
                }
                else
                {
                    Result obj = new Result();
                    obj.Status = "Failure";
                    obj.ExceptionType = e.GetType().ToString().Split('.')[1];
                    obj.ExceptionDetails = e.Message;
                    throw new WebFaultException<Result>(obj, System.Net.HttpStatusCode.InternalServerError);
                }
            }
        }

        public Result Create(CustomerDataObject customer,string apiKey)
        {
            try
            {
               
                BusinessLayer.CustomerBusinessLayer b = new BusinessLayer.CustomerBusinessLayer();
                return b.Create(customer,apiKey);
            }
            catch(Exception e)
            {
                //Result message = new Result();
                //message.result = e.Message;
                //return message;
                if (e.Message == "wrongkey")
                {
                    // throw new WebFaultException<string>(e.Message, System.Net.HttpStatusCode.InternalServerError);



                    Result ErrorObj = new Result();
                    ErrorObj.Status = "Failure";
                    ErrorObj.ExceptionDetails = "Invalid Api Key"; 
                    ErrorObj.ExceptionType = "AuthorizationException";
                    throw new WebFaultException<Result>(ErrorObj, System.Net.HttpStatusCode.Unauthorized);

                }
                   
                else if(e.Message.Contains("null"))
                {
                    Result obj = new Result();
                    obj.Status = "Failure";
                    obj.ExceptionType = "RequiredFieldException";
                    obj.ExceptionDetails = "Required Field Cannot be null";
                    throw new WebFaultException<Result>(obj, System.Net.HttpStatusCode.InternalServerError);
                }
                else
                {
                    Result obj = new Result();
                    obj.Status = "Failure";
                    obj.ExceptionType = e.GetType().ToString().Split('.')[1];
                    obj.ExceptionDetails = e.Message;
                    throw new WebFaultException<Result>(obj, System.Net.HttpStatusCode.InternalServerError);
                }//throw new WebFaultException<string>("Duplicate Entry", System.Net.HttpStatusCode.InternalServerError);
            }
            
        }

        public Result Update(CustomerDataObject customer,string apiKey)
        {
            try
            {

            BusinessLayer.CustomerBusinessLayer BLObject = new BusinessLayer.CustomerBusinessLayer();
            return BLObject.Update(customer, apiKey);
            
            }
            catch (Exception e)
            {
                if (e.Message == "wrongkey")
                {
                    Result ErrorObj = new Result();
                    ErrorObj.Status = "Failure";
                    ErrorObj.ExceptionDetails = "Invalid Api Key"; 
                    ErrorObj.ExceptionType = "AuthorizationException";
                    throw new WebFaultException<Result>(ErrorObj, System.Net.HttpStatusCode.Unauthorized);

                }
                else if (e.Message.Contains("null"))
                {
                    Result obj = new Result();
                    obj.Status = "Failure";
                    obj.ExceptionType = "RequiredFieldException";
                    obj.ExceptionDetails = "Required Field Cannot be null";
                    throw new WebFaultException<Result>(obj, System.Net.HttpStatusCode.InternalServerError);
                }  
                else
                {
                    Result obj = new Result();
                    obj.Status = "Failure";
                    obj.ExceptionType = e.GetType().ToString().Split('.')[1];
                    obj.ExceptionDetails = e.Message;
                    throw new WebFaultException<Result>(obj, System.Net.HttpStatusCode.InternalServerError);

                }
            }
        }

        #endregion

    }   
}
