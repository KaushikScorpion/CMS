using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ComponentModel.DataAnnotations;

namespace CMS_mvc.Models
{
    [DataContract]
    public class UserDataModel
    {

        [DataMember]
        public int Adminid { get; set; }
        [DataMember]
        [StringLength(40, ErrorMessage = "Can be atmost 40 characters long")]
        public string UserName { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public string Dob { get; set; }



        [DataMember]
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public long Phone { get; set; }

        
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string City { get; set; }

        [DataMember]
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Emailid { get; set; }
        [StringLength(10, ErrorMessage = "The {0} must be atleast {2} characters long and maximum 10 characters.", MinimumLength = 6)]
        [DataMember]
        [Required]
        public string Password { get; set; }
        
   
        [Required(ErrorMessage = "Confirm Password required")]
        [CompareAttribute("Password", ErrorMessage = "Password doesn't match.")] 
        public string ConfirmPassword { get; set; }
    }
}