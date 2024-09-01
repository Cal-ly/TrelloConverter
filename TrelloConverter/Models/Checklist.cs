namespace TrelloConverter.Models;

public class Checklist
{
    //[JsonPropertyName("id")]
    public string? Id { get; set; }

    //[JsonPropertyName("name")]
    public string? Name { get; set; }
    //[JsonPropertyName("idCard")]
    public string? IdCard { get; set; }

    //[JsonPropertyName("checkItems")]
    public List<CheckItem>? CheckItems { get; set; }
}
