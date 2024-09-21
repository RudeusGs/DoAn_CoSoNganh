using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.Auction;

namespace DragonAcc.Service.Interfaces
{
    public interface IAuctionService : IServiceBase<Auction>
    {
        public Task<ApiResult> Add(AddAuctionModel model);
        public Task<ApiResult> Update(UpdateAuctionModel model);
        public Task<ApiResult> UpdateCurrentPrice(UpdateCurrentPriceModel model);
    }
}
