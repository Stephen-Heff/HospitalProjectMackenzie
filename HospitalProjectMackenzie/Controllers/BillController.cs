using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Diagnostics;
using HospitalProjectMackenzie.Models;
using HospitalProjectMackenzie.Models.ViewModels;
using System.Web.Script.Serialization;


namespace HospitalProjectMackenzie.Controllers
{
    public class BillController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static BillController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://Chospitalproject-env.eba-infegs3m.us-east-2.elasticbeanstalk.com/api/");
        }
        // GET: Bill/List
        public ActionResult List()
        {
            string url = "billdata/listbills";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<BillDto> bills = response.Content.ReadAsAsync<IEnumerable<BillDto>>().Result;

            return View(bills);
        }

        // GET: Bill/Details/5     
        public ActionResult Details(int id)
        {
            DetailsBill ViewModel = new DetailsBill();

            string url = "billdata/findbill/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            BillDto SelectedBill = response.Content.ReadAsAsync<BillDto>().Result;

            ViewModel.SelectedBill = SelectedBill;

            return View(ViewModel);
        }


        // GET: Bill/New
        public ActionResult New()
        {
            string url = "AppointmentData/ListAppointments/";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<AppointmentDto> appointments = response.Content.ReadAsAsync<IEnumerable<AppointmentDto>>().Result;

            return View(appointments);
        }

        // POST: Bill/Create
        [HttpPost]
        public ActionResult Create(Bill bill)
        {
            string url = "billdata/addbill";
            string jsonpayload = jss.Serialize(bill);

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

        // GET: Bill/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateBill ViewModel = new UpdateBill();

            string url = "billdata/findbill/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            BillDto SelectedBill = response.Content.ReadAsAsync<BillDto>().Result;
            ViewModel.SelectedBill = SelectedBill;

            url = "appointmentdata/listappointments" ;
            response = client.GetAsync(url).Result;
            IEnumerable<AppointmentDto> appointments = response.Content.ReadAsAsync<IEnumerable<AppointmentDto>>().Result;
            ViewModel.AppointmentID = appointments;

            return View(ViewModel);
        }

        // POST: Bill/Update/5
        [HttpPost]

        public ActionResult Update(int id, Bill bill)
        {
            string url = "billdata/updatebill/" + id;
            string jsonpayload = jss.Serialize(bill);
            Debug.WriteLine("my bill is" + jsonpayload);
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

        // GET: Bill/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "billdata/findbill/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            BillDto selectedbill = response.Content.ReadAsAsync<BillDto>().Result;
            return View(selectedbill);
        }

        // POST: Bill/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "billdata/deletebill/" + id;
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

        public ActionResult Error()
        {
            return View();
        }

    }
}