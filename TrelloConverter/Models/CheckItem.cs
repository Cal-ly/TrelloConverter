using System.Text.Json.Serialization;

namespace TrelloConverter.Models;

public class CheckItem
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}
