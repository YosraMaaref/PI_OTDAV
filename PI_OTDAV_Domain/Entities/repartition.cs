namespace PI_OTDAV_Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("basepi.repartition")]
    public partial class repartition
    {
        [Key]
        public int idRepartition { get; set; }

        public double benificeOTDAV { get; set; }

        public double benificeUser { get; set; }

        public double impot { get; set; }

        public double netPercue { get; set; }

        public int? perception_idPerception { get; set; }

        public virtual perciption perciption { get; set; }
    }
}
