using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LogProxyWebAPI.Entities.Request
{
    public class Fields
    {
        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }
        public string Summary { get; set; }
        public string Message { get; set; }
        [JsonProperty(PropertyName = "receivedAt")]
        public DateTime? ReceivedAt { get; set; }
    }
}