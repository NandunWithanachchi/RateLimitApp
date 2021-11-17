//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Text.Json.Serialization;

namespace RateLimitApp
{
    class Rate
    {
        public res resources  { get; set; }
        public class res
        {
            public cores core { get; set; }
            public class cores 
            {
                public double limit { get; set; }
                public double remaining { get; set; }
            }
        }
    }
}
