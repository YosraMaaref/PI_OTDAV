using PI_OTDAV_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace PI_OTDAV_Web.Models
{
    public class CategoryVM : IEnumerable<CategoryVM>
    {
        public int idCategory { get; set; }
        public string libelle { get; set; }
        public IEnumerator<CategoryVM> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}