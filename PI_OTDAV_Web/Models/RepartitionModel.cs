using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PI_OTDAV_Web.Models
{
    public class RepartitionModel
    {

        public int idRepartition { get; set; }

        public double benificeOTDAV { get; set; }

        public double benificeUser { get; set; }

        public double impot { get; set; }

        public double netPercue { get; set; }

        public int? perception_idPerception { get; set; }

        public virtual PerceptionModel perciption { get; set; }
    }
}