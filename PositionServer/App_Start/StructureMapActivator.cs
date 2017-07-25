using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace PositionServer
{
    public class StructureMapActivator : IHttpControllerActivator
    {
        private readonly IContainer _container;

        public StructureMapActivator(IContainer container)
        {
            _container = container;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            return _container.GetInstance(controllerType) as IHttpController;
        }
    }
}