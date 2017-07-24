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
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [RoutePrefix("api/positionmonitor")]
    public class TradeController : ApiController
    {
        private IPositionRepository _repository;

        public TradeController()
        {
            _repository = new PositionRepository();
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
