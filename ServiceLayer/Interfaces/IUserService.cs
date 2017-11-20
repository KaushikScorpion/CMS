using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DataLayer;
using DataLayer.CustomData;

namespace CMSService
{
    [ServiceContract]
    public interface IUserService
    {

        //user services
        [OperationContract]
        [WebInvoke(UriTemplate = "/authenticate/", Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        Result Authenticate(UserDataObject user);


        [OperationContract]
        [WebInvoke(UriTemplate = "/register/", Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        Result Register(UserDataObject user);
        
    }
}
