using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProjectMackenzie.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }
        public int PaymentAmount { get; set; }




        [ForeignKey("Bill")]
        public int BillID { get; set; }
        public virtual Bill Bill { get; set; }

    }
}