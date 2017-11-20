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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CMSUserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CMSUserService.svc or CMSUserService.svc.cs at the Solution Explorer and start debugging.
    public class CMSUserService : IUserService
    {
        public Result Register(UserDataObject user)
        {//registers new user

            try
            {
                
                UserBusinessLayer customerBusiness = new UserBusinessLayer();
                Result res = customerBusiness.Register(user);
                return res;

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
        
        public Result Authenticate(UserDataObject user)
        {//authenticates new user
            try
            {
                UserBusinessLayer userBusiness = new UserBusinessLayer();
                return userBusiness.VerifyUser(user);
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
                    obj.Message = "Bad Parameters";
                    throw new WebFaultException<Result>(obj, System.Net.HttpStatusCode.InternalServerError);
                }
            }
            
           
        }
    }
    }

