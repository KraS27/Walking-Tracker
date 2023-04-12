using WT_Models.Entities;

namespace WT_BLL
{
    public interface IWalkService
    {
        public Task<BaseResponse<List<Walk>>> GetAllWalksAsync();

        public Task<WalksByDayResponse> GetWalksByDayAsync(DateTime date);
    }
}
