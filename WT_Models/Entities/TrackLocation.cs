using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WT_Models.Entities
{
    public class TrackLocation
    {
        public int Id { get; set; }

        public string IMEI { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public DateTime DateTrack { get; set; }
    }
}
