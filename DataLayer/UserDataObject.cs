using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using System.ServiceModel;

namespace DataLayer

{   
    [DataContract]
    public class UserDataObject
    {
        [DataMember]
        public int Adminid { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public string Dob { get; set; }
        [DataMember]
        public long Phone { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string Emailid { get; set; }
        [DataMember]
        public string Password { get; set; }
       
    }
}
