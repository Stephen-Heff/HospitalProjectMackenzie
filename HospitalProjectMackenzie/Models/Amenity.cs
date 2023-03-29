using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalProjectMackenzie.Models
{
    public class Amenity
    {
        [Key]
        public int AmenityID { get; set; }
        public Site Site { get; set; }
        [ForeignKey("Site")]
        public int SiteId { get; set; }
        public string AmenityName { get; set; }
        public string AmenityLocation { get; set; }
        public string AmenityType { get; set; }
        public string AmenityDescription { get; set; }
    }

    public class AmenityDto
    {
        public int AmenityID { get; set; }
        public int SiteID { get; set; }
        public SiteDto SiteDto { get; set; }
        public string AmenityName { get; set; }
        public string AmenityLocation { get; set; }
        public string AmenityType { get; set; }
        public string AmenityDescription { get; set; }
    }
}