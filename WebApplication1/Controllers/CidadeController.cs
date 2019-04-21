using Newtonsoft.Json;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApplication1.Core;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CidadeController : ApiController
    {
        readonly temperaturaDBEntities temperaturaDBEntities = temperaturaDBEntities.GetInstance();

        #region Post cities/{city_name}

        /// <summary>
        /// Cadastra uma nova cidade para ter a temperatura monitorada.
        /// </summary>
        /// <param name="city_name"></param>
        /// <returns></returns>
        [Route("cities/{city_name}")]
        [HttpPost]
        public async Task<IHttpActionResult> RegisterCityAsync(string city_name)
        {
            Response account = await ConsultaAPI.ConsultarApiTempAsync(city_name);
            try
            {
                temperaturaDBEntities.Temperatura.Add(new Temperatura
                {
                    date = DateTime.Parse(account.Results.Date),
                    temperature = int.Parse(account.Results.Temp),
                    Cidade = temperaturaDBEntities.Cidade.Add(new Cidade
                    {
                        city = account.Results.City_name
                    })

                });
                temperaturaDBEntities.SaveChanges();
                return Ok(city_name + " registrada com sucesso!");

            }
            catch (DbUpdateException)
            {
                return BadRequest(account.Results.City_name + " já cadastrada");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }
        #endregion

        #region Delete cities/{city_name}
        /// <summary>
        /// Remove uma cidade do monitoramento.
        /// </summary>
        /// <param name="city_name"></param>
        /// <returns></returns>
        [Route("cities/{city_name}")]
        [HttpDelete]
        public IHttpActionResult DeleteCity(string city_name)
        {
            try
            {
                temperaturaDBEntities.Cidade.Remove(temperaturaDBEntities.Cidade.First(c => c.city == city_name));
                temperaturaDBEntities.SaveChanges();
                return Ok(city_name + " removida com sucesso");

            }
            catch (DbUpdateException)
            {
                return BadRequest("Erro#%!#$%R");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Cidade nao existente");
            }

            catch (Exception)
            {
                return BadRequest("Erro generico");
            }

        }
        #endregion

        #region Post cities/by_cep/{cep}
        /// <summary>
        /// Cadastra a cidade que corresponde a este CEP
        /// </summary>
        /// <param name="cep"></param>
        /// <returns></returns>
        [Route("cities/by_cep/{cep}")]
        [HttpPost]
        public async Task<IHttpActionResult> CadastrarCidadePorCEPAsync(string cep)
        {
            var a = ConsultaAPI.ConsultarApiCep(cep);

            if (a == null) { return BadRequest("Cep Invalido"); }

            return await RegisterCityAsync(a);
        }
        #endregion
    }
}
