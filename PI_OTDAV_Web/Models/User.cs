using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PI_OTDAV_Web.Models
{
    public class User
    {
        public User()
        {

        }

        public int id { get; set; }

        public DateTime? acceptationDate { get; set; }

        public string accountStatuts { get; set; }

        public string accountType { get; set; }

        public string address { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? birthday { get; set; }

        [Required(ErrorMessage ="N° CIN est obligatoire")]
        public string cin { get; set; }


        public string commercialRegisterNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime? dateCin { get; set; }

        [Required]
        public string firstName { get; set; }

        public int? fonction { get; set; }


        public string gouverment { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public int phone { get; set; }

        public int points { get; set; }

        public int postelCode { get; set; }

        public string raisonSocial { get; set; }

        public DateTime? registrationDate { get; set; }

        [Required]
        public string userName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string mail { get; set; }


    }
}