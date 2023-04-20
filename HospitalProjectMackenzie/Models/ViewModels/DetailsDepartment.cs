using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectMackenzie.Models.ViewModels
{
    public class DetailsDepartment
    {
        public DepartmentDto SelectedDepartment { get; set; }
        public IEnumerable<RoomDto> AssociatedRooms { get; set; }
        public IEnumerable<DoctorDto> ResponsibleDoctors { get; set; }
        public IEnumerable<VolunteerDto> ResponsibleVolunteers { get; set; }
    }
}