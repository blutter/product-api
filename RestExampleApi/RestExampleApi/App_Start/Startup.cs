using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using RestExample.Config;
using RestExample.Contracts;
using RestExample.Services;
using RestExampleApi.Controllers;
using System;
using System.Web.Http;

namespace RestExampleApi.App_Start
{
    public class Startup
    {
        internal static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ProductsController>();
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(RestExampleApiMapper));
                cfg.AddProfile(typeof(RestExampleMapper));
            }));
            builder.Register(context => context.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().InstancePerLifetimeScope();

            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}