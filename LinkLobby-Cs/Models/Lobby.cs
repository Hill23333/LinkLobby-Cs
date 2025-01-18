using System.Text.Json.Serialization;

namespace LinkLobby.Models
{
    public class LobbyCreateRequest
    {
        public string? name { get; set; }
        [JsonPropertyName("public")]
        public bool publicProperty { get; set; }
        public string? username { get; set; }
        public string? address { get; set; }
    }
}
