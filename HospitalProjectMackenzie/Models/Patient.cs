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
        public DateTime PatientDateOfBirth { get; set; }
        public string PatientCellPhone { get; set; }

        // Navigation property for Appointments
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
