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


namespace RestService1.Controllers
{
    // GET: Exchangerate
    public class ExchangerateController : ApiController
    {

        [AcceptVerbs("POST","GET")]

        [Route("api/exchangerate/myresult")]
        public string MyResult( string start_at, string end_at, string base1,string target )
        {
            var exchangerate = new Exchangerate() // THIS MY ENDPOINT
            {
                Start_at = start_at,
                End_at = end_at,
                Base1 = base1,
                Target = target,

            };

            var result = GetExchangerate(exchangerate).Result;

            var json = JsonConvert.SerializeObject(result);

            return json;
        }


       
        private async Task<Rate> GetExchangerate(Exchangerate exchangerate)
        {


            var client = new HttpClient();
            var response = await client.GetAsync(new Uri($"https://api.exchangeratesapi.io/history?start_at={exchangerate.Start_at}&end_at={exchangerate.End_at}&base={exchangerate.Base1}&symbols={exchangerate.Target}"));

            var json = response.Content.ReadAsStringAsync();//
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
