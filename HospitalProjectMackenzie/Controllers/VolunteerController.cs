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
    public class VolunteerController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static VolunteerController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://Chospitalproject-env.eba-infegs3m.us-east-2.elasticbeanstalk.com/api/");
        }

        // GET: Volunteer/List
        public ActionResult List()
        {
            string url = "VolunteerData/listVolunteers";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<VolunteerDto> Volunteers = response.Content.ReadAsAsync<IEnumerable<VolunteerDto>>().Result;
            return View(Volunteers);
        }

        // GET: Volunteer/Details/5
        public ActionResult Details(int id)
        {
            string url = "VolunteerData/findVolunteer/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            VolunteerDto selectedVolunteer = response.Content.ReadAsAsync<VolunteerDto>().Result;
            return View(selectedVolunteer);


        }

        // GET: Volunteer/New
        public ActionResult New()
        {
            string url = "DepartmentData/ListDepartments/";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<DepartmentDto> departments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
            return View(departments);
        }

        // POST: Volunteer/Create
        [HttpPost]
        public ActionResult Create(Volunteer Volunteer)
        {
            Debug.WriteLine("Create");
            string url = "VolunteerData/addVolunteer";


            string jsonpayload = jss.Serialize(Volunteer);


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

        // GET: Volunteer/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateVolunteer ViewModel = new UpdateVolunteer();

            string url = "VolunteerData/findVolunteer/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            VolunteerDto selectedVolunteer = response.Content.ReadAsAsync<VolunteerDto>().Result;
            ViewModel.SelectedVolunteer = selectedVolunteer;

            url = "DepartmentData/ListDepartments";
            response = client.GetAsync(url).Result;
            IEnumerable<DepartmentDto> departments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
            ViewModel.Departments = departments;

            return View(ViewModel);
        }

        // POST: Volunteer/Update/5
        [HttpPost]
        public ActionResult Update(int id, Volunteer Volunteer)
        {
            string url = "VolunteerData/updateVolunteer/" + id;
            string jsonpayload = jss.Serialize(Volunteer);
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

        // GET: Volunteer/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "VolunteerData/findVolunteer/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            VolunteerDto selectedVolunteer = response.Content.ReadAsAsync<VolunteerDto>().Result;
            return View(selectedVolunteer);
        }


        // POST: Volunteer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "VolunteerData/deleteVolunteer/" + id;
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