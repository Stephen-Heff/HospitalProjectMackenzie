using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectMackenzie.Models.ViewModels
{
	public class UpdateAppointment
	{
		public AppointmentDto SelectedAppointment { get; set; }

		public IEnumerable<PatientDto> PatientOptions { get; set; }

		public IEnumerable<RoomDto> RoomOptions { get; set; }
		public IEnumerable<DoctorDto> DoctorOptions { get; set; }

	}
}