using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LogProxyWebAPI.Entities.Response
{
    public class Root
    {
        [JsonPropertyName("records")]    
        public List<Record> Records { get; set; }
    }
}