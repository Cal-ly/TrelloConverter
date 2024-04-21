using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ducktape.Trelloconverter.Models
{
    public class ActionData
    {
        public Card Card { get; set; }
        public BoardList List { get; set; }
        public Board Board { get; set; }
        public Member Member { get; set; }
    }

}
