namespace PI_OTDAV_Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("basepi.paiment")]
    public partial class paimentModel
    {
        public int ID { get; set; }

        public DateTime? Date { get; set; }

        public int Price { get; set; }

        public int Status { get; set; }

        [StringLength(255)]
        public string Title { get; set; }
        
        public string Type { get; set; }

        public int? bankcard_IdBank { get; set; }

        public int? cheque_IdCheque { get; set; }

        public int? oeuvreDec_id { get; set; }

        public int? userId { get; set; }

        public int? virement_IdVirement { get; set; }
        

        public virtual chequeModel cheque { get; set; }
        

        public virtual User user { get; set; }

        public virtual virementModel virement { get; set; }
    }
}
