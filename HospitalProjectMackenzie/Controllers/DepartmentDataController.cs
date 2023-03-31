using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Diagnostics;
using HospitalProjectMackenzie.Models;
using System.Web.Http.Results;

namespace HospitalProjectMackenzie.Controllers
{
    public class DepartmentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all departments in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all departments in the database, including their associated site.
        /// </returns>
        /// <example>
        /// GET: api/DepartmentData/ListDepartments
        /// </example>
        [HttpGet]
        [ResponseType(typeof(DepartmentDto))]
        public IHttpActionResult ListDepartments()
        {
            List<Department> Departments = db.Departments.ToList();
            List<DepartmentDto> DepartmentDtos = new List<DepartmentDto>();

            Departments.ForEach(d => DepartmentDtos.Add(new DepartmentDto()
            {
                DepartmentID = d.DepartmentID,
                DepartmentName = d.DepartmentName,
                SiteID = d.Site.SiteID,
                SiteName = d.Site.SiteName,
                SiteDescription = d.Site.SiteDescription,
                SiteAddress = d.Site.SiteAddress,
                SiteNumber = d.Site.SiteNumber,
                SiteImageUrl = d.Site.SiteImageUrl
            }));

            return Ok(DepartmentDtos);
        }

        /// <summary>
        /// Gathers information about all departments related to a particular site
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all departments in the database, including their associated site matched with a particular site ID
        /// </returns>
        /// <param name="id">Site ID.</param>
        /// <example>
        /// GET: api/DepartmentData/ListDepartmentsForSite/3
        /// </example>
        [HttpGet]
        [ResponseType(typeof(DepartmentDto))]
        public IHttpActionResult ListDepartmentsForSite(int id)
        {
            List<Department> Departments = db.Departments.Where(d => d.SiteID == id).ToList();
            List<DepartmentDto> DepartmentDtos = new List<DepartmentDto>();

            Departments.ForEach(d => DepartmentDtos.Add(new DepartmentDto()
            {
                DepartmentID = d.DepartmentID,
                DepartmentName = d.DepartmentName,
                SiteID = d.Site.SiteID,
                SiteName = d.Site.SiteName,
                SiteDescription = d.Site.SiteDescription,
                SiteAddress = d.Site.SiteAddress,
                SiteNumber = d.Site.SiteNumber,
                SiteImageUrl = d.Site.SiteImageUrl
            }));

            return Ok(DepartmentDtos);
        }

        /// <summary>
        /// Returns all departments in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An department in the system matching up to the department ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the department</param>
        /// <example>
        /// GET: api/DepartmentData/FindDepartment/5
        /// </example>
        [ResponseType(typeof(DepartmentDto))]
        [HttpGet]
        public IHttpActionResult FindDepartment(int id)
        {
            Department Department = db.Departments.Find(id);
            DepartmentDto DepartmentDto = new DepartmentDto()
            {
                DepartmentID = Department.DepartmentID,
                DepartmentName = Department.DepartmentName,
                SiteID = Department.Site.SiteID,
                SiteName = Department.Site.SiteName,
                SiteDescription = Department.Site.SiteDescription,
                SiteAddress = Department.Site.SiteAddress,
                SiteNumber = Department.Site.SiteNumber,
                SiteImageUrl = Department.Site.SiteImageUrl
            };
            if (Department == null)
            {
                return NotFound();
            }

            return Ok(DepartmentDto);
        }

        /// <summary>
        /// Updates a particular department in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Department ID primary key</param>
        /// <param name="department">JSON FORM DATA of an department</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/DepartmentData/UpdateDepartment/5
        /// FORM DATA: Department JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateDepartment(int id, Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != department.DepartmentID)
            {

                return BadRequest();
            }

            db.Entry(department).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Adds an department to the system
        /// </summary>
        /// <param name="department">JSON FORM DATA of an department</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Department ID, Department Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/DepartmentData/AddDepartment
        /// FORM DATA: Department JSON Object
        /// </example>
        [ResponseType(typeof(Department))]
        [HttpPost]
        public IHttpActionResult AddDepartment(Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Departments.Add(department);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = department.DepartmentID }, department);
        }

        /// <summary>
        /// Deletes an department from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the department</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/DepartmentData/DeleteDepartment/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Department))]
        [HttpPost]
        public IHttpActionResult DeleteDepartment(int id)
        {
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            db.Departments.Remove(department);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartmentExists(int id)
        {
            return db.Departments.Count(e => e.DepartmentID == id) > 0;
        }
    }
}