using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IPositionRepository
    {
        Task<IEnumerable<Position>> GetPositionsAsync();
        Task<IEnumerable<Security>> GetSecuritiesAsync();
        Task<IEnumerable<Trade>> GetTradesAsync();
    }
}

