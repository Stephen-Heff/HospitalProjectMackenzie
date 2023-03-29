﻿using System;
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

        // GET: api/AmenityData/ListAmenities
        [HttpGet]
        public IEnumerable<AmenityDto> ListAmenities()
        {
            List<Amenity> Amenities = db.Amenities.ToList();
            List<AmenityDto> AmenityDtos = new List<AmenityDto>();

            SiteDataController siteDataController = new SiteDataController();
            Amenities.ForEach(a => {

                IHttpActionResult actionResult = siteDataController.FindSite(a.SiteId);
                var contentResult = actionResult as OkNegotiatedContentResult<SiteDto>;
                SiteDto siteDto = contentResult.Content;

                AmenityDtos.Add(new AmenityDto()
                {
                    AmenityID = a.AmenityID,
                    AmenityName = a.AmenityName,
                    AmenityDescription = a.AmenityDescription,
                    AmenityLocation = a.AmenityLocation,
                    AmenityType = a.AmenityType,
                    SiteID = a.SiteId,
                    SiteDto = siteDto
                });
            });
            return (AmenityDtos);
        }

        // GET: api/AmenityData/FindAmenity/5
        [ResponseType(typeof(Amenity))]
        [HttpGet]
        public IHttpActionResult FindAmenity(int id)
        {
            Amenity Amenity = db.Amenities.Find(id);
            SiteDataController siteDataController = new SiteDataController();

            IHttpActionResult actionResult = siteDataController.FindSite(Amenity.SiteId);
            var contentResult = actionResult as OkNegotiatedContentResult<SiteDto>;
            SiteDto siteDto = contentResult.Content;

            AmenityDto AmenityDto = new AmenityDto()
            {
                AmenityID = Amenity.AmenityID,
                AmenityName = Amenity.AmenityName,
                AmenityDescription = Amenity.AmenityDescription,
                AmenityLocation = Amenity.AmenityLocation,
                AmenityType = Amenity.AmenityType,
                SiteID = Amenity.SiteId,
                SiteDto = siteDto
            };

            if (Amenity == null)
            {
                return NotFound();
            }

            return Ok(AmenityDto);
        }

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
    }
}