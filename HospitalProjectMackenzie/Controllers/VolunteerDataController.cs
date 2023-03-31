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

        /// <summary>
        /// Returns all volunteers in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all volunteers in the database, including their associated department.
        /// </returns>
        /// <example>
        /// GET: api/VolunteerData/ListVolunteers
        /// </example>
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
                DepartmentID = v.DepartmentID,
                DepartmentName = v.Department.DepartmentName,
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
                VolunteerLastName = v.VolunteerLastName,
                DepartmentID = v.DepartmentID,
                DepartmentName = v.Department.DepartmentName,
            }));

            return Ok(VolunteerDtos);
        }

        /// <summary>
        /// Returns a particular volunteer in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A volunteer in the system matching up to the volunteer ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the volunteer</param>
        /// <example>
        /// GET: api/VolunteerData/FindVolunteer/5
        /// </example>
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
                DepartmentID = Volunteer.DepartmentID,
                DepartmentName = Volunteer.Department.DepartmentName,
            };

            if (Volunteer == null)
            {
                return NotFound();
            }

            return Ok(VolunteerDto);
        }

        /// <summary>
        /// Updates a particular volunteer in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Volunteer ID primary key</param>
        /// <param name="volunteer">JSON FORM DATA of a volunteer</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/VolunteerData/UpdateVolunteer/5
        /// FORM DATA: Volunteer JSON Object
        /// </example>
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

        /// <summary>
        /// Adds a volunteer to the system
        /// </summary>
        /// <param name="volunteer">JSON FORM DATA of a volunteer</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Volunteer ID, Volunteer Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/VolunteerData/AddVolunteer
        /// FORM DATA: Volunteer JSON Object
        /// </example>
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

        /// <summary>
        /// Deletes a volunteer from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the volunteer</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/VolunteerData/DeleteVolunteer/5
        /// FORM DATA: (empty)
        /// </example>
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