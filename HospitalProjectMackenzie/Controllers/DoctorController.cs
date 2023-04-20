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
    public class DoctorController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static DoctorController()
        {
            client = new HttpClient();  
            client.BaseAddress = new Uri("https://Chospitalproject-env.eba-infegs3m.us-east-2.elasticbeanstalk.com/api/");
        }

        // GET: Doctor/List
        public ActionResult List()
        {
            string url = "DoctorData/listDoctors";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<DoctorDto> Doctors = response.Content.ReadAsAsync<IEnumerable<DoctorDto>>().Result;
            return View(Doctors);
        }

        // GET: Doctor/Details/5
        public ActionResult Details(int id)
        {
            string url = "DoctorData/findDoctor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            DoctorDto selectedDoctor = response.Content.ReadAsAsync<DoctorDto>().Result;
            return View(selectedDoctor);


        }

        // GET: Doctor/New
        public ActionResult New()
        {
            string url = "DepartmentData/ListDepartments/";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<DepartmentDto> departments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
            return View(departments);
        }

        // POST: Doctor/Create
        [HttpPost]
        public ActionResult Create(Doctor Doctor)
        {
            Debug.WriteLine("Create");
            string url = "DoctorData/addDoctor";


            string jsonpayload = jss.Serialize(Doctor);


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

        // GET: Doctor/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateDoctor ViewModel = new UpdateDoctor();

            string url = "DoctorData/findDoctor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DoctorDto selectedDoctor = response.Content.ReadAsAsync<DoctorDto>().Result;
            ViewModel.SelectedDoctor = selectedDoctor;

            url = "DepartmentData/ListDepartments";
            response = client.GetAsync(url).Result;
            IEnumerable<DepartmentDto> departments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
            ViewModel.Departments = departments;

            return View(ViewModel);
        }

        // POST: Doctor/Update/5
        [HttpPost]
        public ActionResult Update(int id, Doctor Doctor)
        {
            string url = "DoctorData/updateDoctor/" + id;
            string jsonpayload = jss.Serialize(Doctor);
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

        // GET: Doctor/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "DoctorData/findDoctor/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DoctorDto selectedDoctor = response.Content.ReadAsAsync<DoctorDto>().Result;
            return View(selectedDoctor);
        }


        // POST: Doctor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "DoctorData/deleteDoctor/" + id;
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