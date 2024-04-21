using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ducktape.Trelloconverter.Models
{
    public class Limits
    {
        [JsonPropertyName("attachments")]
        public Attachment Attachments { get; set; }
        [JsonPropertyName("boards")]
        public Board Boards { get; set; }
        [JsonPropertyName("cards")]
        public Card Cards { get; set; }
        // Includes LimitDetails sub-classes for various properties like Boards, Cards, etc.
    }
}
