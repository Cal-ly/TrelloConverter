using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ducktape.Trelloconverter.Models
{
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

        [JsonPropertyName("closed")]
        public bool Closed { get; set; }

        [JsonPropertyName("dateLastActivity")]
        public DateTime DateLastActivity { get; set; }

        [JsonPropertyName("idMembers")]
        public List<string> IdMembers { get; set; }

        [JsonPropertyName("idLabels")]
        public List<string> IdLabels { get; set; }

        [JsonPropertyName("attachments")]
        public List<Attachment> Attachments { get; set; }

        [JsonPropertyName("checklists")]
        public List<Checklist> Checklists { get; set; }

        [JsonPropertyName("idShort")]
        public int IdShort { get; set; }

        [JsonPropertyName("shortLink")]
        public string ShortLink { get; set; }

        [JsonPropertyName("idList")]
        public string IdList { get; set; }

        [JsonPropertyName("idBoard")]
        public string IdBoard { get; set; }

        [JsonPropertyName("pos")]
        public double Pos { get; set; }

        [JsonPropertyName("due")]
        public DateTime? Due { get; set; }

        [JsonPropertyName("dueComplete")]
        public bool DueComplete { get; set; }
    }
}
