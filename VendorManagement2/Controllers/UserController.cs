using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using VendorManagement2.Models;
using VendorManagement2.Models.ViewModels;
using System.Web.Script.Serialization;

namespace VendorManagement2.Controllers
{
    public class UserController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static UserController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44324/api/");
        }

        // GET: api/UserData/ListUsers
        public ActionResult List()
        {
            string url = "userdata/listusers";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<UserDto> users = response.Content.ReadAsAsync<IEnumerable<UserDto>>().Result;

            return View(users);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            DetailsUser ViewModel = new DetailsUser();

            string url = "userdata/finduser/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                UserDto SelectedUser = response.Content.ReadAsAsync<UserDto>().Result;
                ViewModel.SelectedUser = SelectedUser;

                // Fetch events associated with the user
                url = "userdata/listeventsforuser/" + id;
                response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<EventDto> UserEvents = response.Content.ReadAsAsync<IEnumerable<EventDto>>().Result;
                    ViewModel.UserEvents = UserEvents;
                }
            }

            return View(ViewModel);
        }


        // POST: Users/Associate/{UserId}/{EventId}
        [HttpPost]
        public ActionResult Associate(int id, int EventId)
        {
            string url = "userdata/associateuserwithevent/" + id + "/" + EventId;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }

        // GET: Users/UnAssociate/{id}?EventId={EventId}
        [HttpGet]
        public ActionResult UnAssociate(int id, int EventId)
        {
            string url = "userdata/unassociateuserwithevent/" + id + "/" + EventId;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: User/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(User @user)
        {
            string url = "userdata/adduser";
            string jsonpayload = jss.Serialize(@user);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            return RedirectToAction("List");
        }

        // GET: Users/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "userdata/finduser/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            UserDto selectedUser = response.Content.ReadAsAsync<UserDto>().Result;
            return View(selectedUser);
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "userdata/deleteuser/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            UpdateUser ViewModel = new UpdateUser();

            string url = "userdata/finduser/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            UserDto SelectedUser = response.Content.ReadAsAsync<UserDto>().Result;
            ViewModel.SelectedUser = SelectedUser;

            return View(ViewModel);
        }
        // POST: Users/Update/5
        [HttpPost]
        public ActionResult Update(int id, User @user)
        {
            string url = "userdata/updateuser/" + id;
            string jsonpayload = jss.Serialize(@user);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("List");
        }

    }
}
