using System.Text.Json.Serialization;

namespace LogProxyWebAPI.Entities.Response
{
    public class Record
    {
        [JsonPropertyName("id")]
        public string ID { get; set; }
        [JsonPropertyName("createdTime")]
        public DateTime CreatedTime { get; set; }
        [JsonPropertyName("fields")]
        public Fields Fields { get; set; }
    }
}