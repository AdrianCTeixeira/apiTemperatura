using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Http;
using WebApplication1.Core;

namespace WebApplication1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "",
                defaults: new { id = RouteParameter.Optional }
            );

            //Thread thread = new Thread(ServerSide.AtualizarDados);
            //thread.Start();

        }
    }
}
