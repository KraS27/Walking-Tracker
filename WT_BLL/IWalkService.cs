using WT_Models.Entities;

namespace WT_BLL
{
    public interface IWalkService
    {
        public Task<BaseResponse<List<Walk>>> GetWalksAsync();
    }
}
