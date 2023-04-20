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
    public class PaymentController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static PaymentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://Chospitalproject-env.eba-infegs3m.us-east-2.elasticbeanstalk.com/api/");
        }
        // GET: Payment/List
        public ActionResult List()
        {
            string url = "paymentdata/listpayments";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<PaymentDto> payments = response.Content.ReadAsAsync<IEnumerable<PaymentDto>>().Result;

            return View(payments);
        }

        // GET: Payment/Details/5
      
        public ActionResult Details(int id)
        {
            DetailsPayment ViewModel = new DetailsPayment();

            string url = "paymentdata/findpayment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            PaymentDto SelectedPayment = response.Content.ReadAsAsync<PaymentDto>().Result;

            ViewModel.SelectedPayment = SelectedPayment;

            return View(ViewModel);
        }

        // GET: Payment/New
        public ActionResult New()
        {
            string url = "BillData/ListBills/";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<BillDto> bills = response.Content.ReadAsAsync<IEnumerable<BillDto>>().Result;

            return View(bills);
        }

        // POST: Payment/Create
        [HttpPost]
        public ActionResult Create(Payment payment)
        {
            string url = "paymentdata/addpayment";
            string jsonpayload = jss.Serialize(payment);

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

        // GET: Payment/Edit/5
        public ActionResult Edit(int id)
        {
            UpdatePayment ViewModel = new UpdatePayment();

            string url = "paymentdata/findpayment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PaymentDto SelectedPayment = response.Content.ReadAsAsync<PaymentDto>().Result;
            ViewModel.SelectedPayment = SelectedPayment;

            url = "billdata/listbills/";
            response = client.GetAsync(url).Result;
            IEnumerable<BillDto> Bills = response.Content.ReadAsAsync<IEnumerable<BillDto>>().Result;
            ViewModel.Bills = Bills;

            return View(ViewModel);
        }

        // POST: Payment/Update/5
        [HttpPost]
        public ActionResult Update(int id, Payment payment)
        {
            string url = "paymentdata/updatepayment/" + id;
            string jsonpayload = jss.Serialize(payment);
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

        // GET: Payment/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "paymentdata/findpayment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            PaymentDto selectedPayment = response.Content.ReadAsAsync<PaymentDto>().Result;
            return View(selectedPayment);
        }

        // POST: Payment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "paymentdata/deletepayment/" + id;
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