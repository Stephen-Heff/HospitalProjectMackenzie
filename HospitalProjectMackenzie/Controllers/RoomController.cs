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
    public class RoomController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static RoomController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44388/api/");
        }

        // GET: Room/List
        public ActionResult List()
        {
            string url = "roomdata/listrooms";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<RoomDto> rooms = response.Content.ReadAsAsync<IEnumerable<RoomDto>>().Result;
            return View(rooms);
        }

        // GET: Room/Details/5
        public ActionResult Details(int id)
        {
            string url = "roomdata/findroom/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            RoomDto selectedrooms = response.Content.ReadAsAsync<RoomDto>().Result;
            return View(selectedrooms);


        }

        // GET: Room/New
        public ActionResult New()
        {
            string url = "DepartmentData/ListDepartments/";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<DepartmentDto> departments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
            return View(departments);
        }

        // POST: Room/Create
        [HttpPost]
        public ActionResult Create(Room room)
        {
            Debug.WriteLine("Create");
            string url = "roomdata/addroom";


            string jsonpayload = jss.Serialize(room);


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

        // GET: Room/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateRoom ViewModel = new UpdateRoom();

            string url = "roomdata/findroom/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            RoomDto SelectedRoom = response.Content.ReadAsAsync<RoomDto>().Result;
            ViewModel.SelectedRoom = SelectedRoom;

            return View(ViewModel);
        }

        // POST: Room/Update/5
        [HttpPost]
        public ActionResult Update(int id, Room room)
        {
            string url = "roomdata/updateroom/" + id;
            string jsonpayload = jss.Serialize(room);
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

        // GET: Room/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "roomdata/findroom/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            RoomDto selectedRoom = response.Content.ReadAsAsync<RoomDto>().Result;
            return View(selectedRoom);
        }


        // POST: Room/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "roomdata/deleteroom/" + id;
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