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

            Departments.ForEach(a => DepartmentDtos.Add(new DepartmentDto()
            {
                DepartmentID = a.DepartmentID,
                DepartmentName = a.DepartmentName,
                SiteID = a.Site.SiteID,
                SiteName = a.Site.SiteName,
                SiteDescription = a.Site.SiteDescription,
                SiteAddress = a.Site.SiteAddress,
                SiteNumber = a.Site.SiteNumber,
                SiteImageUrl = a.Site.SiteImageUrl
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
            List<Department> Departments = db.Departments.Where(a => a.SiteID == id).ToList();
            List<DepartmentDto> DepartmentDtos = new List<DepartmentDto>();

            Departments.ForEach(a => DepartmentDtos.Add(new DepartmentDto()
            {
                DepartmentID = a.DepartmentID,
                DepartmentName = a.DepartmentName,
                SiteID = a.Site.SiteID,
                SiteName = a.Site.SiteName,
                SiteDescription = a.Site.SiteDescription,
                SiteAddress = a.Site.SiteAddress,
                SiteNumber = a.Site.SiteNumber,
                SiteImageUrl = a.Site.SiteImageUrl
            }));

            return Ok(DepartmentDtos);
        }
    }
}