using Owin;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Microsoft.Owin.Cors;
using StructureMap;

namespace PositionServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

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
            config.Services.Replace(typeof(IHttpControllerActivator),new StructureMapActivator(container));

            #endregion
            appBuilder.UseCors(CorsOptions.AllowAll);
            WebApiConfig.Register(config);
            appBuilder.UseWebApi(config);
        }
    }
}