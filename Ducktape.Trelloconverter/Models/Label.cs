using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ducktape.Trelloconverter.Models
{
    public class Label
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
