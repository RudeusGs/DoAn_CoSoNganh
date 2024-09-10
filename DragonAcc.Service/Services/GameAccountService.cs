using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Infrastructure.Extensions;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Common.Services;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.AccountGame;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DragonAcc.Service.Services
{
    public class GameAccountService : BaseService, IGameAccountService
    {
        private readonly IFtpDirectoryService _ftpDirectoryService;

        public GameAccountService(DataContext dataContext, IUserService userService) : base(dataContext, userService)
        {
        }

        public async Task<ApiResult> Add(GameAccount model)
        {
            var gameaccount = await _dataContext.GameAccounts.FirstOrDefaultAsync(x => x.AccountName == model.AccountName && x.AccountName == model.AccountName);
            if (gameaccount == null)
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

        private async Task<string?> UploadFile(int? GameaccountId, IFormFile? file)
        {
            if (file == null || !GameaccountId.HasValue)
            {
                return string.Empty;
            }
            var fileExt = Path.GetExtension(file.FileName);
            var stream = file.OpenReadStream();
            var result = await _ftpDirectoryService.TransferToFtpDirectoryAsync(stream, $"public/gameaccount", $"{GameaccountId}{fileExt}");
            if (result.Succeed)
            {
                return $"Gameaccount/{GameaccountId}{fileExt}";
            }
            return string.Empty;
        }
        /**/
        public async Task<ApiResult> Add(AddGameAccountModel model)
        {
            var gameAccount = await _dataContext.GameAccounts.FirstOrDefaultAsync(x => x.AccountName == model.AccountName);

            if (gameAccount == null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    var newGameAccount = new GameAccount
                    {
                        AccountName = model.AccountName,
                        AccountPassword = model.AccountPassword,
                        Status = model.Status,
                        Server = model.Server,
                        Earring = model.Earring ?? false,
                        Planet = model.Planet,
                        Price = model.Price,
                        UpdatedDate = _now
                    };

                    _dataContext.GameAccounts.Add(newGameAccount);
                    await _dataContext.SaveChangesAsync();

                    if (model.File != null)
                    {
                        var fileUpload = await UploadFile(newGameAccount.Id, model.File);
                        if (!string.IsNullOrEmpty(fileUpload))
                        {
                            newGameAccount.Image = fileUpload;                           
                        }
                    }
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new(newGameAccount);
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }

            return new ()
            {
                Message = "Tài khoản này đã tồn tại! Vui lòng nhập thêm tài khoản khác."
            };
        }

        public async Task<ApiResult> GetAll2()
        {
            var result = await _dataContext.GameAccounts
                              .OrderBy(gameaccount => gameaccount.DeleteDate.HasValue ? 1 : 0)
                              .ThenBy(gameaccount => gameaccount.DeleteDate)
                              .ToListAsync();
            return new(result);
        }

        public async Task<ApiResult> Update(UpdateGameAccountModel model)
        {
            return null;
        }

    }
}
