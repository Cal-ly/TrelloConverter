using System.Reflection.Emit;
using System.Text.Json.Serialization;

namespace Ducktape.Trelloconverter.Models;

public class Board
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("nodeId")]
    public string NodeId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("desc")]
    public string Desc { get; set; }

    [JsonPropertyName("closed")]
    public bool Closed { get; set; }

    [JsonPropertyName("dateClosed")]
    public DateTime? DateClosed { get; set; }

    [JsonPropertyName("idOrganization")]
    public string IdOrganization { get; set; }

    [JsonPropertyName("limits")]
    public Limits Limits { get; set; }

    [JsonPropertyName("pinned")]
    public bool Pinned { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("shortLink")]
    public string ShortLink { get; set; }

    [JsonPropertyName("subscribed")]
    public bool Subscribed { get; set; }

    [JsonPropertyName("labelNames")]
    public List<Label> LabelNames { get; set; }

    [JsonPropertyName("powerUps")]
    public List<string> PowerUps { get; set; }

    [JsonPropertyName("dateLastActivity")]
    public DateTime DateLastActivity { get; set; }

    [JsonPropertyName("dateLastView")]
    public DateTime DateLastView { get; set; }

    [JsonPropertyName("idTags")]
    public List<string> IdTags { get; set; }

    [JsonPropertyName("actions")]
    public List<Action> Actions { get; set; }

    [JsonPropertyName("cards")]
    public List<Card> Cards { get; set; }

    [JsonPropertyName("lists")]
    public List<BoardList> BoardLists { get; set; }

    [JsonPropertyName("idEnterprise")]
    public string IdEnterprise { get; set; }

    [JsonPropertyName("starred")]
    public bool Starred { get; set; }

    [JsonPropertyName("shortUrl")]
    public string ShortUrl { get; set; }

    [JsonPropertyName("datePluginDisable")]
    public DateTime? DatePluginDisable { get; set; }

    [JsonPropertyName("creationMethod")]
    public string CreationMethod { get; set; }

    [JsonPropertyName("ixUpdate")]
    public string IxUpdate { get; set; }

    [JsonPropertyName("templateGallery")]
    public object TemplateGallery { get; set; }

    [JsonPropertyName("enterpriseOwned")]
    public bool EnterpriseOwned { get; set; }

    [JsonPropertyName("idBoardSource")]
    public string IdBoardSource { get; set; }

    [JsonPropertyName("premiumFeatures")]
    public List<string> PremiumFeatures { get; set; }

    [JsonPropertyName("idMemberCreator")]
    public string IdMemberCreator { get; set; }
    [JsonPropertyName("labels")]
    public List<Label> Labels { get; set; }
}
