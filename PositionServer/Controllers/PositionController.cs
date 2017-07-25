using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using PositionServer.Interfaces;
using PositionServer.Entities;
using Autofac.Integration.WebApi;

namespace PositionServer.Controllers
{
    [EnableCors(origins: "http://positionview.azurewebsites.net", headers: "*", methods: "*")]
    [RoutePrefix("api/positionmonitor")]
    //[AutofacControllerConfiguration]
    public class PositionController : ApiController
    {
        private IPositionRepository _repository;

        public PositionController(IPositionRepository repo)
        {
            _repository = repo;
        }

        [HttpGet]
        [Route("positions")]
        [ResponseType(typeof(IEnumerable<Position>))]
        public async Task<IHttpActionResult> GetPositions()
        {
            try
            {
                var positions = await _repository.GetPositionsAsync();
                var json = JsonConvert.SerializeObject(positions);
                return Ok(json);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
