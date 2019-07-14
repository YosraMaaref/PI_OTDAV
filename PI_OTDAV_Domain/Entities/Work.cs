using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI_OTDAV_Domain
{
   public class Work
    {
       
        private int idWork { get; set; }

        private String titre { get; set; }
        private String compositeur { get; set; }
        private String ville { get; set; }
        private DateTime date { get; set; }
        private String genre { get; set; }
        private int duree { get; set; }
        private int etat { get; set; }
        private float pourcentAdaptateur { get; set; }
        private float pourcentArrangeur { get; set; }
        private float pourcentAuteur { get; set; }
        private float pourcentCompositeur { get; set; }
        private float pourcentEditeur { get; set; }
        private String bulletinOfdeclaration { get; set; }
        private String copyOfWork { get; set; }
        private String tradRegisterExcept { get; set; }
        private String statutOfCompany { get; set; }
        private String copyOfThePublicationOfCaompnyJORT { get; set; }
        private String copyTaxIdentificationNumber { get; set; }
        private String copydeclarationOfExistance { get; set; }
        private String copymanagerIdentityCard { get; set; }
    }
}
