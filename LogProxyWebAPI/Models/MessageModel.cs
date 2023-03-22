using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LogProxyWebAPI.Models
{
    public class MessageModel
    {
        [Required]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [Required]
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}