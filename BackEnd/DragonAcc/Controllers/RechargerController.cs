using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models.Payment;
using DragonAcc.Service.Models.Recharger;
using DragonAcc.Service.Models.Transaction;
using DragonAcc.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DragonAcc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RechargerController : BaseController
    {
        private readonly IRechargerService _rechargerService;
        public RechargerController(IRechargerService rechargerService)
        {
            _rechargerService = rechargerService;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _rechargerService.GetAll();
            return Response(result);
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            try
            {
                var result = await _rechargerService.GetById(id);
                return Response(result);
            }
            catch (Exception e)
            {
                return Response(e.Message, 500);
            }
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] AddRechargerModel model)
        {
            try
            {
                var result = await _rechargerService.Add(model);
                return Response(result);
            }
            catch (Exception e)
            {
                return Response(e.Message, 500);
            }
        }


        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm] UpdateStatusRechargerModel model)
        {
            try
            {
                var result = await _rechargerService.Update(model);
                return Response(result);
            }
            catch (Exception e)
            {
                return Response(e.Message, 500);
            }
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                var result = await _rechargerService.Delete(id);
                return Response(result);
            }
            catch (Exception e)
            {
                return Response(e.Message, 500);
            }
        }
    }
}
