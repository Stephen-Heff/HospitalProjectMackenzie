using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalProjectMackenzie.Models
{
    public class Volunteer
    {
        public int VolunteerID { get; set; }
        public string VolunteerFirstName { get; set; }
        public string VolunteerLastName { get; set; }
        [ForeignKey("Department")]
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }
    }

    public class VolunteerDto
    {
        public int VolunteerID { get; set; }
        public string VolunteerFirstName { get; set; }
        public string VolunteerLastName { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
    }
}