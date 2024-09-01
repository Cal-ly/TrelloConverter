namespace TrelloConverter.Models;

public class CheckItem
{
    //[JsonPropertyName("id")]
    public string? Id { get; set; }

    //[JsonPropertyName("name")]
    public string? Name { get; set; }

    //[JsonPropertyName("idChecklist")]
    public string? IdChecklist { get; set; }
}
