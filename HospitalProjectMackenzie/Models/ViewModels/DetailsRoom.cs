using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectMackenzie.Models.ViewModels
{
    public class DetailsRoom
    {
        public RoomDto SelectedRoom { get; set; }
        public IEnumerable<AppointmentDto> ScheduledAppointments { get; set; }
    }
}