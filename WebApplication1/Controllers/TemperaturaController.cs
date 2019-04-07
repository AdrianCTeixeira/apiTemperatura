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
        [Route("cities/{city_name}/temperatures")]
        [HttpGet]
        public LogTemperature GetCityTemperatures(string city_name)
        {
            var extemp = new temperaturaDBEntities().Cidade.First(t => t.city == city_name);

            var listTemperatures = new List<TemperaturaResponse>();
            foreach (Temperatura tmp in extemp.Temperatura)
            {
                TemperaturaResponse tempResponse = new TemperaturaResponse(tmp.date, tmp.temperature);
                listTemperatures.Add(tempResponse);
            }
            return new LogTemperature() { city = extemp.city, temperatures = listTemperatures };

        }

        [Route("cities/{city_name}")]
        [HttpPost]
        public void RegisterCity(string city_name)
        {
            string url = "https://api.hgbrasil.com/weather/?format=json&city_name=" + city_name + "&key=bf8b76a2";
            HttpResponseMessage response = new HttpClient().GetAsync(url).Result;
            Response account = JsonConvert.DeserializeObject<Response>(response.Content.ReadAsStringAsync().Result);

            temperaturaDBEntities temperaturaDBEntities = new temperaturaDBEntities();
            temperaturaDBEntities.Temperatura.Add(new Temperatura
            {
                date = DateTime.Parse(account.results.date),
                temperature = int.Parse(account.results.temp),
                Cidade = temperaturaDBEntities.Cidade.Add(new Cidade
                {
                    city = account.results.city_name
                })

            });
            temperaturaDBEntities.SaveChanges();
        }

        [Route("cities/{city_name}")]
        [HttpDelete]
        public IHttpActionResult DeleteCity(string city_name)
        {
            if (city_name == null)
            {
                return BadRequest("Not valid cityName");
            }
            temperaturaDBEntities temperaturaDBEntities = new temperaturaDBEntities();
            temperaturaDBEntities.Cidade.Remove(temperaturaDBEntities.Cidade.First(c => c.city == city_name));
            temperaturaDBEntities.SaveChanges();
            return Ok();
        }

        [Route("cities/{city_name}/temperatures")]
        [HttpDelete]
        public IHttpActionResult DeleteCityTemps(string city_name)
        {
            if (city_name == null)
            {
                return BadRequest("Not valid cityName");
            }
            temperaturaDBEntities temperaturaDBEntities = new temperaturaDBEntities();
            var extemp = temperaturaDBEntities.Cidade.First(c => c.city == city_name);

            temperaturaDBEntities.Temperatura.RemoveRange(extemp.Temperatura);
            temperaturaDBEntities.SaveChanges();
            //temperaturaDBEntities.Cidade.Remove(temperaturaDBEntities.Cidade.First(c => c.city == city_name));

            return Ok();
        }

        [Route("temperatures")]
        [HttpGet]
        public void GetAllTemperatures()
        {

            //temperaturaDBEntities temperaturaDBEntities = new temperaturaDBEntities();

            //var extemp = temperaturaDBEntities.Cidade.ToList();

            //var listTemperatures = new List<LogTemperature>();
            //LogTemperature logTemperature = new LogTemperature();
            //TemperaturaResponse temperaturaResponse = null;
            //foreach (Cidade tmp in extemp)
            //{
                
            //    var a = tmp.Temperatura.Last();
            //    temperaturaResponse = new TemperaturaResponse(a.date, a.temperature);
            //    listTemperatures.Add(logTemperature);
            //}
            //logTemperature.temperatures.Add(temperaturaResponse);

            //logTemperature.city = tmp.city;
            ////return new LogTemperature() { city = extemp., temperatures = listTemperatures };

        }
    }
}

