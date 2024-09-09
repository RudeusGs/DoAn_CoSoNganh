using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace DragonAcc.Service.Services
{
    public class GameAccountService : BaseService, IGameAccountService
    {

        public GameAccountService(DataContext dataContext, IUserService userService) : base(dataContext, userService)
        {
        }

        public async Task<ApiResult> Add(GameAccount model)
        {
            var accountgame = await _dataContext.GameAccounts.FirstOrDefaultAsync(x => x.AccountName == model.AccountName);
            if (accountgame == null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    model.CreatedDate = _now;
                    _dataContext.GameAccounts.Add(model);
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new(model);
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }

            return new ApiResult() { Message = "Tài khoản này đã được thêm!." };
        }

        public async Task<ApiResult> Delete(int id)
        {
            var gameaccount = await _dataContext.GameAccounts.FirstOrDefaultAsync(x => x.Id == id);
            if (gameaccount != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    gameaccount.DeleteDate = _now;
                    _dataContext.GameAccounts.Remove(gameaccount);
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
            return new ApiResult() { Message = "Không tìm thấy tài khoản này." };
        }

        public async Task<ApiResult> GetAll()
        {
            var result = await _dataContext.GameAccounts.ToListAsync();
            return new(result);

        }

        public async Task<ApiResult> GetById(int id)
        {
            var result = await _dataContext.GameAccounts.FirstOrDefaultAsync(x => x.Id == id);

            return new(result);

        }

        public async Task<ApiResult> Update(GameAccount model)
        {
            var gameaccount = await _dataContext.GameAccounts.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (gameaccount != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    gameaccount.AccountName = model.AccountName;
                    gameaccount.AccountPassword = model.AccountPassword;
                    gameaccount.Status = model.Status;
                    gameaccount.Server = model.Server;
                    gameaccount.Earring = model.Earring;
                    gameaccount.Image = model.Image;
                    gameaccount.Planet = model.Planet;
                    gameaccount.Price = model.Price;
                    gameaccount.UpdatedDate = _now;
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
            return new ApiResult() { Message = "Không tìm thấy tài khoản này." };
        }
    }
}
