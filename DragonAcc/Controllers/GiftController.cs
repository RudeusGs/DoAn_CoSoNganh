using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models.GameService;
using DragonAcc.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DragonAcc.Controllers
{
    public class GiftController : BaseController
    {
        private readonly IGiftService _giftService;
        public GiftController(IGiftService giftService)
        {
            _giftService = giftService;
        }
        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] Gift model)
        {
            try
            {
                var result = await _giftService.Add(model);
                return Response(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [Authorize]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete()
        {
            try
            {
                var result = await _giftService.Delete();
                return Response(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
