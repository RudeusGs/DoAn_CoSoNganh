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
    public class PurchasedAccountController : BaseController
    {
        private readonly IPurchasedAccountService _purchasedAccountService;
        public PurchasedAccountController(IPurchasedAccountService purchasedAccountService)
        {
            _purchasedAccountService = purchasedAccountService;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _purchasedAccountService.GetAll();
            return Response(result);
        }
    }
}