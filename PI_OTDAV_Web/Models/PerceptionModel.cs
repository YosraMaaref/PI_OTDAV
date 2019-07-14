using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PI_OTDAV_Web.Models
{
    public class PerceptionModel
    {
       
        public int idPerception { get; set; }

      
        public bool Statuts { get; set; }

        public double montant { get; set; }

        public double montantotale { get; set; }
        [JsonIgnore]
        public int? OeuvreD_id { get; set; }
        [JsonIgnore]
        public int? idFormuleP { get; set; }

        public virtual formuleperceptionModel formuleperception { get; set; }

        public virtual oeuvredeclaration oeuvreD { get; set; }


    }
}