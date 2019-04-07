using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Core
{
    public class ConsultaAPI
    {
        
        public static Response ConsultarApi(string cityName)
        {
            HttpResponseMessage response = new HttpClient()
                .GetAsync("https://api.hgbrasil.com/weather/?format=json&city_name=" + cityName + "&key=bf8b76a2").Result;

            Response account = JsonConvert.DeserializeObject<Response>(response.Content.ReadAsStringAsync().Result);
            return account;
        }
        private static void SalvarCidade()
        {
            string cidade = "Petrópolis";
            Response account = ConsultarApi(cidade);

            temperaturaDBEntities temperaturaDBEntities = new temperaturaDBEntities();
            temperaturaDBEntities.Temperatura.Add(new Temperatura
            {
                data = DateTime.Parse(account.results.date),
                temperatura1 = int.Parse(account.results.temp),
                Cidade = temperaturaDBEntities.Cidade.Add(new Cidade { nome = account.results.city_name })

            });
            temperaturaDBEntities.SaveChanges();
        }
    }
}