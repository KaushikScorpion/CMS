using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ComponentModel.DataAnnotations;

namespace CMS_mvc.Models
{
    [DataContract]
    public class CustomerDataModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [Required]
        [StringLength(40, ErrorMessage = "Can be atmost 40 characters long")]
        public string CustomerName { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public int Age { get; set; }
        

        [DataMember]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public long Phone { get; set; }
        [DataMember]
        [Required]
        [StringLength(40, ErrorMessage = "Can be atmost 40 characters long")]
        public string Address { get; set; }
        [DataMember]
        [Required]
        public string City { get; set; }

        
        [DataMember]
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Emailid { get; set; }

        [DataMember]
        public int Adminid { get; set; }
        public string status = "success";



    }
}