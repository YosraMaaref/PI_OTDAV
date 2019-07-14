using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PI_OTDAV_Web.Models
{
    public class ArtworkCategory
    {
        public int id { get; set; }

        public string details { get; set; }

        public string nom { get; set; }

        public bool status { get; set; }

        public string type { get; set; }
    }
}