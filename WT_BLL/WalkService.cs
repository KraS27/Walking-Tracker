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
                    Data = GetWalks(tracks)
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

        public async Task<WalksByDayResponse> GetWalksByDayAsync(DateTime date)
        {
            try
            {
                var tracks = await _trackRepository.GetAll()
                    .OrderBy(t => t.DateTrack)
                    .Where(t => t.DateTrack.DayOfYear == date.DayOfYear)
                    .ToArrayAsync();


                var walks = GetWalks(tracks);
                double totalDayWalkDistance = 0;
                TimeSpan totalDayWalkTime = new TimeSpan();

                foreach (var walk in walks)
                {
                    totalDayWalkDistance += walk.Distance;
                    totalDayWalkTime += walk.Duration;
                }

                return new WalksByDayResponse
                {
                    Data = walks,
                    TotalDayDistanceTraveled = totalDayWalkDistance,
                    TotalDayTimeTraveled = totalDayWalkTime
                };
            }
            catch (Exception ex)
            {
                return new WalksByDayResponse
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
                else if (tracks[i + 1].DateTrack.Subtract(tracks[i].DateTrack).TotalMinutes > 30 || i == tracks.Length - 2)
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
                    walk.Distance += Distance(tracks[i].Latitude, tracks[i].Longitude, tracks[i + 1].Latitude, tracks[i + 1].Longitude);
                }
            }
            return walks;
        }

        private double Distance(decimal lat1, decimal lon1, decimal lat2, decimal lon2)
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
                RADIUS_OF_EARTH;
            return distance;
        }      
    }
}
