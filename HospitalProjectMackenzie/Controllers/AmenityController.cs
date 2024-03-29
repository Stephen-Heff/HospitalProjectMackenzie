﻿using System;
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
    public class AmenityController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static AmenityController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44388/api/");
        }

        // GET: Amenity/List
        public ActionResult List()
        {
            string url = "amenitydata/listamenities";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<AmenityDto> amenities = response.Content.ReadAsAsync<IEnumerable<AmenityDto>>().Result;
            return View(amenities);
        }

        // GET: Amenity/Details/5
        public ActionResult Details(int id)
        {
            string url = "amenitydata/findamenity/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            AmenityDto selectedamenities = response.Content.ReadAsAsync<AmenityDto>().Result;
            return View(selectedamenities);


        }

        // GET: Amenity/New
        public ActionResult New()
        {
            string url = "sitedata/listsites/";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<SiteDto> sites = response.Content.ReadAsAsync<IEnumerable<SiteDto>>().Result;
            return View(sites);
        }

        // POST: Amenity/Create
        [HttpPost]
        public ActionResult Create(Amenity amenity)
        {

            string url = "amenitydata/addamenity";

            string jsonpayload = jss.Serialize(amenity);

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

        // GET: Amenity/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateAmenity ViewModel = new UpdateAmenity();

            string url = "amenitydata/findamenity/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            AmenityDto SelectedAmenity = response.Content.ReadAsAsync<AmenityDto>().Result;
            ViewModel.SelectedAmenity = SelectedAmenity;

            url = "sitedata/listsites/";
            response = client.GetAsync(url).Result;
            IEnumerable<SiteDto> sites = response.Content.ReadAsAsync<IEnumerable<SiteDto>>().Result;
            ViewModel.SiteOptions = sites;

            return View(ViewModel);
        }

        // POST: Amenity/Update/5
        [HttpPost]
        public ActionResult Update(int id, Amenity amenity)
        {
            string url = "amenitydata/updateamenity/" + id;
            string jsonpayload = jss.Serialize(amenity);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            Debug.WriteLine(jsonpayload);
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

        // GET: Amenity/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "amenitydata/findamenity/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            AmenityDto selectedAmenity = response.Content.ReadAsAsync<AmenityDto>().Result;
            return View(selectedAmenity);
        }


        // POST: Amenity/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "amenitydata/deleteamenity/" + id;
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