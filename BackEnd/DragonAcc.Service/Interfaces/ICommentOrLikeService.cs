using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.CommentOrLike;

namespace DragonAcc.Service.Interfaces
{
    public interface ICommentOrLikeService : IServiceBase<CommentOrLike>
    {
        Task<ApiResult> AddCommentOrLike (AddCommentOrLikeModel model);
        Task<ApiResult> UpdateCommentOrLike(UpdateCommentOrLikeModel model);
    }
}
