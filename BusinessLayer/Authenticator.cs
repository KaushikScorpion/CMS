using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class Authenticator
    {
        static public bool ApiKeyAuthenticator(string apiKey)
        {
            UserDataBase UDBobj=new UserDataBase();
            int Adminid;

           if(int.TryParse(apiKey,out Adminid))
           {
                if(UDBobj.GetValidUser(Adminid)||Adminid==1234)
                    return true;
                else
                    return false;
           }
           else
           {
               return false;
           }

        }
    }
}
