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
    public class DoctorDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DoctorData/ListDoctors
        [HttpGet]
        public IEnumerable<DoctorDto> ListDoctors()
        {
            List<Doctor> Doctors = db.Doctors.ToList();
            List<DoctorDto> DoctorDtos = new List<DoctorDto>();

            Doctors.ForEach(d => DoctorDtos.Add(new DoctorDto()
            {
                DoctorID = d.DoctorID,
                DoctorFirstName = d.DoctorFirstName,
                DoctorLastName = d.DoctorLastName,
                DoctorEmployeeNumber = d.DoctorEmployeeNumber
            }));

            return (DoctorDtos);
        }

        /// <summary>
        /// Returns all Doctors in the system associated with a particular department.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Doctors in the database responsible for a particular department
        /// </returns>
        /// <param name="id">Department Primary Key</param>
        /// <example>
        /// GET: api/DoctorData/ListDoctorsForDepartment/1
        /// </example>
        [HttpGet]
        [ResponseType(typeof(DoctorDto))]
        public IHttpActionResult ListDoctorsForDepartment(int id)
        {
            List<Doctor> Doctors = db.Doctors.Where(d => d.DepartmentID == id).ToList();
            List<DoctorDto> DoctorDtos = new List<DoctorDto>();

            Doctors.ForEach(d => DoctorDtos.Add(new DoctorDto()
            {
                DoctorID = d.DoctorID,
                DoctorFirstName = d.DoctorFirstName,
                DoctorLastName = d.DoctorLastName,
                DoctorEmployeeNumber = d.DoctorEmployeeNumber
            }));

            return Ok(DoctorDtos);
        }

        // GET: api/DoctorData/FindDoctor/5
        [ResponseType(typeof(Doctor))]
        [HttpGet]
        public IHttpActionResult FindDoctor(int id)
        {
            Doctor Doctor = db.Doctors.Find(id);
            DoctorDto DoctorDto = new DoctorDto()
            {
                DoctorID = Doctor.DoctorID,
                DoctorFirstName = Doctor.DoctorFirstName,
                DoctorLastName = Doctor.DoctorLastName,
                DoctorEmployeeNumber = Doctor.DoctorEmployeeNumber
            };

            if (Doctor == null)
            {
                return NotFound();
            }

            return Ok(DoctorDto);
        }

        // POST: api/DoctorData/UpdateDoctor/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateDoctor(int id, Doctor Doctor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Doctor.DoctorID)
            {
                return BadRequest();
            }

            db.Entry(Doctor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(id))
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

        // POST: api/DoctorData/AddDoctor
        [HttpPost]
        [ResponseType(typeof(Doctor))]
        public IHttpActionResult AddDoctor(Doctor Doctor)
        {
            Debug.WriteLine("AddDoctor");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Doctors.Add(Doctor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Doctor.DoctorID }, Doctor);
        }

        // POST: api/DoctorData/DeleteDoctor/5
        [ResponseType(typeof(Doctor))]
        [HttpPost]
        public IHttpActionResult DeleteDoctor(int id)
        {
            Doctor Doctor = db.Doctors.Find(id);
            if (Doctor == null)
            {
                return NotFound();
            }

            db.Doctors.Remove(Doctor);
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

        private bool DoctorExists(int id)
        {
            return db.Doctors.Count(e => e.DoctorID == id) > 0;
        }
    }
}