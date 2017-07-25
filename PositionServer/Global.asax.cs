using System.Web.Http;
using StructureMap;
using System.Web.Http.Dispatcher;

namespace PositionServer
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var config = GlobalConfiguration.Configuration;

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
