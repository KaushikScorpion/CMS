using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSException
{
    public class DuplicateEmailException:Exception
    {
        public string Name = "DuplicateEmailException";
        public DuplicateEmailException(string message):base(message)
        {
            
        }
        
    }
}
