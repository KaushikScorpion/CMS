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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "/retrieveAll/{apiKey}", Method = "POST", ResponseFormat = WebMessageFormat.Json,RequestFormat=WebMessageFormat.Json)]
        List<CustomerDataObject> RetrieveAll(Retrieve_message msg, string apiKey);

        [OperationContract]
        [WebInvoke(UriTemplate = "/delete/{apiKey}", Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        Result Delete(Retrieve_message msg, string apiKey);

        [OperationContract]
        [WebInvoke(UriTemplate = "/create/{apiKey}", Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        Result Create(CustomerDataObject customer, string apiKey);

        [OperationContract]
        [WebInvoke(UriTemplate = "/update/{apiKey}", Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        Result Update(CustomerDataObject customer, string apiKey);

    }





   

    

  
}
