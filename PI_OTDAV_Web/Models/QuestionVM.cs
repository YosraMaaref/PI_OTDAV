using PI_OTDAV_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace PI_OTDAV_Web.Models
{
    public class QuestionVM : IEnumerable<QuestionVM>
    {

        public int questionId { get; set; }
        public String question { get; set; }
        public String prop1 { get; set; }
        public String prop2 { get; set; }
        public String prop3 { get; set; }
        public String prop4 { get; set; }
        public int response { get; set; }
        public String description { get; set; }

        public IEnumerator<QuestionVM> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}