using System.Text.Json.Serialization;

namespace LogProxyWebAPI.Entities
{
    public class Fields
    {
        public string Summary { get; set; }
        public string Message { get; set; }
        [JsonPropertyName("id")]
        public string ID { get; set; }
        [JsonPropertyName("receivedAt")]
        public DateTime? ReceivedAt { get; set; }
    }

    public class Record
    {
        [JsonPropertyName("id")]
        public string ID { get; set; }
        [JsonPropertyName("fields")]
        public Fields Fields { get; set; }
        [JsonPropertyName("createdTime")]
        public DateTime? CreatedTime { get; set; }
    }

    public class Root
    {
        [JsonPropertyName("records")]
        public List<Record> Records { get; set; }
    }
}