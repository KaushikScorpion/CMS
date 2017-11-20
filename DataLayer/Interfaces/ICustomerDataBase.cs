using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.CustomData;

namespace DataLayer.Interfaces
{
    public interface ICustomerDataBase
    {
        List<CustomerDataObject> GetDetailsAll(Retrieve_message msg);
        int DeleteDetails(Retrieve_message msg);
        int SetDetails(CustomerDataObject obj);
        int UpdateDetails(CustomerDataObject obj);

    }
}
