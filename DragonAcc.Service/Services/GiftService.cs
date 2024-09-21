using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DragonAcc.Service.Services
{
    public class GiftService : BaseService, IGiftService
    {
        public GiftService(DataContext dataContext,Common.IServices.IUserService userService) : base(dataContext, userService)
        {            
        }
        public async Task<ApiResult> GetAll()
        {
            var result = await _dataContext.Gifts.ToListAsync();
            return new(result);
        }
        private string GenerateGiftCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public async Task<ApiResult> Add(Gift model)
        {
            var existingGift = await _dataContext.Gifts
                .FirstOrDefaultAsync(x => x.GiftCode == model.GiftCode);
            using var tran = await _dataContext.Database.BeginTransactionAsync();
            try
            {
                model.GiftCode = GenerateGiftCode();
                model.CreatedDate = DateTime.Now;
                _dataContext.Gifts.Add(model);
                await _dataContext.SaveChangesAsync();
                await tran.CommitAsync();

                return new ApiResult(model);
            }
            catch (Exception e)
            {
                await tran.RollbackAsync();
                return new ApiResult
                {
                    Message = "Có lỗi xảy ra khi thêm quà tặng: " + e.Message,
                };
            }
        }

        public async Task<ApiResult> Delete(int id)
        {
            var expiredGifts = await _dataContext.Gifts
                .Where(x => x.Expiry.HasValue && x.Expiry.Value <= DateTime.Now)
                .ToListAsync();

            if (expiredGifts.Count == 0)
            {
                return new ApiResult
                {
                    Message = "Không có mã quà tặng nào hết hạn để xóa!",
                };
            }

            using var tran = await _dataContext.Database.BeginTransactionAsync();
            try
            {
                _dataContext.Gifts.RemoveRange(expiredGifts);
                await _dataContext.SaveChangesAsync();
                await tran.CommitAsync();

                return new ApiResult
                {
                    Message = "Các mã quà tặng hết hạn đã được xóa thành công!",
                };
            }
            catch (Exception e)
            {
                await tran.RollbackAsync();
                return new ApiResult
                {
                    Message = "Có lỗi xảy ra khi xóa các mã quà tặng hết hạn: " + e.Message,
                };
            }
        }
        public async Task<ApiResult> Delete()
        {
            var expiredGifts = await _dataContext.Gifts
                .Where(x => x.Expiry.HasValue && x.Expiry.Value <= DateTime.Now)
                .ToListAsync();

            if (expiredGifts.Count != 0)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    int deletedCount = expiredGifts.Count;
                    _dataContext.Gifts.RemoveRange(expiredGifts);
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();

                    return new ApiResult
                    {
                        Message = $"Các mã quà tặng hết hạn đã được xóa thành công! Số lượng đã xóa: {deletedCount}",
                    };
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    return new ApiResult
                    {
                        Message = $"Có lỗi xảy ra khi xóa các mã quà tặng hết hạn: {e.Message}",
                    };
                }
            }

            return new ApiResult
            {
                Message = "Không có mã quà tặng nào hết hạn cần xóa."
            };
        }

        public Task<ApiResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult> Update(Gift model)
        {
            throw new NotImplementedException();
        }
    }
}
