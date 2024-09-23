using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Infrastructure.Extensions;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Common.Services;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.AccountGame;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net.NetworkInformation;
using System.Numerics;

namespace DragonAcc.Service.Services
{
    public class GameAccountService : BaseService, IGameAccountService
    {
        private readonly IFtpDirectoryService _ftpDirectoryService;

        public GameAccountService(DataContext dataContext, IFtpDirectoryService ftpDirectoryService,
            Common.IServices.IUserService userService) : base(dataContext, userService)
        {
            _ftpDirectoryService = ftpDirectoryService;
        }

        public async Task<ApiResult> Add(GameAccount model)
        {
            return null;
        }

        public async Task<ApiResult> Delete(int id)
        {
            var gameaccount = await _dataContext.GameAccounts.FirstOrDefaultAsync(x => x.Id == id);
            if (gameaccount != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
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

        private async Task<string?> UploadFile(int? accountId, IFormFile? file)
        {
            if (file == null || !accountId.HasValue)
            {
                return string.Empty;
            }
            var fileExt = Path.GetExtension(file.FileName);
            var stream = file.OpenReadStream();
            var result = await _ftpDirectoryService.TransferToFtpDirectoryAsync(stream, $"public/gameaccounts", $"{accountId}{fileExt}");
            if (result.Succeed)
            {
                return $"gameaccounts/{accountId}{fileExt}";
            }
            return string.Empty;
        }
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
                        CreatedDate = _now
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

            return new()
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
            var gameAccount = await _dataContext.GameAccounts
                                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (gameAccount != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    if (string.IsNullOrEmpty(model.AccountName) &&
                        string.IsNullOrEmpty(model.AccountPassword) &&
                        string.IsNullOrEmpty(model.Price) &&
                        string.IsNullOrEmpty(model.Status) &&
                        model.File == null &&
                        string.IsNullOrEmpty(model.Planet) &&
                        model.Server == null &&
                        model.Earring == null)
                    {
                        return new() { Message = "Không có thông tin nào được cập nhật" };
                    }

                    gameAccount.AccountName = model.AccountName ?? gameAccount.AccountName;
                    gameAccount.AccountPassword = model.AccountPassword ?? gameAccount.AccountPassword;
                    gameAccount.Price = model.Price ?? gameAccount.Price;
                    gameAccount.Status = model.Status ?? gameAccount.Status;
                    gameAccount.Planet = model.Planet ?? gameAccount.Planet;
                    gameAccount.Server = model.Server ?? gameAccount.Server;
                    gameAccount.Earring = model.Earring ?? gameAccount.Earring;
                    gameAccount.UpdatedDate = _now;

                    if (model.File != null)
                    {
                        var fileUpload = await UploadFile(gameAccount.Id, model.File);
                        if (!string.IsNullOrEmpty(fileUpload))
                        {
                            gameAccount.Image = fileUpload;
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

            return new() { Message = "Không tìm thấy tài khoản game này." };
        }


    }
}