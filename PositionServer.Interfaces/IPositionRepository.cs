using System.Collections.Generic;
using System.Threading.Tasks;
using PositionServer.Entities;

namespace PositionServer.Interfaces
{
    public interface IPositionRepository
    {
        Task<IEnumerable<Position>> GetPositionsAsync();
        Task<IEnumerable<Security>> GetSecuritiesAsync();
        Task<IEnumerable<Trade>> GetTradesAsync();
        Task SubmitTradeAsync(Trade req);
    }
}
