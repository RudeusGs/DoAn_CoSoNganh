using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.Review;
using Microsoft.EntityFrameworkCore;

namespace DragonAcc.Service.Services
{
    public class ReviewService : BaseService, IReviewService
    {
        public ReviewService(DataContext dataContext, Common.IServices.IUserService userService) : base(dataContext, userService)
        {
        }

        public async Task<ApiResult> Add(Review model)
        {
            return null;
        }
        public async Task<ApiResult> Add(AddReviewModel model)
        {
            var review = new Review
            {
                Comment = model.Comment,
                Rating = model.Rating,
                UserId = _userService.UserId,
                CreatedDate = DateTime.Now
            };

            await _dataContext.Reviews.AddAsync(review);
            await _dataContext.SaveChangesAsync();
            return new ApiResult(review);
        }
        public async Task<ApiResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult> GetAll()
        {
            var result = await _dataContext.Reviews.ToListAsync();
            return new(result);
        }

        public async Task<ApiResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult> Update(Review model)
        {
            throw new NotImplementedException();
        }
    }
}
