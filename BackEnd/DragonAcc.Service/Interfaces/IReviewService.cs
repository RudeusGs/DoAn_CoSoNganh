using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Models.GameService;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.Review;

namespace DragonAcc.Service.Interfaces
{
    public interface IReviewService : IServiceBase<Review>
    {
        public Task<ApiResult> Add(AddReviewModel model);
    }
}
