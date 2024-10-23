using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.CommentOrLike;
using Microsoft.EntityFrameworkCore;

namespace DragonAcc.Service.Services
{
    public class CommentOrLikeService : BaseService, ICommentOrLikeService
    {
        public CommentOrLikeService(DataContext dataContext, IUserService userService) : base(dataContext, userService)
        {
        }

        public Task<ApiResult> Add(CommentOrLike model)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult> AddCommentOrLike(AddCommentOrLikeModel model)
        {
            var commentOrLike = await _dataContext.CommentOrLikes.FirstOrDefaultAsync();

                using var tran = _dataContext.Database.BeginTransaction();
            try
            {
                var newComment = new CommentOrLike
                {
                    Comment = model.Comment,
                    UserId = _userService.UserId,
                    GameAccountId = model.GameAccountId,
                    CreatedDate = _now
                };

                _dataContext.CommentOrLikes.Add(newComment);
                await _dataContext.SaveChangesAsync();
                await tran.CommitAsync();
                return new(newComment);
            }
            catch (Exception e)
            {
                await tran.RollbackAsync();
                throw new Exception(e.Message);
            }
        }

        public async Task<ApiResult> Delete(int id)
        {
            var commentOrLike = await _dataContext.CommentOrLikes.FirstOrDefaultAsync(x => x.Id == id);
            if (commentOrLike != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    _dataContext.CommentOrLikes.Remove(commentOrLike);
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new();
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }
            return new ApiResult() { Message = "Không tìm thấy bình luận này này." };
        }

        public async Task<ApiResult> GetAll()
        {
            var result = await _dataContext.CommentOrLikes.ToListAsync();
            return new(result);
        }

        public async Task<ApiResult> GetById(int id)
        {
            var result = await _dataContext.CommentOrLikes.FirstOrDefaultAsync(x => x.Id == id);
            return new(result);
        }

        public Task<ApiResult> Update(CommentOrLike model)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult> UpdateCommentOrLike(UpdateCommentOrLikeModel model)
        {
            var commentOrLike = await _dataContext.CommentOrLikes.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (commentOrLike != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    commentOrLike.Comment = model.Comment ?? commentOrLike.Comment;
                    commentOrLike.UpdatedDate = _now;

                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();

                    return new() { Message = "Cập nhật thành công!" };
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }

            return new() { Message = "Không tìm thấy bình luận này game này." };
        }
    }
}
