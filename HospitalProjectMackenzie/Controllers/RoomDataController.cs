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
using System.Diagnostics;
using HospitalProjectMackenzie.Models;

namespace HospitalProjectMackenzie.Controllers
{
    public class RoomDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all rooms in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all rooms in the database, including their associated department.
        /// </returns>
        /// <example>
        /// GET: api/RoomData/ListRooms
        /// </example>
        [HttpGet]
        [ResponseType(typeof(RoomDto))]
        public IHttpActionResult ListRooms()
        {
            List<Room> Rooms = db.Rooms.ToList();
            List<RoomDto> RoomDtos = new List<RoomDto>();

            Rooms.ForEach(a => RoomDtos.Add(new RoomDto()
            {
                RoomID = a.RoomID,
                RoomName = a.RoomName,
                DepartmentID = a.Department.DepartmentID,
                DepartmentName = a.Department.DepartmentName
            }));

            return Ok(RoomDtos);
        }

        /// <summary>
        /// Gathers information about all rooms related to a particular department
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all rooms in the database, including their associated department matched with a particular department ID
        /// </returns>
        /// <param name="id">Department ID.</param>
        /// <example>
        /// GET: api/RoomData/ListRoomsForDepartment/3
        /// </example>
        [HttpGet]
        [ResponseType(typeof(RoomDto))]
        public IHttpActionResult ListRoomsForDepartment(int id)
        {
            List<Room> Rooms = db.Rooms.Where(a => a.DepartmentID == id).ToList();
            List<RoomDto> RoomDtos = new List<RoomDto>();

            Rooms.ForEach(a => RoomDtos.Add(new RoomDto()
            {
                RoomID = a.RoomID,
                RoomName = a.RoomName,
                DepartmentID = a.Department.DepartmentID,
                DepartmentName = a.Department.DepartmentName
            }));

            return Ok(RoomDtos);
        }


        // GET: api/RoomData/FindRoom/5
        [ResponseType(typeof(RoomDto))]
        [HttpGet]
        public IHttpActionResult FindRoom(int id)
        {
            Room Room = db.Rooms.Find(id);
            RoomDto RoomDto = new RoomDto()
            {
                RoomID = Room.RoomID,
                RoomName = Room.RoomName,
                DepartmentID = Room.Department.DepartmentID,
                DepartmentName = Room.Department.DepartmentName
            };

            if (Room == null)
            {
                return NotFound();
            }

            return Ok(RoomDto);
        }

        // POST: api/RoomData/UpdateRoom/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateRoom(int id, Room Room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Room.RoomID)
            {
                return BadRequest();
            }

            db.Entry(Room).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
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

        // POST: api/RoomData/AddRoom
        [HttpPost]
        [ResponseType(typeof(Room))]
        public IHttpActionResult AddRoom(Room Room)
        {
            Debug.WriteLine("AddRoom");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rooms.Add(Room);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Room.RoomID }, Room);
        }

        // POST: api/RoomData/DeleteRoom/5
        [ResponseType(typeof(Room))]
        [HttpPost]
        public IHttpActionResult DeleteRoom(int id)
        {
            Room Room = db.Rooms.Find(id);
            if (Room == null)
            {
                return NotFound();
            }

            db.Rooms.Remove(Room);
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

        private bool RoomExists(int id)
        {
            return db.Rooms.Count(e => e.RoomID == id) > 0;
        }
    }
}