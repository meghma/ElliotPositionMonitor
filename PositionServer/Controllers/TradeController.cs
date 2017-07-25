using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using System;
using PositionServer.Interfaces;
using PositionServer.Entities;

namespace PositionServer.Controllers
{
    [EnableCors(origins: "http://positionview.azurewebsites.net", headers: "*", methods: "*")]
    [RoutePrefix("api/positionmonitor")]
    public class TradeController : ApiController
    {
        private IPositionRepository _repository;

        public TradeController(IPositionRepository repo)
        {
            _repository = repo;
        }

        [HttpGet]
        [Route("trades")]
        [ResponseType(typeof(IEnumerable<Trade>))]
        public async Task<IHttpActionResult> GetTrades()
        {
            try
            {
                var trades = await _repository.GetTradesAsync();
                var json = JsonConvert.SerializeObject(trades);
                return Ok(json);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
