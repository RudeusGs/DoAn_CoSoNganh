using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace DragonAcc.Service.Services
{
    public class PurchasedAccountService : BaseService, IPurchasedAccountService
    {
        public PurchasedAccountService(DataContext dataContext, IUserService userService)
            : base(dataContext, userService)
        {
        }

        public Task<ApiResult> Add(PurchasedAccount model)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult> Delete(int id)
        {
            var purchasedAccount = await _dataContext.PurchasedAccounts.FirstOrDefaultAsync(x => x.Id == id);
            if (purchasedAccount != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    _dataContext.PurchasedAccounts.Remove(purchasedAccount);
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();

                    return new();
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception($"Error while deleting purchased account: {e.Message}");
                }
            }
            return new ApiResult { Message = "Purchased account not found." };
        }
        public async Task<ApiResult> GetAllByUser(int userId)
        {
            var result = await _dataContext.PurchasedAccounts
                .Where(x => x.UserId == userId)
                .ToListAsync();
            return new(result);
        }

        public async Task<ApiResult> GetAll()
        {
            var result = await _dataContext.PurchasedAccounts.ToListAsync();
            return new(result);
        }

        public async Task<ApiResult> GetById(int id)
        {
            var result = await _dataContext.PurchasedAccounts.FirstOrDefaultAsync(x => x.Id == id);
            return new(result);
        }

        public Task<ApiResult> Update(PurchasedAccount model)
        {
            throw new NotImplementedException();
        }
    }
}
