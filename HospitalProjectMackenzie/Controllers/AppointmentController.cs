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
    public class AppointmentController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static AppointmentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44388/api/appointmentdata/");
        }

        // GET: Appointment/List
        public ActionResult List()
        {
            string url = "listappointments";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<AppointmentDto> appointments = response.Content.ReadAsAsync<IEnumerable<AppointmentDto>>().Result;
            return View(appointments);
        }

        // GET: Appointment/Details/5
        public ActionResult Details(int id)
        {
            string url = "findappointment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            AppointmentDto selectedappointments = response.Content.ReadAsAsync<AppointmentDto>().Result;
            return View(selectedappointments);


        }

        // GET: Appointment/New
        public ActionResult New()
        {
            string url = "listappointments/";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<PatientDto> patients = response.Content.ReadAsAsync<IEnumerable<PatientDto>>().Result;
            return View(patients);

            url = "listappointments/";
            response = client.GetAsync(url).Result;
            IEnumerable<DoctorDto> doctors = response.Content.ReadAsAsync<IEnumerable<DoctorDto>>().Result;
            return View(doctors);

            url = "listappointments/";
            response = client.GetAsync(url).Result;
            IEnumerable<RoomDto> rooms = response.Content.ReadAsAsync<IEnumerable<RoomDto>>().Result;
            return View(rooms);
        }

        // POST: Appointment/Create
        [HttpPost]
        public ActionResult Create(Appointment appointment)
        {
            string url = "addappointment";


            string jsonpayload = jss.Serialize(appointment);


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

        // GET: Appointment/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateAppointment ViewModel = new UpdateAppointment();


            string url = "findappointment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            AppointmentDto SelectedAppointment = response.Content.ReadAsAsync<AppointmentDto>().Result;
            ViewModel.SelectedAppointment = SelectedAppointment;


            url = "listappointments/";
            response = client.GetAsync(url).Result;
            IEnumerable<PatientDto> patients = response.Content.ReadAsAsync<IEnumerable<PatientDto>>().Result;
            ViewModel.PatientOptions = patients;

            url = "listappointments/";
            response = client.GetAsync(url).Result;
            IEnumerable<DoctorDto> doctors = response.Content.ReadAsAsync<IEnumerable<DoctorDto>>().Result;
            ViewModel.DoctorOptions = doctors;


            url = "listappointments/";
            response = client.GetAsync(url).Result;
            IEnumerable<RoomDto> rooms = response.Content.ReadAsAsync<IEnumerable<RoomDto>>().Result;
            ViewModel.RoomOptions = rooms;

            return View(ViewModel);


          

          

           




        }

        // POST: Appointment/Update/5
        [HttpPost]
        public ActionResult Update(int id, Appointment appointment)
        {
            string url = "appointmentdata/updateappointment/" + id;
            string jsonpayload = jss.Serialize(appointment);
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

        // GET: Appointment/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "appointmentdata/findappointment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            AppointmentDto selectedappointment = response.Content.ReadAsAsync<AppointmentDto>().Result;
            return View(selectedappointment);
        }

        // POST: Appointment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "appointmentdata/deleteappointment/" + id;
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