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
using HospitalProjectMackenzie.Models;

namespace HospitalProjectMackenzie.Controllers
{
    public class PatientDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PatientData/ListPatients
        [HttpGet]
        public IEnumerable<PatientDto> ListPatients()
        {
            List<Patient> Patients = db.Patients.ToList();
            List<PatientDto> PatientDtos = new List<PatientDto>();

            Patients.ForEach(p => PatientDtos.Add(new PatientDto()
            {
                PatientID = p.PatientID,
                PatientFirstName = p.PatientFirstName,
                PatientLastName = p.PatientLastName,
                PatientAddress = p.PatientAddress,
                PatientDateOfBirth = p.PatientDateOfBirth,
                PatientCellPhone = p.PatientCellPhone
            }));

            return (PatientDtos);
        }

        // GET: api/PatientData/FindPatient/5
        [ResponseType(typeof(Patient))]
        [HttpGet]
        public IHttpActionResult FindPatient(int id)
        {
            Patient Patient = db.Patients.Find(id);
            PatientDto PatientDto = new PatientDto()
            {
                PatientID = Patient.PatientID,
                PatientFirstName = Patient.PatientFirstName,
                PatientLastName = Patient.PatientLastName,
                PatientAddress = Patient.PatientAddress,
                PatientDateOfBirth = Patient.PatientDateOfBirth,
                PatientCellPhone = Patient.PatientCellPhone
            };

            if (Patient == null)
            {
                return NotFound();
            }

            return Ok(PatientDto);
        }

        // POST: api/PatientData/UpdatePatient/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult PutPatient(int id, Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != patient.PatientID)
            {
                return BadRequest();
            }

            db.Entry(patient).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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

        // POST: api/PatientData/AddPatient
        [ResponseType(typeof(Patient))]
        [HttpPost]
        public IHttpActionResult AddPatient(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Patients.Add(patient);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = patient.PatientID }, patient);
        }

        // POST: api/PatientData/DeletePatient/5
        [ResponseType(typeof(Patient))]
        [HttpPost]
        public IHttpActionResult DeletePatient(int id)
        {
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }

            db.Patients.Remove(patient);
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

        private bool PatientExists(int id)
        {
            return db.Patients.Count(e => e.PatientID == id) > 0;
        }
    }
}