using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProjectMackenzie.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }
        public string AppointmentName { get; set; }




        // a patient belongs to one appointment
        // an appointment can have many patients
        //Foreign keys for Patients

        [ForeignKey("Patient")]
        public int PatientID { get; set; }
        public virtual Appointment Patient { get; set; }

        //Foreign keys for Doctors
        [ForeignKey("Doctor")]
        public int DoctorID { get; set; }
        public virtual Appointment Doctor { get; set; }
        //Foreign keys for Room
        [ForeignKey("Room")]
        public int RoomID { get; set; }
        public virtual Appointment Room { get; set; }


    }
}