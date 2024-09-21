using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
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
                    var timeAuction = TimeSpan.Parse(model.TimeAuction);
                    var newAuction = new Auction
                    {
                        Prize = model.Prize,
                        AuctionName = model.AuctionName,
                        StartPrice = model.StartPrice,
                        StartDateTime = model.StartDateTime,
                        TimeAuction = timeAuction.ToString(@"hh\:mm\:ss"),
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

        public async Task<ApiResult> GetAll()
        {
            var result = await _dataContext.Auctions.ToListAsync();
            return new(result);
        }

        public Task<ApiResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult> Update(Auction model)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult> Update(UpdateAuctionModel model)
        {
            var auction = await _dataContext.Auctions.FirstOrDefaultAsync(x => x.AuctionName == model.AucionName);
            if (auction != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    auction.StartPrice = model.StartPrice;
                    auction.StartDateTime = model.StartDateTime;
                    auction.TimeAuction = model.TimeAuction;
                    auction.UpdatedDate = _now;
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
            return new ApiResult() { Message = "Không tìm thấy phiên đấu giá này." };
        }
        public async Task<ApiResult> UpdateCurrentPrice(UpdateCurrentPriceModel model)
        {
            var auction = await _dataContext.Auctions.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (auction == null)
            {
                return new ApiResult() { Message = "Không tìm thấy phiên đấu giá." };
            }
            if (decimal.TryParse(model.CurrentPrice, out decimal newPrice))
            {
                if (newPrice > decimal.Parse(auction.StartPrice))
                {
                    using var tran = await _dataContext.Database.BeginTransactionAsync();
                    try
                    {
                        auction.StartPrice = newPrice.ToString();
                        await _dataContext.SaveChangesAsync();
                        await tran.CommitAsync();
                        return new ApiResult() { Message = "Cập nhật giá thành công.", Data = auction };
                    }
                    catch (Exception ex)
                    {
                        await tran.RollbackAsync();
                        throw new Exception(ex.Message);
                    }
                }
                else
                {
                    return new ApiResult() { Message = "Giá đặt mới thấp hơn giá hiện tại." };
                }
            }
            else
            {
                return new ApiResult() { Message = "Số tiền không hợp lệ." };
            }
        }

    }
}
