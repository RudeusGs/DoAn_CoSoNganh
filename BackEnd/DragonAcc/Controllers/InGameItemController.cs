using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models.GameService;
using DragonAcc.Service.Models.InGameItem;
using DragonAcc.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DragonAcc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InGameItemController : BaseController
    {
        private readonly IIngameItemService _ingameItem;
        public InGameItemController(IIngameItemService ingameitem)
        {
            _ingameItem = ingameitem;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _ingameItem.GetAll();
            return Response(result);
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            try
            {
                var result = await _ingameItem.GetById(id);
                return Response(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] AddInGameItemModel model)
        {
            try
            {
                var result = await _ingameItem.Add(model);
                return Response(result);
            }
            catch (Exception e)
            {
                return Response(e.Message, 500);
            }
        }
        [Authorize]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
                try
                {
                    var result = await _ingameItem.Delete(id);
                    return Response(result);
                }
                catch (Exception e)
                {
                    return Response(e.Message, 500);
                }

        }
        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] InGameItem model)
        {
            try
            {
                var result = await _ingameItem.Update(model);
                return Response(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
