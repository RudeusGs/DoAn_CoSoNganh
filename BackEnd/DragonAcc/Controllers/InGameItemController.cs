using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models.InGameItem;
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
            [Authorize]
            [HttpGet("get-all")]
            public async Task<IActionResult> GetAll()
            {
                var result = await _ingameItem.GetAll();
                return Response(result);
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
    }
}
