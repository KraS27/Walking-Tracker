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
        private readonly IBaseRepository<TrackLocation> _trackRepository;

        public WalkService(IBaseRepository<TrackLocation> trackRepository)
        {
            _trackRepository = trackRepository;
        }

        public async Task<BaseResponse<List<Walk>>> GetWalksAsync()
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
            }
            return walks;
        }
    }
}
