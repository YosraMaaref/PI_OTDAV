using System;
using System.Collections.Generic;

namespace PI_OTDAV_Domain
{
    public class Question
    {
        
        public int questionId { get; set; }
        public String question { get; set; }
        public String prop1 { get; set; }
        public String prop2 { get; set; }
        public String prop3 { get; set; }
        public String prop4 { get; set; }
        public int response { get; set; }
        public String description { get; set; }
        public String warningCorrectResponse { get; set; }

        public static implicit operator List<object>(Question v)
        {
            throw new NotImplementedException();
        }
    }
}
