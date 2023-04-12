using WT_Models.Entities;

namespace WT_BLL
{
    public interface IWalkService
    {
        public Task<BaseResponse<List<Walk>>> GetAllWalksAsync();

        public Task<WalksResponse> GetWalksByDayAsync(DateTime date);

        public Task<WalksResponse> GetWalksByIMEIAsync(string IMEI);

        public Task<BaseResponse<List<Walk>>> GetTopWalksAsync(int count);
    }
}
