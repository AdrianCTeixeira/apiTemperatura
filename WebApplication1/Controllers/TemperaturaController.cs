using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Core;
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
        public List<CityTemp> GetAllTemperatures()
        {
            var extemp = new temperaturaDBEntities().Cidade.ToList();

            var listTemperatures = new List<CityTemp>();

            foreach (Cidade tmp in extemp)
            {
                CityTemp cityTemp = new CityTemp();
                if (tmp.Temperatura.Count > 0)
                {
                    cityTemp.city = tmp.city;

                    var lastTemperature = tmp.Temperatura.Last();
                    cityTemp.temperatures = new TemperaturaResponse(lastTemperature.date, lastTemperature.temperature);
                    listTemperatures.Add(cityTemp);
                }
            }

            return listTemperatures;

        }

        [Route("cities/by_cep/{cep}")]
        [HttpPost]
        public IHttpActionResult CadastrarCidadePorCEP(string cep)
        {
            if (cep == null)
            {
                return BadRequest("Not valid cep");
            }

            RegisterCity(ConsultaAPI.ConsultarApiCep(cep).Localidade);

            return Ok();

        }

    }

}


