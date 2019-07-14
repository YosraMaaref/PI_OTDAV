namespace PI_OTDAV_Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("basepi.virement")]
    public partial class virementModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdVirement { get; set; }

        public int agence { get; set; }

        [StringLength(255)]
        public string bank { get; set; }

        [StringLength(255)]
        public string codeVirement { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<paimentModel> paiment { get; set; }
    }
}
