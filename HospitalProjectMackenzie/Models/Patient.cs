using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HospitalProjectMackenzie.Models
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientAddress { get; set; }
        public string PatientDateOfBirth { get; set; }
        public string PatientCellPhone { get; set; }

        // Navigation property for Appointments
        public virtual ICollection<Appointment> Appointments { get; set; }
    }


    public class PatientDto
    {
        public int PatientID { get; set; }
        public string PatientFirstName { get; set; }

        public string PatientLastName { get; set; }
        public string PatientAddress { get; set; }

        public string PatientDateOfBirth { get; set; }

        public string PatientCellPhone { get; set; }

        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public int DoctorID { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }

    }

}




