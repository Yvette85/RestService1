using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestService1.Models
{
    public class Exchangerate
    {

            [JsonProperty("base")]
            public string Base1 { get; set; }


            [JsonProperty("start_at")]
            public string Start_at { get; set; }

            [JsonProperty("end_at")]
            public string End_at { get; set; }


            [JsonProperty("symbols")]
            public string Target { get; set; }

            public JObject Rates { get; set; }

        }
    }
