using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using VendorManagement2.Models;
using VendorManagement2.Models.ViewModels;

namespace VendorManagement2.Controllers
{
    public class HomeController : Controller
    {
        private static readonly HttpClient client;

        static HomeController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:44324/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ActionResult Index()
        {
            EventsAndUsersViewModel viewModel = new EventsAndUsersViewModel();

            // Fetch events
            string eventsUrl = "eventdata/listevents";
            HttpResponseMessage eventsResponse = client.GetAsync(eventsUrl).Result;

            if (eventsResponse.IsSuccessStatusCode)
            {
                viewModel.Events = eventsResponse.Content.ReadAsAsync<IEnumerable<EventDto>>().Result;
            }
            else
            {
                return View("Error");
            }

            // Fetch users
            string usersUrl = "userdata/listusers";
            HttpResponseMessage usersResponse = client.GetAsync(usersUrl).Result;

            if (usersResponse.IsSuccessStatusCode)
            {
                viewModel.Users = usersResponse.Content.ReadAsAsync<IEnumerable<UserDto>>().Result;
            }
            else
            {
                return View("Error");
            }

            return View(viewModel);
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
