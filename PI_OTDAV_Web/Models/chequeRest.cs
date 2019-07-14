namespace PI_OTDAV_Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class chequeRest
    {

        public int num { get; set; }
        
        public string price { get; set; }

        public int agence { get; set; }

        [StringLength(255)]
        public string bank { get; set; }

        [StringLength(255)]

        public string image { get; set; }

        public int status { get; set; }

    }
}
