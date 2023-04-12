using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WT_Models.Entities;

namespace WT_DAL
{
    public class TrackRepository : IBaseRepository<TrackLocation>
    {
        private readonly AppDbContext _appDbContext;

        public TrackRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task Create(TrackLocation entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(TrackLocation entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TrackLocation> GetAll()
        {
            return _appDbContext.TrackLocations;
        }

        public Task<TrackLocation> Update(TrackLocation entity)
        {
            throw new NotImplementedException();
        }
    }
}
