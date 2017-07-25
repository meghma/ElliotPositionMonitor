using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PositionServer.Entities
{
    public class Position
    {
        [JsonProperty(PropertyName = "securityId")]
        public int SecurityID { get; set; }
        [JsonProperty(PropertyName = "securityName")]
        public string SecurityName { get; set; }
        [JsonProperty(PropertyName = "avgPrice")]
        public double AveragePrice { get; set; }
        [JsonProperty(PropertyName = "tradeCount")]
        public int NumberOfTrades { get; set; }
        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }
    }
}
