using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalProjectMackenzie.Models
{
    public class Doctor
    {
        public int DoctorID { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string DoctorEmployeeNumber { get; set; }
        [ForeignKey("Department")]
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }
        // Navigation property for Appointments
        public virtual ICollection<Appointment> Appointments { get; set; }
    }

    public class DoctorDto
    {
        public int DoctorID { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string DoctorEmployeeNumber { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
    }
}