using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectMackenzie.Models.ViewModels
{
    public class UpdateAmenity
    {
        public AmenityDto SelectedAmenity { get; set; }
        public IEnumerable<SiteDto> SiteOptions { get; set; }
    }
}