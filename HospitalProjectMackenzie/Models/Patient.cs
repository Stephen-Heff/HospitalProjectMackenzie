using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectMackenzie.Models
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientAddress { get; set; }
        public int PatientDateOfBirth { get; set; }
        public int PatientCellPhone { get; set; }

    }
}
