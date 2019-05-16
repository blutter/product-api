using RestExampleApi.App_Start;
using System.Web.Http;
using System.Web.Mvc;

namespace RestExampleApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            Startup.ConfigureContainer();
        }
    }
}
