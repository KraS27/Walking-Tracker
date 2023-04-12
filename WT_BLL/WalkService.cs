using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WT_DAL;
using WT_Models.Entities;

namespace WT_BLL
{
    public class WalkService : IWalkService
    {
        const int RADIUS_OF_EARTH = 6371;

        private readonly IBaseRepository<TrackLocation> _trackRepository;

        public WalkService(IBaseRepository<TrackLocation> trackRepository)
        {
            _trackRepository = trackRepository;
        }

        public async Task<BaseResponse<List<Walk>>> GetAllWalksAsync()
        {
            try
            {
                var tracks = await _trackRepository.GetAll().OrderBy(t => t.DateTrack).ToArrayAsync();
                
                return new BaseResponse<List<Walk>>
                {
                    Date = GetWalks(tracks)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Walk>>
                {
                    Description = ex.Message
                };
            }           
        }

        private List<Walk> GetWalks(TrackLocation[] tracks)
        {
            var walks = new List<Walk>();

            var walk = new Walk();
            for (int i = 0; i < tracks.Length - 1; i++)
            {
                if (i == 0)
                {
                    walk.Start = tracks[i].DateTrack;
                }
                else if (tracks[i + 1].DateTrack.Subtract(tracks[i].DateTrack).TotalMinutes > 30)
                {
                    walk.End = tracks[i].DateTrack;
                    walk.Duration = walk.End.Subtract(walk.Start);
                    walks.Add(walk);

                    walk = new Walk();
                    walk.Start = tracks[i + 1].DateTrack;
                }
                else if (tracks[i].Latitude != tracks[i + 1].Latitude ||
                    tracks[i].Longitude != tracks[i + 1].Longitude)
                {
                    walk.Distance += Dist(tracks[i].Latitude, tracks[i].Longitude, tracks[i + 1].Latitude, tracks[i + 1].Longitude);
                }
            }
            return walks;
        }

        private double Dist(decimal lat1, decimal lon1, decimal lat2, decimal lon2)
        {            
            double lat1Double = Convert.ToDouble(lat1);
            double lat2Double = Convert.ToDouble(lat2);
            double lon1Double = Convert.ToDouble(lon1);
            double lon2Double = Convert.ToDouble(lon2);
            double distance = Math.Acos(Math.Sin(lat1Double) * 
                Math.Sin(lat2Double) + 
                Math.Cos(lat1Double) * 
                Math.Cos(lat2Double) * 
                Math.Cos(lon1Double - lon2Double)) * 
                6371;
            return distance;
        }

        private double Distance(decimal lat1, decimal lon1, decimal lat2, decimal lon2)
        {           
            double lat1Rad = ToRadians(lat1);
            double lon1Rad = ToRadians(lon1);
            double lat2Rad = ToRadians(lat2);
            double lon2Rad = ToRadians(lon2);
            
            double dlat = lat2Rad - lat1Rad;
            double dlon = lon2Rad - lon1Rad;
            double a = Math.Sin(dlat / 2) * Math.Sin(dlat / 2) +
                       Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                       Math.Sin(dlon / 2) * Math.Sin(dlon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = RADIUS_OF_EARTH * c;

            return distance;           
        }

        private double ToRadians(decimal degrees)
        {
            return  Convert.ToDouble(degrees) * Math.PI / 180;
        }

    }
}
