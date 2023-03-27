using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProjectMackenzie.Models
{
    public class Room
    {
        [Key]
        public int RoomID { get; set; }
        public string RoomName { get; set; }

        [ForeignKey("Department")]
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        // Navigation property for Appointments
        public virtual ICollection<Appointment> Appointments { get; set; }
    }

    public class RoomDto
    {
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
    }
}