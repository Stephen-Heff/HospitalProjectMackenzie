using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectMackenzie.Models.ViewModels
{
    public class CombinedData
    {
        public IEnumerable<PatientDto> PatientData { get; set; }
        public IEnumerable<DoctorDto> DoctorData { get; set; }
        public IEnumerable<RoomDto> RoomData { get; set; }
    }
}