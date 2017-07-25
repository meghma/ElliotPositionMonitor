using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PositionServer.Entities
{
    public class Security
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "sector")]
        public string Sector { get; set; }
    }
}
