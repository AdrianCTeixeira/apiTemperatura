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

            //verificar Integridade db e apis..
            //script de criacao de db e tabelas(usando webconfig.cnnstring)
            //caso uma api nao esteja online(health checker), invalidar todos os metodos usados por ela
            

            //melhorar atualizacao de temperatura(substituir threads)
            //descartar usos de entity framework(usar dapper)
            //*documentar api

            //remover o maximo de classes de modelo
            //*usar o maximo de async/await
            //*separar responsabilidades
            
            //*recriar plano de testes(apenas de integracao)            

            // Serviços e configuração da API da Web

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "",
                defaults: new { id = RouteParameter.Optional }
            );

            //Thread thread = new Thread(() => ServerSide.AtualizarDados(30)); //3600(1 hora)
            //thread.Start();

        }
    }
}
