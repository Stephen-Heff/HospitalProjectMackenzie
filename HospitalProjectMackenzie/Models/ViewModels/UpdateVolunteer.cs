using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectMackenzie.Models.ViewModels
{
    public class UpdateVolunteer
    {
        public VolunteerDto SelectedVolunteer { get; set; }

        public IEnumerable<DepartmentDto> Departments { get; set; }
    }
}