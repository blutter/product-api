using RestExampleApi.Auth;
using System.Web.Http;

namespace RestExampleApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Message handlers
            config.MessageHandlers.Add(new CustomAuthenticationHandler());
        }
    }
}
