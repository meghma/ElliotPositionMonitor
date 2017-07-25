using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using System;
using PositionServer.Interfaces;
using PositionServer.Entities;
using Autofac.Integration.WebApi;

namespace PositionServer.Controllers
{
    [EnableCors(origins: "http://positionview.azurewebsites.net", headers: "*", methods: "*")]
    [RoutePrefix("api/positionmonitor")]
    //[AutofacControllerConfiguration]
    public class SecurityController : ApiController
    {
        private IPositionRepository _repository;

        public SecurityController(IPositionRepository repo)
        {
            _repository = repo;
        }

        [HttpGet]
        [Route("securities")]
        [ResponseType(typeof(IEnumerable<Security>))]
        public async Task<IHttpActionResult> GetSecurities()
        {
            try
            {
                var securities = await _repository.GetSecuritiesAsync();
                var json = JsonConvert.SerializeObject(securities);
                return Ok(json);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
