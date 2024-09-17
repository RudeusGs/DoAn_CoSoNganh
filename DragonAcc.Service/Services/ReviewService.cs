using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.Review;
using HocWeb.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace DragonAcc.Service.Services
{
    public class ReviewService : BaseService, IReviewService
    {
        private readonly ICurrentUserService _currentUserService;
        public ReviewService(DataContext dataContext, Common.IServices.IUserService userService, ICurrentUserService currentUserService) : base(dataContext, userService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<ApiResult> Add(Review model)
        {
            return null;
        }
        public async Task<ApiResult> Add(AddReviewModel model)
        {
            var userId = await _currentUserService.GetCurrentUserIdAsync();

            if (!userId.HasValue)
            {
                return new ApiResult { Message = "User not authenticated" };
            }

            using var tran = _dataContext.Database.BeginTransaction();
            try
            {
                var newReview = new Review
                {
                    UserId = userId.Value,
                    Comment = model.Comment,
                    Rating = model.Rating,
                    CreatedDate = DateTime.UtcNow
                };

                _dataContext.Reviews.Add(newReview);
                await _dataContext.SaveChangesAsync();
                await tran.CommitAsync();
                return new ApiResult { Data = newReview };
            }
            catch (Exception e)
            {
                await tran.RollbackAsync();
                throw new Exception(e.Message);
            }
        }

        public async Task<ApiResult> GetUserId()
        {
            var result = await _dataContext.Reviews
               .Where(x => x.UserId == _userService.UserId).ToListAsync();
            return new(result);
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
