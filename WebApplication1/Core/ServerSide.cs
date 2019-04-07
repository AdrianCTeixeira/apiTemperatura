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
        public static void AtualizarDados()
        {
            for (; ; )
            {
                CarregarInformacoes();
                Thread.Sleep(10000);
                Debug.WriteLine("Send to debug output.");
            }
        }
        public static void CarregarInformacoes()
        {

            temperaturaDBEntities temperaturaDBEntities = new temperaturaDBEntities();
            foreach (var cidade in temperaturaDBEntities.Cidade.ToList())
            {
                var extemp = temperaturaDBEntities.Cidade.First(t => t.id == cidade.id);
                List<Temperatura> gettemp = extemp.Temperatura.ToList(); ;
                for (int i = 0; cidade.Temperatura.Count >= 30; i++)
                {
                    temperaturaDBEntities.Temperatura.Remove(gettemp[i]);
                    temperaturaDBEntities.SaveChanges();
                }
                Response retorno = ConsultaAPI.ConsultarApi(cidade.city);
                string date = DateTime.ParseExact(retorno.results.date +
                    " " + retorno.results.time + ":00", "dd/MM/yyyy HH:mm:ss",
                    CultureInfo.InvariantCulture).ToString("dd/MM/yyyy HH");
                temperaturaDBEntities.Temperatura.Add(new Temperatura
                {
                    date = DateTime.Parse(date + ":00" + ":00"),
                    temperature = int.Parse(retorno.results.temp),
                    cidade_id = cidade.id
                });
                temperaturaDBEntities.SaveChanges();
            }
        }

    }
}