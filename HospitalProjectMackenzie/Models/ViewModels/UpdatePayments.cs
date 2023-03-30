using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProjectMackenzie.Models.ViewModels
{
    public class UpdatePayment
    {
        public PaymentDto SelectedPayment { get; set; }

        public IEnumerable<BillDto> BillID { get; set; }
    }
}