using DragonAcc.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DragonAcc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : BaseController
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }
        [HttpGet("get-order-by-oder-id")]
        public async Task<IActionResult> GetOrderByOrderId([FromQuery] int oderId)
        {
            try
            {
                var result = await _orderDetailService.GetOrderDetailByOrderId(oderId);
                return Response(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
