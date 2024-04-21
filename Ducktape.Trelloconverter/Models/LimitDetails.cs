using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ducktape.Trelloconverter.Models
{
    public class LimitDetails
    {
        public int PerBoard { get; set; }
        public string Status { get; set; }
        public int DisableAt { get; set; }
        public int WarnAt { get; set; }
    }

}
