using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.Auction;

namespace DragonAcc.Service.Interfaces
{
    public interface IAuctionService : IServiceBase<Auction>
    {
        Task<ApiResult> Add(AddAuctionModel model);
        Task<ApiResult> Update(UpdateAuctionModel model);
    }
}
