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
    public class PaymentDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PaymentData/ListPayments
        [HttpGet]
        public IEnumerable<PaymentDto> ListPayments()
        {
            List<Payment> Payments = db.Payments.ToList();
            List<PaymentDto> PaymentDtos = new List<PaymentDto>();

            Payments.ForEach(p => PaymentDtos.Add(new PaymentDto()
            {

                PaymentID = p.PaymentID,
                PaymentAmount = p.PaymentAmount
            }));

            return PaymentDtos;
        }

        // GET: api/PaymentData/FindPayment/5
        [ResponseType(typeof(Payment))]
        public IHttpActionResult FindPayment(int id)
        {
            Payment Payment = db.Payments.Find(id);
            PaymentDto PaymentDto = new PaymentDto()
            {
                PaymentID = Payment.PaymentID,
                PaymentAmount = Payment.PaymentAmount
            };

            if (Payment == null)
            {
                return NotFound();
            }

            return Ok(PaymentDto);
        }

        // POST: api/PaymentData/UpdatePayment/5
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdatePayment(int id, Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != payment.PaymentID)
            {
                return BadRequest();
            }

            db.Entry(payment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
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

        // POST: api/PaymentData/AddPayment
        [ResponseType(typeof(Payment))]
        public IHttpActionResult AddPayment(Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Payments.Add(payment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = payment.PaymentID }, payment);
        }

        // POSt: api/PaymentData/DeletePayment/5
        [ResponseType(typeof(Payment))]
        public IHttpActionResult DeletePayment(int id)
        {
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return NotFound();
            }

            db.Payments.Remove(payment);
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

        private bool PaymentExists(int id)
        {
            return db.Payments.Count(e => e.PaymentID == id) > 0;
        }
    }
}