using PI_OTDAV_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PI_OTDAV_Web.Models
{
    public class EspaceModel
    {

       
        public int idEspace { get; set; }

        
        public string NomEspace { get; set; }

     
        public string adresse { get; set; }

        
        public string details { get; set; }

        public int? formulePerception_idFormuleP { get; set; }

     
        public virtual ICollection<perceptioncategory> perceptioncategory { get; set; }

        public virtual formuleperceptionModel formuleperception { get; set; }

    }
}