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
        
        public static Response ConsultarApiTemp(string cityName)
        {
            HttpResponseMessage response = new HttpClient()
                .GetAsync("https://api.hgbrasil.com/weather/?format=json&city_name=" + cityName + "&key=bf8b76a2").Result;

            Response account = JsonConvert.DeserializeObject<Response>(response.Content.ReadAsStringAsync().Result);
            return account;
        }
        public static CepJSON ConsultarApiCep(string cep)
        {
            HttpResponseMessage response = new HttpClient()
                .GetAsync("https://viacep.com.br/ws/" + cep + "/json/").Result;

            CepJSON account = JsonConvert.DeserializeObject<CepJSON>(response.Content.ReadAsStringAsync().Result);
            return account;
        }
        


        //private static void SalvarCidade()
        //{
        //    string cidade = "Petrópolis";
        //    Response account = ConsultarApi(cidade);

        //    temperaturaDBEntities temperaturaDBEntities = new temperaturaDBEntities();
        //    temperaturaDBEntities.Temperatura.Add(new Temperatura
        //    {
        //        date = DateTime.Parse(account.results.date),
        //        temperature = int.Parse(account.results.temp),
        //        cidade_id = temperaturaDBEntities.Cidade.First(t => t.city == cidade)

        //    });
        //    temperaturaDBEntities.SaveChanges();
        //}
    }
}