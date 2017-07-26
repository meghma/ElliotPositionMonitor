using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using System;
using PositionServer.Interfaces;
using PositionServer.Entities;

namespace PositionServer.Controllers
{
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

        [HttpPost]
        [Route("trades")]
        [ResponseType(typeof(IEnumerable<Trade>))]
        public async Task<IHttpActionResult> SubmitTrade(Trade req)
        {
            try
            {
                await _repository.SubmitTradeAsync(req);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
