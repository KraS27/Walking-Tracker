using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WT_Models.Entities
{
    public class WalksByDayResponse : BaseResponse<List<Walk>>
    {
        public double TotalDayDistanceTraveled { get; set; }

        public TimeSpan TotalDayTimeTraveled { get; set; }
    }
}
