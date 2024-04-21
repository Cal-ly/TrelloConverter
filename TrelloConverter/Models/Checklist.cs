using System.Text.Json.Serialization;

namespace TrelloConverter.Models;

public class Checklist
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("checkItems")]
    public List<CheckItem> CheckItems { get; set; }
}
