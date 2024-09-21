using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models.AccountGame;
using DragonAcc.Service.Models.Auction;
using DragonAcc.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DragonAcc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : BaseController
    {
        private readonly IAuctionService _auctionService;
        public AuctionController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }
        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] AddAuctionModel model)
        {
            try
            {
                var result = await _auctionService.Add(model);
                return Response(result);
            }
            catch (Exception e)
            {
                return Response(e.Message, 500);
            }
        }
        [Authorize]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _auctionService.Delete(id);
                return Response(result);
            }
            catch (Exception e)
            {
                return Response(e.Message, 500);
            }
        }
        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm] UpdateAuctionModel model)
        {
            try
            {
                var result = await _auctionService.Update(model);
                return Response(result);
            }
            catch (Exception e)
            {
                return Response(e.Message, 500);
            }
        }
        [Authorize]
        [HttpPost("update-current-price")]
        public async Task<IActionResult> UpdateCurrentPrice([FromForm] UpdateCurrentPriceModel model)
        {
            try
            {
                var result = await _auctionService.UpdateCurrentPrice(model);
                return Response(result);
            
            }
            catch(Exception e)
            {
                return Response(e.Message, 500);
            }
        }

    }
}
