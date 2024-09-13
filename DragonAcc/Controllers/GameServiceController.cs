using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using DragonAcc.Service.Services;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models.GameService;

namespace DragonAcc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameServiceController : BaseController
    {
        private IGameServiceService _gameServiceService;
        public GameServiceController(IGameServiceService gameServiceService)
        {
            _gameServiceService = gameServiceService;
        }

        [Authorize]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _gameServiceService.GetAll2();
            return Response(result);
        }

        [Authorize]
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(int Id)
        {
            var result = await _gameServiceService.GetById(Id);
            return Response(result);
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                var result = await _gameServiceService.Delete(id);
                return Response(result);
            }
            catch (Exception e)
            {
                return Response(e, 500);
            }
        }
        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddGameServiceModel model)
        {
            try
            {
                var result = await _gameServiceService.Add(model);
                return Response(result);
            }
            catch (Exception e)
            {
                return Response(e, 500);
            }
        }
    }
}
