using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Common.Services;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.Auction;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;

namespace DragonAcc.Service.Services
{

    public class AuctionService : BaseService, IAuctionService
    {
        private readonly IFtpDirectoryService _ftpDirectoryService;
        public AuctionService(DataContext dataContext, IUserService userService, IFtpDirectoryService ftpDirectoryService) : base(dataContext, userService)
        {
            _ftpDirectoryService = ftpDirectoryService; 
        }

        public Task<ApiResult> Add(Auction model)
        {
            throw new NotImplementedException();
        }
        private async Task<string?> UploadFile(int? auctionId, IFormFile? file)
        {
            if (file == null || !auctionId.HasValue)
            {
                return string.Empty;
            }
            var fileExt = Path.GetExtension(file.FileName);
            var stream = file.OpenReadStream();
            var result = await _ftpDirectoryService.TransferToFtpDirectoryAsync(stream, $"public/Auctions", $"{auctionId}{fileExt}");
            if (result.Succeed)
            {
                return $"Auctions/{auctionId}{fileExt}";
            }
            return string.Empty;
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
                        CurrentPrice = model.StartPrice,
                        StartDateTime = model.StartDateTime,
                        Status = "Sắp tới",
                        TimeAuction = timeAuction.ToString(@"hh\:mm\:ss"),
                        Winner = null,
                        CreatedDate = DateTime.Now,
                    };
                    _dataContext.Auctions.Add(newAuction);
                    await _dataContext.SaveChangesAsync();

                    if (model.File != null)
                    {
                        var fileUpload = await UploadFile(newAuction.Id, model.File);
                        if (!string.IsNullOrEmpty(fileUpload))
                        {
                            newAuction.Image = fileUpload;
                        }
                    }
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
            return new() { Message = "Không tìm thấy đấu giá này." };
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
            var auction = await _dataContext.Auctions
                             .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (auction != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    if (string.IsNullOrEmpty(model.AucionName) &&
                        string.IsNullOrEmpty(model.StartPrice) &&
                        model.StartDateTime == null &&
                        string.IsNullOrEmpty(model.TimeAuction) &&
                        model.File == null &&
                        model.Prize == null)
                    {
                        return new() { Message = "Không có thông tin nào được cập nhật" };
                    }
                    auction.AuctionName = model.AucionName ?? auction.AuctionName;
                    auction.Prize = model.Prize ?? auction.Prize;
                    auction.StartPrice = model.StartPrice ?? auction.StartPrice;
                    auction.StartDateTime = model.StartDateTime ?? auction.StartDateTime;
                    auction.CurrentPrice = model.StartPrice ?? auction.StartPrice;
                    auction.TimeAuction = model.TimeAuction ?? auction.TimeAuction;           
                    auction.UpdatedDate = _now;

                    if (model.File != null)
                    {
                        var fileUpload = await UploadFile(auction.Id, model.File);
                        if (!string.IsNullOrEmpty(fileUpload))
                        {
                            auction.Image = fileUpload;
                        }
                    }
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

            return new() { Message = "Không tìm thấy phiên đấu giá này." };
        }

        public async Task<ApiResult> UpdateCurrentPrice(UpdateCurrentPriceModel model)
        {
            var auction = await _dataContext.Auctions.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (auction == null)
            {
                return new() { Message = "Không tìm thấy phiên đấu giá." };
            }
            if (decimal.TryParse(model.CurrentPrice, out decimal newPrice))
            {
                if (newPrice > decimal.Parse(auction.CurrentPrice))
                {
                    using var tran = await _dataContext.Database.BeginTransactionAsync();
                    try
                    {
                        auction.Winner = _userService.UserId;
                        auction.CurrentPrice = newPrice.ToString();
                        await _dataContext.SaveChangesAsync();
                        await tran.CommitAsync();
                        return new();
                    }
                    catch (Exception ex)
                    {
                        await tran.RollbackAsync();
                        throw new Exception(ex.Message);
                    }
                }
                else
                {
                    return new() { Message = "Giá đặt mới thấp hơn giá hiện tại." };
                }
            }
            else
            {
                return new() { Message = "Số tiền không hợp lệ." };
            }
        }



    }
}
