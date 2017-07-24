using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Trade
    {
        [JsonProperty(PropertyName = "tradeDate")]
        public DateTime Date { get; set; }
        [JsonProperty(PropertyName = "securityId")]
        public int SecurityID { get; set; }
        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }
        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }
    }

    public class TradeWrapper : Trade
    {
        [JsonProperty(PropertyName = "securityName")]
        public string SecurityName { get; set; }
    }
}
