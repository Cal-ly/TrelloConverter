using System.Text.Json.Serialization;

namespace TrelloConverter.Models;

public class Card
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("desc")]
    public string Description { get; set; }

    [JsonPropertyName("labels")]
    public List<Label> Labels { get; set; }

    [JsonPropertyName("idLabels")]
    public List<string> IdLabels { get; set; }

    [JsonPropertyName("checklists")]
    public List<Checklist> Checklists { get; set; }

    [JsonPropertyName("idList")]
    public string IdList { get; set; }

    [JsonPropertyName("idBoard")]
    public string IdBoard { get; set; }
}