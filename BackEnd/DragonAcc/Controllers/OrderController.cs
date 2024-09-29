using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DragonAcc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderService.GetAll();
            return Response(result);
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var result = await _orderService.GetById(id);
            return Response(result);
        }

        [HttpGet("get-by-user-id")]
        public async Task<IActionResult> GetByUserId()
        {
            var result = await _orderService.GetOrderByUserId();
            return Response(result);
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] Order model)
        {
            var result = await _orderService.Add(model);
            return Response(result);
        }

        [Authorize]
        [HttpPost("add-new")]
        public async Task<IActionResult> CreateNew([FromForm] OrderModel model)
        {
            var result = await _orderService.CreateNew(model);
            return Response(result);
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var result = await _orderService.Delete(id);
            return Response(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm] Order model)
        {
            var result = await _orderService.Update(model);
            return Response(result);
        }
    }
}
