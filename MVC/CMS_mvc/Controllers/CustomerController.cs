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
    public class CustomerController : Controller
    {
    
        #region Create
        public ActionResult Create()
        {
            if (!Request.Cookies.AllKeys.Contains("Adminid"))
                return RedirectToAction("Login", "User");
            ViewBag.message = "empty";
            return View();
        }

        [HttpPost]
        public ActionResult Create(CustomerDataModel CustomerObj)
        {
            string URL = ConfigurationManager.AppSettings["CustomerServiceURL"].ToString();
            string ApiKey = Crypto.Decrypt(Request.Cookies["Adminid"].Value);
            ViewBag.message = "empty";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            var response = JsonConvert.DeserializeObject<Result>(client.PostAsJsonAsync("create/" + ApiKey, CustomerObj).Result.Content.ReadAsStringAsync().Result);
            return Json(response, JsonRequestBehavior.AllowGet); 
        } 
        #endregion

        #region Retrieve
        public ActionResult Search()
        {

            if (Request.Cookies.AllKeys.Contains("Adminid"))
            {
                retrieve_message Model = new retrieve_message();
                Model.Start_index = 0;
                Model.End_index = 10;
                ViewBag.isfirst = false;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["CustomerServiceURL"].ToString());
                string ApiKey = Crypto.Decrypt(Request.Cookies["Adminid"].Value);
                var response = JsonConvert.DeserializeObject<List<CustomerDataModel>>(client.PostAsJsonAsync("retrieveAll/" + ApiKey, Model).Result.Content.ReadAsStringAsync().Result);
                ViewBag.jsonlist = response;

                retrieve_message countfinder = new retrieve_message();
                countfinder.Searchparam = Model.Searchparam;
                countfinder.Searchvalue = Model.Searchvalue;
                var countresponse = JsonConvert.DeserializeObject<List<CustomerDataModel>>(client.PostAsJsonAsync("retrieveAll/" + ApiKey, countfinder).Result.Content.ReadAsStringAsync().Result);
                int count = countresponse.Count;
                ViewBag.count = count;
                ViewBag.isfirst = true;

                return View(Model);
            }
            else
                return RedirectToAction("Login", "User");

       }

        public ActionResult _PartialSearchResults(retrieve_message post)
        {
            //for chrome
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

            // Stop Caching in Firefox
            Response.Cache.SetNoStore();

            //filtering post to find search param
            if (post.Searchvalue != null)
            {

                if (post.Searchvalue == "male" || post.Searchvalue == "female")
                    post.Searchparam = "Gender";
                else if (post.Searchvalue.Contains("@") && post.Searchvalue.Contains("."))
                    post.Searchparam = "Emailid";
                else
                {
                    post.Searchvalue = "%" + post.Searchvalue + "%";
                    post.Searchparam = "CustomerName";
                }
            }

            ViewBag.isfirst = false;
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri(ConfigurationManager.AppSettings["CustomerServiceURL"].ToString());
            string ApiKey = Crypto.Decrypt(Request.Cookies["Adminid"].Value);
            var response = JsonConvert.DeserializeObject<List<CustomerDataModel>>(Client.PostAsJsonAsync("retrieveAll/" + ApiKey, post).Result.Content.ReadAsStringAsync().Result);
            ViewBag.jsonlist = response;
            //finding count
            retrieve_message Countfinder = new retrieve_message();
            Countfinder.Searchparam = post.Searchparam;
            Countfinder.Searchvalue = post.Searchvalue;
            var countresponse = JsonConvert.DeserializeObject<List<CustomerDataModel>>(Client.PostAsJsonAsync("retrieveAll/" + ApiKey, Countfinder).Result.Content.ReadAsStringAsync().Result);
            int count = countresponse.Count;
            ViewBag.count = count;
            return View("PartialSearchResults");

        }

        public ActionResult PartialSearchResults(retrieve_message post)
        {
            

            //for chrome
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

            // Stop Caching in Firefox
            Response.Cache.SetNoStore();

            //filtering post to find search param
            if (post.Searchvalue != null)
            {

                if (post.Searchvalue == "male" || post.Searchvalue == "female")
                    post.Searchparam = "Gender";
                else if (post.Searchvalue.Contains("@") && post.Searchvalue.Contains("."))
                    post.Searchparam = "Emailid";
                else
                {
                    post.Searchvalue = "%" + post.Searchvalue + "%";
                    post.Searchparam = "CustomerName";
                }
            }

            ViewBag.isfirst = false;
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri(ConfigurationManager.AppSettings["CustomerServiceURL"].ToString());
            string ApiKey = Crypto.Decrypt(Request.Cookies["Adminid"].Value);
            var response = JsonConvert.DeserializeObject<List<CustomerDataModel>>(Client.PostAsJsonAsync("retrieveAll/" + ApiKey, post).Result.Content.ReadAsStringAsync().Result);
            ViewBag.jsonlist = response;
            //finding count
            retrieve_message Countfinder = new retrieve_message();
            Countfinder.Searchparam = post.Searchparam;
            Countfinder.Searchvalue = post.Searchvalue;
            var countresponse = JsonConvert.DeserializeObject<List<CustomerDataModel>>(Client.PostAsJsonAsync("retrieveAll/" + ApiKey, Countfinder).Result.Content.ReadAsStringAsync().Result);
            int count = countresponse.Count;
            ViewBag.count = count;
            return View("PartialSearchResults");

        }
        #endregion

        #region Update
        [HttpPost]
        public ActionResult DoUpdate(CustomerDataModel customerObj)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["CustomerServiceURL"].ToString());
            string ApiKey = Crypto.Decrypt(Request.Cookies["Adminid"].Value);

            var response = JsonConvert.DeserializeObject<Result>(client.PostAsJsonAsync("update/" + ApiKey, customerObj).Result.Content.ReadAsStringAsync().Result);

            return Json(response, JsonRequestBehavior.AllowGet); 
        }

        public ActionResult Update()
        {
            
            if (!Request.Cookies.AllKeys.Contains("Adminid"))
                return RedirectToAction("Login", "User");
            string CustomerId = Request.Form["CustomerId"];
            string ApiKey = Crypto.Decrypt(Request.Cookies["Adminid"].Value);
            string ApiEndPoint = "retrieveAll/" + ApiKey;
            ViewBag.isfirst = false;
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri(ConfigurationManager.AppSettings["CustomerServiceURL"].ToString());
            retrieve_message Post = new retrieve_message();
            Post.Searchparam = "id";
            Post.Searchvalue = CustomerId;
            var Response = JsonConvert.DeserializeObject<List<CustomerDataModel>>(Client.PostAsJsonAsync(ApiEndPoint, Post).Result.Content.ReadAsStringAsync().Result);
            return View(Response.First<CustomerDataModel>());

        }

        #endregion

        #region Delete
        public JsonResult Delete(retrieve_message msgobj)
        {
            string ApiKey = Crypto.Decrypt(Request.Cookies["Adminid"].Value);
            HttpClient Client = new HttpClient();
            Client.BaseAddress = new Uri(ConfigurationManager.AppSettings["CustomerServiceURL"].ToString());
            retrieve_message Msg = new retrieve_message();
            Msg.id = msgobj.id;
            var Response = JsonConvert.DeserializeObject<Result>(Client.PostAsJsonAsync("delete/" + ApiKey, Msg).Result.Content.ReadAsStringAsync().Result);
            return Json(Response, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
