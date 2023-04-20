using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectMackenzie.Models.ViewModels
{
    public class DetailsDoctor
    {
        public DoctorDto SelectedDoctor { get; set; }

        public IEnumerable<AppointmentDto> AssociatedAppointments { get; set; }
    }
}