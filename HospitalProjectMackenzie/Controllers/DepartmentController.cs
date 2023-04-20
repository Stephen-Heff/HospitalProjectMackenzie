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
    public class DepartmentController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static DepartmentController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44388/api/");
        }

        // GET: Department/List
        public ActionResult List()
        {
            string url = "departmentdata/listdepartments";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<DepartmentDto> departments = response.Content.ReadAsAsync<IEnumerable<DepartmentDto>>().Result;
            return View(departments);
        }

        // GET: Department/Details/5
        public ActionResult Details(int id)
        {
            DetailsDepartment ViewModel = new DetailsDepartment();

            string url = "departmentdata/finddepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            DepartmentDto SelectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;
            Debug.WriteLine("department received : ");
            Debug.WriteLine(SelectedDepartment.DepartmentName);

            ViewModel.SelectedDepartment = SelectedDepartment;

            //show associated rooms with this department
            url = "roomdata/listroomsfordepartment/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<RoomDto> AssociatedRooms = response.Content.ReadAsAsync<IEnumerable<RoomDto>>().Result;

            ViewModel.AssociatedRooms = AssociatedRooms;

            //show responsible doctors with this department
            url = "doctordata/listdoctorsfordepartment/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<DoctorDto> ResponsibleDoctors = response.Content.ReadAsAsync<IEnumerable<DoctorDto>>().Result;

            ViewModel.ResponsibleDoctors = ResponsibleDoctors;

            //show responsible volunteers with this department
            url = "volunteerdata/listvolunteersfordepartment/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<VolunteerDto> ResponsibleVolunteers = response.Content.ReadAsAsync<IEnumerable<VolunteerDto>>().Result;

            ViewModel.ResponsibleVolunteers = ResponsibleVolunteers;

            return View(ViewModel);
        }

        // GET: Department/New
        public ActionResult New()
        {
            string url = "sitedata/listsites/";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<SiteDto> sites = response.Content.ReadAsAsync<IEnumerable<SiteDto>>().Result;
            return View(sites);
        }

        // POST: Department/Create
        [HttpPost]
        public ActionResult Create(Department department)
        {
            Debug.WriteLine("Create");
            string url = "departmentdata/adddepartment";


            string jsonpayload = jss.Serialize(department);


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

        // GET: Department/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateDepartment ViewModel = new UpdateDepartment();

            //the existing department information
            string url = "departmentdata/finddepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DepartmentDto SelectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;
            ViewModel.SelectedDepartment = SelectedDepartment;

            // all sites to choose from when updating this department
            url = "sitedata/listsites/";
            response = client.GetAsync(url).Result;
            IEnumerable<SiteDto> Sites = response.Content.ReadAsAsync<IEnumerable<SiteDto>>().Result;

            ViewModel.Sites = Sites;

            return View(ViewModel);
        }

        // POST: Department/Update/5
        [HttpPost]
        public ActionResult Update(int id, Department department)
        {
            string url = "departmentdata/updatedepartment/" + id;
            string jsonpayload = jss.Serialize(department);
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

        // GET: Department/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "departmentdata/finddepartment/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DepartmentDto selectedDepartment = response.Content.ReadAsAsync<DepartmentDto>().Result;
            return View(selectedDepartment);
        }


        // POST: Department/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "departmentdata/deletedepartment/" + id;
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