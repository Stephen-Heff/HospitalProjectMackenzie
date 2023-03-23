using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectMackenzie.Models
{
    public class Room
    {
        public int RoomID { get; set; }
        public string RoomName { get; set; }

        // Navigation property for Appointments
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}