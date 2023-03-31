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

        /// <summary>
        /// Returns all doctors in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all doctors in the database, including their associated department.
        /// </returns>
        /// <example>
        /// GET: api/DoctorData/ListDoctors
        /// </example>
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
                DoctorEmployeeNumber = d.DoctorEmployeeNumber,
                DepartmentID = d.DepartmentID,
                DepartmentName = d.Department.DepartmentName,
            }));

            return (DoctorDtos);
        }

        /// <summary>
        /// Gathers information about all doctors related to a particular department
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all doctors in the database, including their associated department matched with a particular department ID
        /// </returns>
        /// <param name="id">Department ID.</param>
        /// <example>
        /// GET: api/DoctorData/ListDoctorsForDepartment/3
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
                DoctorEmployeeNumber = d.DoctorEmployeeNumber,
                DepartmentID = d.DepartmentID,
                DepartmentName = d.Department.DepartmentName,
            }));

            return Ok(DoctorDtos);
        }

        /// <summary>
        /// Returns a particular doctor in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A doctor in the system matching up to the doctor ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the doctor</param>
        /// <example>
        /// GET: api/DoctorData/FindDoctor/5
        /// </example>
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
                DoctorEmployeeNumber = Doctor.DoctorEmployeeNumber,
                DepartmentID = Doctor.DepartmentID,
                DepartmentName = Doctor.Department.DepartmentName,
            };

            if (Doctor == null)
            {
                return NotFound();
            }

            return Ok(DoctorDto);
        }

        /// <summary>
        /// Updates a particular doctor in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Doctor ID primary key</param>
        /// <param name="doctor">JSON FORM DATA of a doctor</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/DoctorData/UpdateDoctor/5
        /// FORM DATA: Doctor JSON Object
        /// </example>
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

        /// <summary>
        /// Adds a doctor to the system
        /// </summary>
        /// <param name="doctor">JSON FORM DATA of a doctor</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Doctor ID, Doctor Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/DoctorData/AddDoctor
        /// FORM DATA: Doctor JSON Object
        /// </example>
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

        /// <summary>
        /// Deletes a doctor from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the doctor</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/DoctorData/DeleteDoctor/5
        /// FORM DATA: (empty)
        /// </example>
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