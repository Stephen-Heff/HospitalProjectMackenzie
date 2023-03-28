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