using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProjectMackenzie.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        [ForeignKey("Site")]
        public int SiteID { get; set; }
        public virtual Site Site { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Volunteer> Volunteers { get; set; }
    }

    public class DepartmentDto
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int SiteID { get; set; }
        public string SiteName { get; set; }
        public string SiteDescription { get; set; }
        public string SiteAddress { get; set; }
        public string SiteNumber { get; set; }
        public string SiteImageUrl { get; set; }
    }
}