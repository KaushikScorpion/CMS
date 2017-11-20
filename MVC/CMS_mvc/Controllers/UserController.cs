using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMS_mvc.Models;
using System.Runtime.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;
using Jv.Json;
using Newtonsoft.Json;
using System.Configuration;

namespace CMS_mvc.Controllers
{
    public class UserController : Controller
    {

        #region Login

       
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Authenticate(UserDataModel userObj)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UserServiceURL"].ToString());
            var response = JsonConvert.DeserializeObject<Result>(Client.PostAsJsonAsync("authenticate", userObj).Result.Content.ReadAsStringAsync().Result);
            HttpCookie Admin = new HttpCookie("Adminid", Crypto.Encrypt(response.Adminid.ToString()));
            Response.SetCookie(Admin);
            HttpCookie User = new HttpCookie("User", response.UserName);
            Response.SetCookie(User);
            if (response.Status == "Success")
            {
                return Json(response);
            }
            else
                return Json(response);
        } 
        #endregion

        #region Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoRegister(UserDataModel userObj)
        {
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UserServiceURL"].ToString());
            var Response = JsonConvert.DeserializeObject<Result>(Client.PostAsJsonAsync("register", userObj).Result.Content.ReadAsStringAsync().Result);
            return Json(Response, JsonRequestBehavior.AllowGet); ;
        }
        
        #endregion

        #region Logout
        public ActionResult Logout()
        {
            HttpCookie Admin = new HttpCookie("Adminid", "0");
            Admin.Expires = DateTime.Now.AddDays(-1);
            Response.SetCookie(Admin);
            return RedirectToAction("Login", "User");
        } 
        #endregion

    }
}
