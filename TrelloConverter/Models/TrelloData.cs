namespace TrelloConverter.Models;

public class TrelloData
{
    public List<Card>? Cards { get; set; }
    public List<Models.List>? Lists { get; set; }
    public List<Label>? Labels { get; set; }
    public List<Checklist>? Checklists { get; set; }
}