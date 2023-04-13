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
    public class BillDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/BillData/ListBills
        [HttpGet]
        public IEnumerable<BillDto> ListBills()
        {
            List<Bill> Bills = db.Bills.ToList();
            List<BillDto> BillDtos = new List<BillDto>();

            Bills.ForEach(b => BillDtos.Add(new BillDto()
            {
                BillID = b.BillID,
                BillAmount = b.BillAmount,
                AppointmentID = b.Appointment.AppointmentID,
                AppointmentName = b.Appointment.AppointmentName
            }));


            return BillDtos;
        }


        [HttpGet]
        [ResponseType(typeof(BillDto))]
        public IHttpActionResult ListBillsForAppointment(int id)
        {
            List<Bill> Bill = db.Bills.Where(b => b.AppointmentID == id).ToList();
            List<BillDto> BillDtos = new List<BillDto>();

            Bill.ForEach(b => BillDtos.Add(new BillDto()
            {
                BillID = b.BillID,
                BillAmount = b.BillAmount,
                AppointmentID = b.Appointment.AppointmentID,
                AppointmentName = b.Appointment.AppointmentName
            }));

            return Ok(BillDtos);
        }
        // GET: api/BillData/FindBill/5
        [ResponseType(typeof(Bill))]
        [HttpGet]
        public IHttpActionResult FindBill(int id)
        {
            Bill Bill = db.Bills.Find(id);
            BillDto BillDto = new BillDto()
            {
                BillID = Bill.BillID,
                BillAmount = Bill.BillAmount,
                AppointmentID = Bill.Appointment.AppointmentID
            };

            if (Bill == null)
            {
                return NotFound();
            }

            return Ok(BillDto);
        }

        // POST: api/BillData/UpdateBill/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateBill(int id, Bill bill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bill.BillID)
            {
                return BadRequest();
            }

            db.Entry(bill).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillExists(id))
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

        // POST: api/BillData/AddBill
        [ResponseType(typeof(Bill))]
        [HttpPost]
        public IHttpActionResult AddBill(Bill bill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bills.Add(bill);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bill.BillID }, bill);
        }

        // POST: api/BillData/DeleteBill/5
        [ResponseType(typeof(Bill))]
        [HttpPost]
        public IHttpActionResult DeleteBill(int id)
        {
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return NotFound();
            }

            db.Bills.Remove(bill);
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

        private bool BillExists(int id)
        {
            return db.Bills.Count(e => e.BillID == id) > 0;
        }
    }
}