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
using System.Web.Http.Results;
using HospitalProjectMackenzie.Models;


namespace HospitalProjectMackenzie.Controllers
{
    public class AmenityDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Returns all amenities in the system.
        /// </summary>
        /// <returns>
        /// all amenities in the database.
        /// </returns>
        /// <example>
        /// GET: api/AmenityData/ListAmenities
        /// </example>
        
        // GET: api/AmenityData/ListAmenities
        [HttpGet]
        public IEnumerable<AmenityDto> ListAmenities()
        {
            List<Amenity> Amenities = db.Amenities.ToList();
            List<AmenityDto> AmenityDtos = new List<AmenityDto>();

            Amenities.ForEach(a => {

                AmenityDtos.Add(new AmenityDto()
                {
                    AmenityID = a.AmenityID,
                    AmenityName = a.AmenityName,
                    AmenityDescription = a.AmenityDescription,
                    AmenityLocation = a.AmenityLocation,
                    AmenityType = a.AmenityType,
                    SiteID = a.SiteId,
                    SiteName = a.Site.SiteName
                });
            }); 
            return (AmenityDtos);
        }

        /// <summary>
        /// Returns an amenities with a particular ID in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An amenity in the system matching up to the amenity ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the amenity</param>
        /// <example>
        /// GET: api/AmenityData/FindAmenity/5
        /// </example>

        // GET: api/AmenityData/FindAmenity/5
        [ResponseType(typeof(Amenity))]
        [HttpGet]
        public IHttpActionResult FindAmenity(int id)
        {
            Amenity Amenity = db.Amenities.Find(id);
            AmenityDto AmenityDto = new AmenityDto()
            {
                AmenityID = Amenity.AmenityID,
                AmenityName = Amenity.AmenityName,
                AmenityDescription = Amenity.AmenityDescription,
                AmenityLocation = Amenity.AmenityLocation,
                AmenityType = Amenity.AmenityType,
                SiteID = Amenity.SiteId,
                SiteName = Amenity.Site.SiteName
            };

            if (Amenity == null)
            {
                return NotFound();
            }

            return Ok(AmenityDto);
        }

        /// <summary>
        /// Updates a particular amenity in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Amenity ID primary key</param>
        /// <param name="Amenity">JSON FORM DATA of an amenity</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/AmenityData/UpdateAmenity/5
        /// FORM DATA: Amenity JSON Object
        /// </example>
        
        // POST: api/AmenityData/UpdateAmenity/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateAmenity(int id, Amenity Amenity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Amenity.AmenityID)
            {
                return BadRequest();
            }

            db.Entry(Amenity).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AmenityExists(id))
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
        /// Adds an amenity to the system
        /// </summary>
        /// <param name="amenity">JSON FORM DATA of an amenity</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Amenity ID, Amenity Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/AmenityData/AddAmenity
        /// FORM DATA: Amenity JSON Object
        /// </example>
        
        // POST: api/AmenityData/AddAmenity
        [HttpPost]
        [ResponseType(typeof(Amenity))]
        public IHttpActionResult AddAmenity(Amenity amenity)
        {
            Debug.WriteLine("AddAmenity");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Amenities.Add(amenity);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = amenity.AmenityID }, amenity);
        }

        /// <summary>
        /// Deletes an amenity from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the amenity</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/AmenityData/DeleteAmenity/5
        /// FORM DATA: (empty)
        /// </example>
        
        // POST: api/AmenityData/DeleteAmenity/5
        [ResponseType(typeof(Amenity))]
        [HttpPost]
        public IHttpActionResult DeleteAmenity(int id)
        {
            Amenity amenity = db.Amenities.Find(id);
            if (amenity == null)
            {
                return NotFound();
            }

            db.Amenities.Remove(amenity);
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

        private bool AmenityExists(int id)
        {
            return db.Amenities.Count(e => e.AmenityID == id) > 0;
        }

        /// <summary>
        /// Gathers information about all Amenities related to a particular site
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// Content: List of AmenityDto
        /// </returns>
        /// <param name="id">Site ID.</param>
        /// <example>
        /// GET: api/AmenityData/ListAmenitiesForSite/1
        /// </example>
        /// 
        [HttpGet]
        [ResponseType(typeof(AmenityDto))]
        public IHttpActionResult ListAmenitiesForSite(int id)
        {
            List<Amenity> Amenities = db.Amenities.Where(d => d.SiteId == id).ToList();
            List<AmenityDto> AmenityDtos = new List<AmenityDto>();

            Amenities.ForEach(d => AmenityDtos.Add(new AmenityDto()
            {
                AmenityID = d.AmenityID,
                AmenityName = d.AmenityName,
                AmenityDescription = d.AmenityDescription,
                AmenityLocation = d.AmenityLocation,
                AmenityType = d.AmenityType,
                SiteID = d.SiteId,
                SiteName = d.Site.SiteName
            }));

            return Ok(AmenityDtos);
        }
    }
}