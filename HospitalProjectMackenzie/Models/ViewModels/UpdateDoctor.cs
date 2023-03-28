using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectMackenzie.Models.ViewModels
{
    public class UpdateDoctor
    {
        public DoctorDto SelectedDoctor { get; set; }

        public IEnumerable<DepartmentDto> Departments { get; set; }
    }
}