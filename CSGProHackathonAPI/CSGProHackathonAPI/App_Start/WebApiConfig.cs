using CSGProHackathonAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CSGProHackathonAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
#if !DEBUG
            config.Filters.Add(new ForceHttpsAttribute());
#endif

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
