using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSException
{
    public class DuplicatePhoneException:Exception
    {
        public string Name = "DuplicatePhoneException";
        public DuplicatePhoneException(string message):base(message)
        {
            
        }
        
    }
}
