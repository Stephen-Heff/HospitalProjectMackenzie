using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectMackenzie.Models.ViewModels
{
    public class DetailsAppointment
    {
        public AppointmentDto SelectedAppointment { get; set; }
        public IEnumerable<PatientDto> Patients { get; set; }
        public IEnumerable<DoctorDto> Doctors { get; set; }
        public IEnumerable<RoomDto> Rooms { get; set; }
    }
}