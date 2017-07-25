using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Reflection;
using Autofac;
using Autofac.Configuration;
using Autofac.Integration.WebApi;
using Microsoft.Extensions.Configuration;
using System.IO;
using StructureMap;
using System.Web.Http.Dispatcher;

namespace PositionServer
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var config = GlobalConfiguration.Configuration;

            #region # Autofac
            //var builder = new ContainerBuilder();
            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //var jsonBuilder = new ConfigurationBuilder();
            //jsonBuilder.AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "autofac.json"));
            //var module = new ConfigurationModule(jsonBuilder.Build());
            //builder.RegisterModule(module);
            //var container = builder.Build();
            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            #endregion

            #region StructureMap

            var container = new Container(_ =>
            {
                _.Scan(x =>
                {
                    x.Assembly("PositionServer.Repository");
                    x.TheCallingAssembly();
                    x.WithDefaultConventions();
                });
            });
            config.Services.Replace(
                typeof(IHttpControllerActivator),
                new StructureMapActivator(container));

            #endregion

            //AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All;


            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}
