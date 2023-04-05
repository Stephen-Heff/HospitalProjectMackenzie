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
    public class AppointmentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/AppointmentData/ListAppointments
        [HttpGet]
        public IEnumerable<AppointmentDto> ListAppointments()
        {
            List<Appointment> Appointments = db.Appointments.ToList();
            List<AppointmentDto> AppointmentDtos = new List<AppointmentDto>();

            Appointments.ForEach(a => AppointmentDtos.Add(new AppointmentDto()
            {
                AppointmentID = a.AppointmentID,
                AppointmentName = a.AppointmentName,
                PatientID = a.PatientID,
                PatientFirstName = a.Patient.PatientFirstName,
                PatientLastName = a.Patient.PatientLastName,
                PatientCellPhone = a.Patient.PatientCellPhone,
                DoctorID = a.DoctorID,
                DoctorFirstName = a.Doctor.DoctorFirstName,
                DoctorLastName = a.Doctor.DoctorLastName,
                RoomID = a.RoomID,
                RoomName = a.Room.RoomName
                
            })); ;

            return (AppointmentDtos);
        }

        /// <summary>
        /// Returns all appointments in the system associated with a particular room.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all appointments in the database responsible for a particular room
        /// </returns>
        /// <param name="id">Room Primary Key</param>
        /// <example>
        /// GET: api/AppointmentData/ListAppointmentsForRoom/1
        /// </example>
        [HttpGet]
        [ResponseType(typeof(AppointmentDto))]
        public IHttpActionResult ListAppointmentsForRoom(int id)
        {
            List<Appointment> Appointments = db.Appointments.Where(a => a.RoomID == id).ToList();
            List<AppointmentDto> AppointmentDtos = new List<AppointmentDto>();

            Appointments.ForEach(a => AppointmentDtos.Add(new AppointmentDto()
            {
                AppointmentID = a.AppointmentID,
                AppointmentName = a.AppointmentName,
                PatientID = a.PatientID,
                PatientFirstName = a.Patient.PatientFirstName,
                PatientLastName = a.Patient.PatientLastName,
                PatientCellPhone = a.Patient.PatientCellPhone,
                DoctorID = a.DoctorID,
                DoctorFirstName = a.Doctor.DoctorFirstName,
                DoctorLastName = a.Doctor.DoctorLastName,
                RoomID = a.RoomID,
                RoomName = a.Room.RoomName
            }));

            return Ok(AppointmentDtos);
        }

        /// <summary>
        /// Returns all appointments in the system associated with a particular patient.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all appointments in the database responsible for a particular patient
        /// </returns>
        /// <param name="id">Patient Primary Key</param>
        /// <example>
        /// GET: api/AppointmentData/ListAppointmentsForPatient/1
        /// </example>
        [HttpGet]
        [ResponseType(typeof(AppointmentDto))]
        public IHttpActionResult ListAppointmentsForPatient(int id)
        {
            List<Appointment> Appointments = db.Appointments.Where(a => a.PatientID == id).ToList();
            List<AppointmentDto> AppointmentDtos = new List<AppointmentDto>();

            Appointments.ForEach(a => AppointmentDtos.Add(new AppointmentDto()
            {
                AppointmentID = a.AppointmentID,
                AppointmentName = a.AppointmentName,
                PatientID = a.PatientID,
                PatientFirstName = a.Patient.PatientFirstName,
                PatientLastName = a.Patient.PatientLastName,
                PatientCellPhone = a.Patient.PatientCellPhone,
                DoctorID = a.DoctorID,
                DoctorFirstName = a.Doctor.DoctorFirstName,
                DoctorLastName = a.Doctor.DoctorLastName,
                RoomID = a.RoomID,
                RoomName = a.Room.RoomName
            }));

            return Ok(AppointmentDtos);
        }


        /// <summary>
        /// Returns all appointments in the system associated with a particular doctor.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all appointments in the database responsible for a particular doctor
        /// </returns>
        /// <param name="id">Doctor Primary Key</param>
        /// <example>
        /// GET: api/AppointmentData/ListAppointmentsForDoctor/1
        /// </example>
        [HttpGet]
        [ResponseType(typeof(AppointmentDto))]
        public IHttpActionResult ListAppointmentsForDoctor(int id)
        {
            List<Appointment> Appointments = db.Appointments.Where(a => a.DoctorID == id).ToList();
            List<AppointmentDto> AppointmentDtos = new List<AppointmentDto>();

            Appointments.ForEach(a => AppointmentDtos.Add(new AppointmentDto()
            {
                AppointmentID = a.AppointmentID,
                AppointmentName = a.AppointmentName,
                PatientID = a.PatientID,
                PatientFirstName = a.Patient.PatientFirstName,
                PatientLastName = a.Patient.PatientLastName,
                PatientCellPhone = a.Patient.PatientCellPhone,
                DoctorID = a.DoctorID,
                DoctorFirstName = a.Doctor.DoctorFirstName,
                DoctorLastName = a.Doctor.DoctorLastName,
                RoomID = a.RoomID,
                RoomName = a.Room.RoomName
            }));

            return Ok(AppointmentDtos);
        }

        // GET: api/AppointmentData/FindAppointment/5
        [ResponseType(typeof(Appointment))]
        [HttpGet]
        public IHttpActionResult FindAppointment(int id)
        {
            Appointment Appointment = db.Appointments.Find(id);
            AppointmentDto AppointmentDto = new AppointmentDto()
            {
                AppointmentID = Appointment.AppointmentID,
                AppointmentName = Appointment.AppointmentName,
                PatientID = Appointment.Patient.PatientID,
                PatientFirstName = Appointment.Patient.PatientFirstName,
                PatientLastName = Appointment.Patient.PatientLastName,
                PatientCellPhone = Appointment.Patient.PatientCellPhone,
                DoctorID = Appointment.Doctor.DoctorID,
                DoctorFirstName = Appointment.Doctor.DoctorFirstName,
                DoctorLastName = Appointment.Doctor.DoctorLastName,
                RoomID = Appointment.Room.RoomID,
                RoomName = Appointment.Room.RoomName
            };

            if (Appointment == null)
            {
                return NotFound();
            }

            return Ok(AppointmentDto);
        }

        // POST: api/AppointmentData/UpdateAppointment/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateAppointment(int id, Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appointment.AppointmentID)
            {
                return BadRequest();
            }

            db.Entry(appointment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
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

        // POST: api/AppointmentData/AddAppointment
        [HttpPost]
        [ResponseType(typeof(Appointment))]
        public IHttpActionResult AddAppointment(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Appointments.Add(appointment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = appointment.AppointmentID }, appointment);
        }

        // POST: api/AppointmentData/DeleteAppointment/5
        [ResponseType(typeof(Appointment))]
        [HttpPost]
        public IHttpActionResult DeleteAppointment(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return NotFound();
            }

            db.Appointments.Remove(appointment);
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

        private bool AppointmentExists(int id)
        {
            return db.Appointments.Count(e => e.AppointmentID == id) > 0;
        }
    }
}