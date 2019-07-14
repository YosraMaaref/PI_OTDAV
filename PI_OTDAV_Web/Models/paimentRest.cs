using System;
using System.ComponentModel.DataAnnotations;

namespace PI_OTDAV_Web.Models
{
    public class paimentRest
    {

        public int? date { get; set; }

        public int price { get; set; }

        public int status { get; set; }

        [StringLength(255)]
        public string title { get; set; }
        

        public virtual chequeRest cheque { get; set; }


        public virtual UserRest user { get; set; }

        public virtual virementRest virement { get; set; }
    }
}