using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestService1.Models
{
    public class Rate
    {

       
            public JObject Rates { get; set; }
            public double Max { get; set; }
            public double Min { get; set; }
            public double Average { get; set; }
            public double AllRates { get; set; }
            public int Count { get; set; }
        
    }
}