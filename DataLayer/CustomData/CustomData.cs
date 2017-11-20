using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
namespace DataLayer.CustomData
{
    [DataContract]
    public class Result
    {

        [DataMember(Order=1)]
        public string Status ;
        [DataMember(EmitDefaultValue = false,Order=2)]
        public string Message= null;
        [DataMember(EmitDefaultValue = false,Order=2)]
        public string ExceptionType = null;
        [DataMember(EmitDefaultValue = false,Order=3)]
        public string ExceptionDetails = null;
        [DataMember(EmitDefaultValue = false, Order = 3)]
        public int Adminid = 0;
        [DataMember(EmitDefaultValue = false, Order = 4)]
        public string UserName;
    }

    [DataContract]
    public class Retrieve_message
    {
        [DataMember]
        public string Searchparam;
        [DataMember]
        public string Searchvalue;
        [DataMember]
        public int id;
        
        public int Adminid;
        [DataMember]
        public int Start_index;
        [DataMember]
        public int End_index;
        [DataMember]
        public string Sortparam;
        [DataMember]
        public string token;
       
    }
}
