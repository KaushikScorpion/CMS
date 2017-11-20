using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;



namespace DataLayer
{
    [DataContract]
    public class CustomerDataObject
    {

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public long Phone { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string Emailid { get; set; }
        [DataMember]
        public int Adminid { get; set; }
        public string status="success";
    }
}
