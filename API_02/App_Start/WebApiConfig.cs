using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API_02
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //enable cors without restrictions
            config.EnableCors();
            //var cors = new EnableCorsAttribute("https://localhost:3030", "*", "*");
            //config.EnableCors(cors);

            // Web API configuration and services
            //config.EnableCors();
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
        }
    }
}
