using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace DataLayer.Interfaces
{
    public interface IUserDataBase
    {
        bool GetValidUser(int adminid);
        UserDataObject VerifyUser(string mail);
        int Register(UserDataObject obj);
    }
}
