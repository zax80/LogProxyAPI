using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LogProxyWebAPI.Entities.Request
{
    public class Record
    {
        [JsonProperty(PropertyName = "fields")]
        public Fields Fields { get; set; }
    }
}