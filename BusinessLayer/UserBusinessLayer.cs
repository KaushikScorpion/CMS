using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.CustomData;
using CMSException;


namespace BusinessLayer
{
    public class UserBusinessLayer
    {
        public Result Register(UserDataObject userObj)
        {
            try
            {
                UserDataBase User = new UserDataBase();
                Result ResultObj = new Result();
                if (Validator.IsValidEmail(userObj.Emailid) == false)
                    throw new CMSException.FormatException("Not a Valid Email");
                if (Validator.IsValidPhone(userObj.Phone) == false)
                    throw new CMSException.FormatException("Not a Valid PhoneNumber");
                if(User.Register(userObj)==1)
                {
                    ResultObj.Status = "Success";
                    ResultObj.Message = "Created User Successfully";
                }
                else
                {
                    ResultObj.Status = "Failure";
                    ResultObj.Message = "Couldnt Create User";

                }
                return ResultObj;
            }
            catch(Exception e)
            {
                if (e.Message.Contains(userObj.Emailid))
                {
                    DuplicateEmailException Dupex = new DuplicateEmailException("Email ID already Taken");
                    throw Dupex;
                }
                else if (e.Message.Contains(userObj.Phone.ToString()))
                {
                    DuplicatePhoneException Dupex = new DuplicatePhoneException("Phone Number already Taken");
                    throw Dupex;

                }
                
                else
                    throw e;
            }

        }


        public Result VerifyUser(UserDataObject UserObj)
        {
            try
            {
                UserDataBase obj = new UserDataBase();
                Result VerificationResult = new Result();
                if (UserObj.Emailid == "" || UserObj.Emailid == null)
                {
                    VerificationResult.Message = "Email ID is Required";
                    VerificationResult.Status = "Failure";
                    return VerificationResult;   
                }
                else if (UserObj.Password == "" || UserObj.Password == null)
                {
                    VerificationResult.Message = "Password is Required";
                    VerificationResult.Status = "Failure";
                    return VerificationResult; 
                }

                UserDataObject DBVerificationResponse = new UserDataObject();
                DBVerificationResponse = obj.VerifyUser(UserObj.Emailid);

                if (DBVerificationResponse.Password.Equals(UserObj.Password))
                {
                    VerificationResult.Status = "Success";
                    VerificationResult.Message = "User Successfully Logged In";
                    VerificationResult.Adminid = DBVerificationResponse.Adminid;
                    VerificationResult.UserName = DBVerificationResponse.UserName;

                }
                else
                {
                    VerificationResult.Status = "Failure";
                    VerificationResult.Message = "Wrong Credentials";
                }
                
                return VerificationResult;
            }
            catch
            {
                throw;
            }
           
        }
    }
}
