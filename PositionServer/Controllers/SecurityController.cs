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
    public class SecurityController : ApiController
    {
        private IPositionRepository _repository;

        public SecurityController()
        {
            _repository = new PositionRepository();
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
