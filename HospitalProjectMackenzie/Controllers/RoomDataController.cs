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


    }
}