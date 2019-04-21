using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Core
{
    public class ServerSide
    {
        static readonly temperaturaDBEntities temperaturaDBEntities = temperaturaDBEntities.GetInstance();

        /// <summary>
        /// Lopping que irá rodar automaticamente acessando a cada hora a API de dados de temperatura.
        /// </summary>
        /// <param name="segundos"></param>
        public static void AtualizarDados(int segundos)
        {
            for ( ; ; )
            {
                CarregarInformacoes();
                Thread.Sleep(segundos*1000);
                Debug.WriteLine("Send to debug output.");
            }
        }
        protected static void CarregarInformacoes()
        {
            foreach (var cidade in temperaturaDBEntities.Cidade.ToList())
            {
                var extemp = temperaturaDBEntities.Cidade.First(t => t.id == cidade.id);
                List<Temperatura> gettemp = extemp.Temperatura.ToList(); ;
                for (int i = 0; cidade.Temperatura.Count >= 30; i++)
                {
                    temperaturaDBEntities.Temperatura.Remove(gettemp[i]);
                    temperaturaDBEntities.SaveChanges();
                }
                Response retorno = ConsultaAPI.ConsultarApiTempAsync(cidade.city).Result;
                string date = DateTime.ParseExact(retorno.Results.Date +
                    " " + retorno.Results.Time + ":00", "dd/MM/yyyy HH:mm:ss",
                    CultureInfo.InvariantCulture).ToString("dd/MM/yyyy HH");
                temperaturaDBEntities.Temperatura.Add(new Temperatura
                {
                    date = DateTime.Parse(date + ":00" + ":00"),
                    temperature = int.Parse(retorno.Results.Temp),
                    cidade_id = cidade.id
                });
                temperaturaDBEntities.SaveChanges();
            }
        }
    }
}