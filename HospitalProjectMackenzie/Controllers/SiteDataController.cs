using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HospitalProjectMackenzie.Models;

namespace HospitalProjectMackenzie.Controllers
{
    public class SiteDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all Sites in the system.
        /// </summary>
        /// <returns>
        /// all Sites in the database, including their associated species.
        /// </returns>
        /// <example>
        /// GET: api/SiteData/ListSites
        /// </example>

        // GET: api/SiteData/ListSites
        [HttpGet]
        public IEnumerable<SiteDto> ListSites()
        {
            List<Site> Sites = db.Sites.ToList();
            List<SiteDto> SiteDtos = new List<SiteDto>();

            Sites.ForEach(a => SiteDtos.Add(new SiteDto()
            {
                SiteID = a.SiteID,
                SiteName = a.SiteName,
                SiteDescription = a.SiteDescription,
                SiteAddress = a.SiteAddress,
                SiteNumber = a.SiteNumber,
                SiteImageUrl = a.SiteImageUrl
            }));

            return (SiteDtos);
        }

        /// <summary>
        /// Returns a stie by it's id.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An site in the system matching up to the site ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the site</param>
        /// <example>
        /// GET: api/SiteData/FindSite/5
        /// </example>
        
        // GET: api/SiteData/FindSite/5
        [ResponseType(typeof(Site))]
        [HttpGet]
        public IHttpActionResult FindSite(int id)
        {
            Site Site = db.Sites.Find(id);
            SiteDto SiteDto = new SiteDto()
            {
                SiteID = Site.SiteID,
                SiteName = Site.SiteName,
                SiteDescription = Site.SiteDescription,
                SiteAddress = Site.SiteAddress,
                SiteNumber = Site.SiteNumber,
                SiteImageUrl = Site.SiteImageUrl
            };

            if (Site == null)
            {
                return NotFound();
            }

            return Ok(SiteDto);
        }

        /// <summary>
        /// Updates a particular site in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Site ID primary key</param>
        /// <param name="Site">JSON FORM DATA of an site</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/SiteData/UpdateSite/5
        /// FORM DATA: Site JSON Object
        /// </example>
        
        // POST: api/SiteData/UpdateSite/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateSite(int id, Site Site)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Site.SiteID)
            {
                return BadRequest();
            }

            db.Entry(Site).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteExists(id))
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
        /// Adds an site to the system
        /// </summary>
        /// <param name="site">JSON FORM DATA of an site</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Site ID, Site Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/SiteData/AddSite
        /// FORM DATA: Site JSON Object
        /// </example>
        
        // POST: api/SiteData/AddSite
        [HttpPost]
        [ResponseType(typeof(Site))]
        public IHttpActionResult AddSite(Site site)
        {
            Debug.WriteLine("AddSite");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sites.Add(site);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = site.SiteID }, site);
        }

        /// <summary>
        /// Deletes an site from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the site</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/SiteData/DeleteSite/5
        /// FORM DATA: (empty)
        /// </example>
        
        // POST: api/SiteData/DeleteSite/5
        [ResponseType(typeof(Site))]
        [HttpPost]
        public IHttpActionResult DeleteSite(int id)
        {
            Site site = db.Sites.Find(id);
            if (site == null)
            {
                return NotFound();
            }

            db.Sites.Remove(site);
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

        private bool SiteExists(int id)
        {
            return db.Sites.Count(e => e.SiteID == id) > 0;
        }
    }
}