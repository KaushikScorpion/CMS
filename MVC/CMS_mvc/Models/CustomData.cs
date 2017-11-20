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
    public class Result
    {

        [DataMember(Order = 1)]
        public string Status;
        [DataMember(EmitDefaultValue = false, Order = 2)]
        public string Message = null;
        [DataMember(EmitDefaultValue = false, Order = 2)]
        public string ExceptionType = null;
        [DataMember(EmitDefaultValue = false, Order = 3)]
        public string ExceptionDetails = null;
        [DataMember(EmitDefaultValue = false, Order = 4)]
        public int Adminid;
        [DataMember(EmitDefaultValue = false, Order = 5)]
        public string UserName;
    }

    [DataContract]
    public class retrieve_message
    {
        [DataMember]
        [Required]
        public string Searchparam {get; set;}
        [DataMember]
        [DataType(DataType.Text)]
        public string Searchvalue{get; set;}
        [DataMember]
        public int id{get; set;}

        public int Adminid{get; set;}
        [DataMember]
        public int Start_index{get; set;}
        [DataMember]
        public int End_index{get; set;}
        [DataMember]
        public string Sortparam{get; set;}
        [DataMember]
        public string token{get; set;}

    }
}