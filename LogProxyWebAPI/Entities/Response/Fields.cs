using System.Text.Json.Serialization;

namespace LogProxyWebAPI.Entities.Response
{
    public class Fields
    {
        [JsonPropertyName("id")]
        public string ID { get; set; }
        public string Summary { get; set; }
        public string Message { get; set; }
        [JsonPropertyName("receivedAt")]
        public DateTime ReceivedAt { get; set; }
    }
}