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
    public class EventController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static EventController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44305/api/");
        }

        // GET: api/EventData/ListEvents
        public ActionResult List()
        {
            string url = "eventdata/listevents";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<EventDto> events = response.Content.ReadAsAsync<IEnumerable<EventDto>>().Result;

            return View(events);
        }

        // GET: Events/Details/5
        public ActionResult Details(int id)
        {
            DetailsEvent ViewModel = new DetailsEvent();

            string url = "eventdata/findevent/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            EventDto SelectedEvent = response.Content.ReadAsAsync<EventDto>().Result;
            ViewModel.SelectedEvent = SelectedEvent;

            // Fetch users associated with the event
            url = "eventdata/listusersforevent/" + id;
            response = client.GetAsync(url).Result;
            Debug.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<UserDto> EventUsers = response.Content.ReadAsAsync<IEnumerable<UserDto>>().Result;
                Debug.WriteLine(EventUsers);
                ViewModel.AssociatedUsers = EventUsers;
            }

            // Fetch users not associated with the event
            url = "eventdata/listusersnotinevent/" + id;
            response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<UserDto> AvailableUsers = response.Content.ReadAsAsync<IEnumerable<UserDto>>().Result;
                ViewModel.AvailableUsers = AvailableUsers;
            }



            return View(ViewModel);
        }

        // POST: Events/Associate/{EventId}/{UserId}
        [HttpPost]
        public ActionResult Associate(int id, int UserId)
        {
            string url = "eventdata/associateeventwithuser/" + id + "/" + UserId;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }

        // GET: Events/UnAssociate/{id}?UserId={UserId}
        [HttpGet]
        public ActionResult UnAssociate(int id, int UserId)
        {
            string url = "eventdata/unassociateeventwithuser/" + id + "/" + UserId;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Events/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        public ActionResult Create(Event @event)
        {
            string url = "eventdata/addevent";
            string jsonpayload = jss.Serialize(@event);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            
            return RedirectToAction("List");
            
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateEvent ViewModel = new UpdateEvent();

            string url = "eventdata/findevent/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            EventDto SelectedEvent = response.Content.ReadAsAsync<EventDto>().Result;
            ViewModel.SelectedEvent = SelectedEvent;

            return View(ViewModel);
        }

        // POST: Events/Update/5
        [HttpPost]
        public ActionResult Update(int id, Event @event)
        {
            string url = "eventdata/updateevent/" + id;
            string jsonpayload = jss.Serialize(@event);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("List");
            
        }

        // GET: Events/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "eventdata/findevent/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            EventDto selectedevent = response.Content.ReadAsAsync<EventDto>().Result;
            return View(selectedevent);
        }

        // POST: Events/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "eventdata/deleteevent/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;


            return RedirectToAction("List");
            
        }
    }
}