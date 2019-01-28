using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestService1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace RestService1.Controllers
{
    public class ExchangerateController : Controller
    {
        // GET: Exchangerate
        public class ExchangerateController : ApiController
        {


           //GET: Exchangerate

            //public Exchangerate[] Get()
            //{

            //    return new Exchangerate[]
            //     {
            //           new Exchangerate
            //           {
            //               Start_at="2018-02-02"

            //           }


            //};



            //}


            public string MyResult(string target, string base1, string start_at, string end_at)
            {
                var exchangerate = new Exchangerate() // THIS MY ENDPOINT
                {
                    Target = target,
                    Start_at = start_at,
                    End_at = end_at,
                    Base1 = base1,

                };

                var result = GetExchangerate(exchangerate).Result;

                var json = JsonConvert.SerializeObject(result);

                return json;
            }

            private async Task<Rate> GetExchangerate(Exchangerate exchangerate)
            {


                var client = new HttpClient();
                var response = await client.GetAsync("https://api.exchangeratesapi0+.io/history?start_at=2018-01-01&end_at=2018-03-01&base=USD&symbols=SEK");

                var json = await response.Content.ReadAsStringAsync();//
                var content = response.Content.ReadAsAsync<Rate>().Result;// , create a jobject ,change my json in type Exchangerate


                var rt = new Rate()
                {
                    Max = double.MinValue,
                    Min = double.MaxValue


                };

                foreach (var rate in content.Rates)
                {

                    var maxDate = DateTime.MaxValue;
                    var minDate = DateTime.MinValue;


                    if (rate.Value.Value<JObject>().Value<double>() > rt.Max) //Dynamic type, Jobject : is dictionnary that contains any value not in the begining

                    {
                        rt.Max = rate.Value.Value<double>();
                        maxDate = DateTime.Parse(rate.Key);

                    }

                    if (rate.Value.Value<JObject>().Value<double>() < rt.Min)
                    {
                        rt.Min = rate.Value.Value<double>();
                        minDate = DateTime.Parse(rate.Key);

                    }

                    rt.AllRates += rate.Value.Value<double>();
                    {
                        var i = rt.AllRates / content.Rates.Count;
                    }
                }

                return rt;

            }


        }

    }
}