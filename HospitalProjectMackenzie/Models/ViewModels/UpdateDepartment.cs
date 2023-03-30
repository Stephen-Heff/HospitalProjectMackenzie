using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectMackenzie.Models.ViewModels
{
    public class UpdateDepartment
    {
        public DepartmentDto SelectedDepartment { get; set; }

        public IEnumerable<SiteDto> Sites { get; set; }
    }
}