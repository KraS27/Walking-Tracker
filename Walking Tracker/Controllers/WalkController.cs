﻿using Microsoft.AspNetCore.Http;
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

        [HttpGet("/walks")]
        public async Task<BaseResponse<List<Walk>>> GetWalksAsync()
        {
            var walks = await _walkService.GetAllWalksAsync();
            return walks;
        }

        [HttpGet("/walks/date")]
        public async Task<WalksResponse> GetWalksByDayAsync(DateTime date)
        {
            var walks = await _walkService.GetWalksByDayAsync(date);
            return walks;
        }

        [HttpGet("/walks/imei")]
        public async Task<WalksResponse> GetWalksByIMEIAsync(string IMEI)
        {
            var walks = await _walkService.GetWalksByIMEIAsync(IMEI);
            return walks;
        }

        [HttpGet("/walks/top")]
        public async Task<BaseResponse<List<Walk>>> GetTopWalksAsync(int count)
        {
            var walks = await _walkService.GetTopWalksAsync(count);
            return walks;
        }


    }
}
