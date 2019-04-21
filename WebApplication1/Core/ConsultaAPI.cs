using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WebApplication1.Models;
using static System.Net.WebRequestMethods;

namespace WebApplication1.Core
{
    public class ConsultaAPI
    {

        public static async Task<Response> ConsultarApiTempAsync(string cityName)
        {
            HttpResponseMessage response = await new HttpClient()
                .GetAsync("https://api.hgbrasil.com/weather/?format=json&city_name=" + cityName + "&key=bf8b76a2");

            Response account = JsonConvert.DeserializeObject<Response>(response.Content.ReadAsStringAsync().Result);
            return account;
        }
        public static string ConsultarApiCep(string cep)
        {
            
            string url = "https://viacep.com.br/ws/" + cep + "/json/";
            HttpResponseMessage response = new HttpClient()
                .GetAsync(url).Result;
            if(response.StatusCode == HttpStatusCode.BadRequest){ return null;}

            return ((dynamic)JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result))["localidade"];
        }

        

    }
}