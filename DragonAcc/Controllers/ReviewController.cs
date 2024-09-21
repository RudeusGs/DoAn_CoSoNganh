using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models.Review;
using DragonAcc.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;

namespace DragonAcc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : BaseController
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _reviewService.GetAll();
            return Response(result);
        }
        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] AddReviewModel model)
        {
            try
            {
                var result = await _reviewService.Add(model);
                return Response(result);
            }
            catch (Exception e)
            {
                return Response(e.Message, 500);
            }
        }
    }
}
