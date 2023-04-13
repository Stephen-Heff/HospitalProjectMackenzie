using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalProjectMackenzie.Models
{
    public class Site
    {
        [Key]
        public int SiteID { get; set; }
        public string SiteName { get; set; }
        public string SiteDescription { get; set; }
        public string SiteAddress { get; set; }
        public string SiteNumber { get; set; }
        public string SiteImageUrl { get; set; }
    }

    public class SiteDto
    {
        public int SiteID { get; set; }
        public string SiteName { get; set; }
        public string SiteDescription { get; set; }
        public string SiteAddress { get; set; }
        public string SiteNumber { get; set; }
        public string SiteImageUrl { get; set; }

        public IEnumerable<AmenityDto> amenities { get; set; }
    }
}