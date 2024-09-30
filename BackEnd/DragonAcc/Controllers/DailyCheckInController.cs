using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models.AccountGame;
using DragonAcc.Service.Models.DailyCheckIn;
using DragonAcc.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DragonAcc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyCheckInController : BaseController
    {
        private readonly IDailyCheckInService _dailycheckinservice;

        public DailyCheckInController(IDailyCheckInService dailycheckinservice)
        {
            _dailycheckinservice = dailycheckinservice;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dailycheckinservice.GetAll();
            return Response(result);
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            try
            {
                var result = await _dailycheckinservice.GetById(id);
                return Response(result);
            }
            catch (Exception e)
            {
                return Response(e.Message, 500);
            }
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] AddDailyCheckInModel model)
        {
            try
            {
                var result = await _dailycheckinservice.Add(model);
                return Response(result);
            }
            catch (Exception e)
            {
                return Response(e.Message, 500);
            }
        }
        [Authorize]
        [HttpPost("add2")]
        public async Task<IActionResult> Add2([FromForm] CheckInModel model)
        {
            try
            {
                var result = await _dailycheckinservice.Add2(model);
                return Response(result);
            }
            catch (Exception e)
            {
                return Response(e.Message, 500);
            }
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm] UpdateDailyCheckInModel model)
        {
            try
            {
                var result = await _dailycheckinservice.Update(model);
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
                var result = await _dailycheckinservice.Delete(id);
                return Response(result);
            }
            catch (Exception e)
            {
                return Response(e.Message, 500);
            }
        }
    }
}

