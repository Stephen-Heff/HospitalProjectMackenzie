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
        public virtual Patient Patient { get; set; }

        //Foreign keys for Doctors
        [ForeignKey("Doctor")]
        public int DoctorID { get; set; }
        public virtual Doctor Doctor { get; set; }
        //Foreign keys for Room
        [ForeignKey("Room")]
        public int RoomID { get; set; }
        public virtual Room Room { get; set; }


       




    }

    public class AppointmentDto
    {
        public int AppointmentID { get; set; }
        public string AppointmentName { get; set; }
        public int PatientID { get; set; }

        public string PatientFirstName { get; set; }

        public string PatientLastName { get; set; }

        public string PatientDateOfBirth { get; set; }

        public string PatientCellPhone { get; set; }

        public int DoctorID { get; set; }
        public string DoctorFirstName { get; set; }

        public string DoctorLastName { get; set; }

        public int RoomID { get; set; }
        public string RoomName { get; set; }

        public int BillID { get; set; }
        public int BillAmount { get; set; }


    }

}
