using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TemperaturaController : ApiController
    {
        public string Get()
        {
            return "Hello World";
        }
        public async System.Threading.Tasks.Task<string> PostAsync([FromBody]string cityName)
        {
            string url = "https://api.hgbrasil.com/weather/?format=json&city_name="+cityName+"&key=bf8b76a2";
            HttpResponseMessage response = await new HttpClient().GetAsync(url);
            Response account = JsonConvert.DeserializeObject<Response>(await response.Content.ReadAsStringAsync());

            temperaturaDBEntities temperaturaDBEntities = new temperaturaDBEntities();
            temperaturaDBEntities.Temperatura.Add(new Temperatura
            {
                data = DateTime.Parse(account.results.date),
                temperatura1 = int.Parse(account.results.temp),
                Cidade = temperaturaDBEntities.Cidade.Add(new Cidade
                {
                    nome = account.results.city_name
                })

            });
            temperaturaDBEntities.SaveChanges();
            return account.results.temp;
        }
    }
}
