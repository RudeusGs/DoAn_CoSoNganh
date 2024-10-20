using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models.Auction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DragonAcc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountWebsiteController : BaseController
    {
        private readonly IAccountWebsiteService _accountWebsiteService;
        public AccountWebsiteController(IAccountWebsiteService accountWebsiteService)
        {
            _accountWebsiteService = accountWebsiteService;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _accountWebsiteService.GetAll();
            return Response(result);
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            try
            {
                var result = await _accountWebsiteService.GetById(id);
                return Response(result);
            }
            catch (Exception e)
            {
                return Response(e.Message, 500);
            }
        }

    }
}