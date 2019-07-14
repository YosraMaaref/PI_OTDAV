using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI_OTDAV_Domain
{
    public class Notification
    {


        private int idNotification { get; set; }
        private DateTime dateNotification { get; set; }
        private String etat { get; set; }
        private int typeNotification { get; set; }
        private String description { get; set; }
    }
}
