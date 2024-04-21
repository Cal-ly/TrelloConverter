using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ducktape.Trelloconverter.Models
{
    public class Checklist
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("idCard")]
        public string IdCard { get; set; }

        [JsonPropertyName("pos")]
        public double Pos { get; set; }

        [JsonPropertyName("checkItems")]
        public List<CheckItem> CheckItems { get; set; }
    }
}
