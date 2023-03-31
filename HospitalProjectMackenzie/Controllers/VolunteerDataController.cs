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
    public class VolunteerDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/VolunteerData/ListVolunteers
        [HttpGet]
        public IEnumerable<VolunteerDto> ListVolunteers()
        {
            List<Volunteer> Volunteers = db.Volunteers.ToList();
            List<VolunteerDto> VolunteerDtos = new List<VolunteerDto>();

            Volunteers.ForEach(v => VolunteerDtos.Add(new VolunteerDto()
            {
                VolunteerID = v.VolunteerID,
                VolunteerFirstName = v.VolunteerFirstName,
                VolunteerLastName = v.VolunteerLastName,
            }));

            return (VolunteerDtos);
        }

        /// <summary>
        /// Returns all Volunteers in the system associated with a particular department.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Volunteers in the database responsible for a particular department
        /// </returns>
        /// <param name="id">Department Primary Key</param>
        /// <example>
        /// GET: api/VolunteerData/ListVolunteersForDepartment/1
        /// </example>
        [HttpGet]
        [ResponseType(typeof(VolunteerDto))]
        public IHttpActionResult ListVolunteersForDepartment(int id)
        {
            List<Volunteer> Volunteers = db.Volunteers.Where(d => d.DepartmentID == id).ToList();
            List<VolunteerDto> VolunteerDtos = new List<VolunteerDto>();

            Volunteers.ForEach(v => VolunteerDtos.Add(new VolunteerDto()
            {
                VolunteerID = v.VolunteerID,
                VolunteerFirstName = v.VolunteerFirstName,
                VolunteerLastName = v.VolunteerLastName
            }));

            return Ok(VolunteerDtos);
        }

        // GET: api/VolunteerData/FindVolunteer/5
        [ResponseType(typeof(Volunteer))]
        [HttpGet]
        public IHttpActionResult FindVolunteer(int id)
        {
            Volunteer Volunteer = db.Volunteers.Find(id);
            VolunteerDto VolunteerDto = new VolunteerDto()
            {
                VolunteerID = Volunteer.VolunteerID,
                VolunteerFirstName = Volunteer.VolunteerFirstName,
                VolunteerLastName = Volunteer.VolunteerLastName,
            };

            if (Volunteer == null)
            {
                return NotFound();
            }

            return Ok(VolunteerDto);
        }

        // POST: api/VolunteerData/UpdateVolunteer/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateVolunteer(int id, Volunteer Volunteer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Volunteer.VolunteerID)
            {
                return BadRequest();
            }

            db.Entry(Volunteer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VolunteerExists(id))
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

        // POST: api/VolunteerData/AddVolunteer
        [HttpPost]
        [ResponseType(typeof(Volunteer))]
        public IHttpActionResult AddVolunteer(Volunteer Volunteer)
        {
            Debug.WriteLine("AddVolunteer");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Volunteers.Add(Volunteer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Volunteer.VolunteerID }, Volunteer);
        }

        // POST: api/VolunteerData/DeleteVolunteer/5
        [ResponseType(typeof(Volunteer))]
        [HttpPost]
        public IHttpActionResult DeleteVolunteer(int id)
        {
            Volunteer Volunteer = db.Volunteers.Find(id);
            if (Volunteer == null)
            {
                return NotFound();
            }

            db.Volunteers.Remove(Volunteer);
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

        private bool VolunteerExists(int id)
        {
            return db.Volunteers.Count(e => e.VolunteerID == id) > 0;
        }
    }
}