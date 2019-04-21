using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TemperaturaController : ApiController
    {
        readonly temperaturaDBEntities temperaturaDBEntities = temperaturaDBEntities.GetInstance();

        #region Get cities/{city_name}/temperatures

        /// <summary>
        /// Retorna as temperaturas das últimas 30 horas da cidade informada
        /// </summary>
        /// <param name="city_name"></param>
        /// <returns></returns>
        [Route("cities/{city_name}/temperatures")]
        [HttpGet]
        public IHttpActionResult GetCityTemperatures(string city_name)
        {
            try
            {
                var paramDecode = Encoding.UTF8.GetString(Encoding.Default.GetBytes(city_name));
                var extemp = temperaturaDBEntities.GetInstance().Cidade.First(t => t.city == paramDecode);
                if (extemp == null)
                {
                    return Content(HttpStatusCode.NotFound, "Nao achado");
                }

                var listTemperatures = new List<TemperaturaResponse>();
                foreach (Temperatura tmp in extemp.Temperatura)
                {
                    //TemperaturaResponse tempResponse = new TemperaturaResponse(tmp.date, tmp.temperature);
                    listTemperatures.Add(new TemperaturaResponse(tmp.date, tmp.temperature));
                }
                return Ok(new LogTemperature() { City = extemp.city, Temperatures = listTemperatures });

            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        #endregion

        #region Delete cities/{city_name}/temperatures

        /// <summary>
        /// Apaga o histórico de temperaturas da cidade.
        /// </summary>
        /// <param name="city_name"></param>
        /// <returns></returns>
        [Route("cities/{city_name}/temperatures")]
        [HttpDelete]
        public IHttpActionResult DeleteCityTemps(string city_name)
        {
            try
            {
                if (city_name == null)
                {
                    return BadRequest("Not valid cityName");
                }
                temperaturaDBEntities.Temperatura.RemoveRange(temperaturaDBEntities.Cidade.First(c => c.city == city_name).Temperatura);
                temperaturaDBEntities.SaveChanges();
                return Ok("Todas temperaturas relacionada a " + city_name + " foram removidas com sucesso");

            }
            catch (InvalidOperationException)
            {
                return BadRequest("Cidade não registrada");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        #endregion

        #region Get /temperatures

        /// <summary>
        /// Retorna a lista paginada das cidades em ordem decrescente da última temperatura registrada.
        /// </summary>
        /// <returns></returns>
        [Route("temperatures")]
        [HttpGet]
        public List<CityTemp> GetAllTemperatures()
        {
            var extemp = temperaturaDBEntities.GetInstance().Cidade.ToList();

            var listTemperatures = new List<CityTemp>();

            foreach (Cidade tmp in extemp)
            {
                CityTemp cityTemp = new CityTemp();
                if (tmp.Temperatura.Count > 0)
                {
                    cityTemp.City = tmp.city;

                    var lastTemperature = tmp.Temperatura.Last();
                    cityTemp.Temperatures = new TemperaturaResponse(lastTemperature.date, lastTemperature.temperature);
                    listTemperatures.Add(cityTemp);
                }
            }

            return listTemperatures;

        }
        #endregion
    }
}


