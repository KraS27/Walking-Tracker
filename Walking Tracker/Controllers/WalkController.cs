using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WT_BLL;
using WT_Models.Entities;

namespace Walking_Tracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : ControllerBase
    {
        private readonly IWalkService _walkService;

        public WalksController(IWalkService walkService)
        {
            _walkService = walkService;
        }

        [HttpGet("walk")]
        public async Task<BaseResponse<List<Walk>>> GetWalksAsync()
        {
            var walks = await _walkService.GetAllWalksAsync();
            return walks;
        }
    }
}
