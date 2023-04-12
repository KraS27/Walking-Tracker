using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WT_Models.Entities
{
    public class WalksResponse : BaseResponse<List<Walk>>
    {
        public double TotalDistanceTraveled { get; set; }

        public TimeSpan TotalTimeTraveled { get; set; }

        public int WalksCount { get; set; }
    }
}
