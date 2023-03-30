using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using HospitalProjectMackenzie.Models;
using HospitalProjectMackenzie.Models.ViewModels;
using System.Web.Script.Serialization;
using System.Diagnostics;


namespace HospitalProjectMackenzie.Controllers
{
    public class SiteController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static SiteController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44388/api/sitedata/");
        }

        // GET: Site/List
        public ActionResult List()
        {
            string url = "listsites";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<SiteDto> sites = response.Content.ReadAsAsync<IEnumerable<SiteDto>>().Result;
            return View(sites);
        }

        // GET: Site/Details/5
        public ActionResult Details(int id)
        {
            string url = "findsite/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            SiteDto selectedsites = response.Content.ReadAsAsync<SiteDto>().Result;
            return View(selectedsites);


        }

        // GET: Site/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Site/Create
        [HttpPost]
        public ActionResult Create(Site site)
        {
            Debug.WriteLine("Create");
            string url = "addsite";


            string jsonpayload = jss.Serialize(site);


            HttpContent content = new StringContent(jsonpayload);
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

        // GET: Site/Edit/5
        public ActionResult Edit(int id)
        {
            SiteDto ViewModel = new SiteDto();

            string url = "findsite/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            SiteDto SelectedSite = response.Content.ReadAsAsync<SiteDto>().Result;
            ViewModel = SelectedSite;

            return View(ViewModel);
        }

        // POST: Site/Update/5
        [HttpPost]
        public ActionResult Update(int id, Site site)
        {
            string url = "updatesite/" + id;
            string jsonpayload = jss.Serialize(site);
            HttpContent content = new StringContent(jsonpayload);
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

        // GET: Site/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "findsite/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            SiteDto selectedSite = response.Content.ReadAsAsync<SiteDto>().Result;
            return View(selectedSite);
        }


        // POST: Site/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "deletesite/" + id;
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
    }
}
