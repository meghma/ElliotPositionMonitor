using System.Collections.Generic;
using Entities;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Interfaces;
using Repository;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using System;

namespace PositionServer.Controllers
{
    [EnableCors(origins: "http://positionview.azurewebsites.net", headers: "*", methods: "*")]
    [RoutePrefix("api/positionmonitor")]
    public class PositionController : ApiController
    {
        private IPositionRepository _repository;

        public PositionController()
        {
            _repository = new PositionRepository();
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
