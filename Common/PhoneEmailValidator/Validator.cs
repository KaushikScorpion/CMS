using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BusinessLayer
{
    public class Validator
    {
        public static bool IsValidEmail(string email)
        {
            Regex Regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = Regex.Match(email);
            if (match.Success)
                return true;
            else
                return  false;
        }

        public static bool IsValidPhone(long phone)
        {
            if(phone.ToString().Length==10)
                return true;
            else
                return false;
        }
    }
}
