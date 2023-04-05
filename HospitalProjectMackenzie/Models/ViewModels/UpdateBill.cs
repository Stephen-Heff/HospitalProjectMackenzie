﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectMackenzie.Models.ViewModels
{
    public class UpdateBill
    {
        public BillDto SelectedBill { get; set; }

        public IEnumerable<AppointmentDto> AppointmentID { get; set; }
    }
}