using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSException
{
    public class AuthorizationException:Exception
    {
        public string Name = "AuthorizationException";
        public AuthorizationException(string message)
           : base(message)
        {
        }

    }
    
}
