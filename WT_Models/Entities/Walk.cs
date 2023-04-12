using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WT_Models.Entities
{
    public class Walk
    {        
        public TimeSpan Duration { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
