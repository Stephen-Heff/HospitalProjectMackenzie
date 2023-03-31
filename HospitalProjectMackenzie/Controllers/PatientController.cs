using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using HospitalProjectMackenzie.Models;
using HospitalProjectMackenzie.Models.ViewModels;
using System.Web.Script.Serialization;


namespace HospitalProjectMackenzie.Controllers
{
    public class PatientController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static PatientController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44388/api/patientdata/");
        }
        // GET: Patient/List
        public ActionResult List()
        {

            string url = "listpatients";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<PatientDto> patients = response.Content.ReadAsAsync<IEnumerable<PatientDto>>().Result;
            return View(patients);
        }

        // GET: Patient/Details/5
        public ActionResult Details(int id)
        {
            string url = "findpatient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            PatientDto selectedpatients = response.Content.ReadAsAsync<PatientDto>().Result;
            return View(selectedpatients);


        }

        // GET: Patient/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        public ActionResult Create(Patient patient)
        {
            string url = "addpatient";


            string jsonpayload = jss.Serialize(patient);


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

        // GET: Patient/Edit/5
        public ActionResult Edit(int id)
        {
            PatientDto ViewModel = new PatientDto();


            string url = "findpatient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PatientDto SelectedPatient = response.Content.ReadAsAsync<PatientDto>().Result;
            ViewModel = SelectedPatient;



            return View(ViewModel);
        }

        // POST: Patient/Update/5
        [HttpPost]
        public ActionResult Update(int id, Patient patient)
        {
            string url = "updatepatient/" + id;
            string jsonpayload = jss.Serialize(patient);
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

        // GET: Patient/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "findpatient/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PatientDto selectedpatient = response.Content.ReadAsAsync<PatientDto>().Result;
            return View(selectedpatient);
        }

        // POST: Patient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "deletepatient/" + id;
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
