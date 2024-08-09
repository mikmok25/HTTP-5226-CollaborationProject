using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using VendorManagement2.Models;
using VendorManagement2.Models.ViewModels;

namespace VendorManagement2.Controllers
{
    public class CategoryController : Controller
    {
        private static readonly HttpClient client;

        static CategoryController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44305/api/");
        }

        // GET: Categories/List
        public ActionResult List()
        {
            string url = "CategoryData/ListCategories";
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                IEnumerable<Category> categories = response.Content.ReadAsAsync<IEnumerable<Category>>().Result;
                return View(categories);
            }
            else
            {
                return View("Error");
            }
        }

        // GET: Categories/Details/5
        public ActionResult Details(int id)
        {
            string url = "CategoryData/FindCategory/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                Category category = response.Content.ReadAsAsync<Category>().Result;
                return View(category);
            }
            else
            {
                return View("Error");
            }
        }

        // GET: Categories/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        public ActionResult Create(Category category)
        {
            string url = "CategoryData/AddCategory";
            string jsonpayload = new JavaScriptSerializer().Serialize(category);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return View("New");
            }
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "CategoryData/FindCategory/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                Category category = response.Content.ReadAsAsync<Category>().Result;
                return View(category);
            }
            else
            {
                return View("Error");
            }
        }

        // POST: Categories/Update/5
        [HttpPost]
        public ActionResult Update(Category category)
        {
            string url = "CategoryData/UpdateCategory/" + category.CategoryID;
            string jsonpayload = new JavaScriptSerializer().Serialize(category);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return View("Edit", category);
            }
        }

        // GET: Categories/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "CategoryData/FindCategory/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                Category category = response.Content.ReadAsAsync<Category>().Result;
                return View(category);
            }
            else
            {
                return View("Error");
            }
        }

        // POST: Categories/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string url = "CategoryData/DeleteCategory/" + id;

            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return View("Error");
            }
        }
    }
}
