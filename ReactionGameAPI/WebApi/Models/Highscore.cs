using System.Text.Json.Serialization;

namespace WebApi.Models
{
    public class Highscore
    {
        [JsonPropertyName("id")] public Guid Id { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("time")] public long Time { get; set; }
    }
}
