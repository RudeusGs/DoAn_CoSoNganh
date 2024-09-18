using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Common.Services;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.Auction;
using Microsoft.EntityFrameworkCore;

namespace DragonAcc.Service.Services
{

    public class AuctionService : BaseService, IAuctionService
    {
        public AuctionService(DataContext dataContext, IUserService userService) : base(dataContext, userService)
        {
        }

        public Task<ApiResult> Add(Auction model)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult> Add(AddAuctionModel model)
        {
            var auction = await _dataContext.Auctions.FirstOrDefaultAsync(x => x.AuctionName == model.AuctionName);
            if(auction == null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    var newAuction = new Auction
                    {
                        AuctionName = model.AuctionName,
                        GameAccountId = model.GameAccountId,
                        InGameItemId = model.InGameItemId,
                        UserId = _userService.UserId,
                        StartPrice = model.StartPrice,
                        CurrentPrice = model.CurrentPrice,
                        StartDateTime = model.StartDateTime,
                        TimeAuction = model.TimeAuction,
                        CreatedDate = DateTime.Now,
                    };
                    _dataContext.Auctions.Add(newAuction);
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new(newAuction);
                }
                catch(Exception ex)
                {
                    await tran.RollbackAsync();
                    throw new Exception(ex.Message);
                }
            }
            return new() { Message = "Phiên đấu giá này đã tồn tại" };
        }

        public async Task<ApiResult> Delete(int id)
        {
            var auction = await _dataContext.Auctions.FirstOrDefaultAsync(x => x.Id == id);
            if (auction != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    auction.DeleteDate = _now;
                    _dataContext.Auctions.Remove(auction);
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
            return new ApiResult() { Message = "Không tìm thấy đấu giá này." };
        }

        public Task<ApiResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult> Update(Auction model)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult> Update(UpdateAuctionModel model)
        {
            throw new NotImplementedException();
        }
    }
}
