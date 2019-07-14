using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI_OTDAV_Domain
{
    public class Category
    {

        public int idCategory { get; set; }
        public String libele { get; set; }
        public static implicit operator List<object>(Category v)
        {
            throw new NotImplementedException();
        }
    }
}
