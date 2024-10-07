using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.AccountGame;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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

        private async Task<List<string>> UploadFiles(int? accountId, List<IFormFile>? files)
        {
            var uploadedFilePaths = new List<string>();

            if (files == null || !accountId.HasValue)
            {
                return uploadedFilePaths;
            }

            var accountFolder = $"public/GameAccounts/{accountId}";

            foreach (var file in files)
            {
                var fileExt = Path.GetExtension(file.FileName);
                var stream = file.OpenReadStream();


                var fileName = $"{accountId}.{uploadedFilePaths.Count + 1}{fileExt}";

                var result = await _ftpDirectoryService.TransferToFtpDirectoryAsync(stream, accountFolder, fileName);

                if (result.Succeed)
                {
                    uploadedFilePaths.Add($"{accountFolder}/{fileName}");
                }
            }

            return uploadedFilePaths;
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
                        Name = _userService.UserName,
                        Created = DateTime.UtcNow,
                        CreatedDate = _now
                    };

                    _dataContext.GameAccounts.Add(newGameAccount);
                    await _dataContext.SaveChangesAsync();

                    if (model.Files != null && model.Files.Any())
                    {
                        var fileUploads = await UploadFiles(newGameAccount.Id, model.Files);
                        if (fileUploads.Any())
                        {
                            newGameAccount.Image = string.Join(";", fileUploads);
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
            var gameAccount = await _dataContext.GameAccounts.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (gameAccount != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    gameAccount.AccountName = model.AccountName ?? gameAccount.AccountName;
                    gameAccount.AccountPassword = model.AccountPassword ?? gameAccount.AccountPassword;
                    gameAccount.Price = model.Price ?? gameAccount.Price;
                    gameAccount.Status = model.Status ?? gameAccount.Status;
                    gameAccount.Planet = model.Planet ?? gameAccount.Planet;
                    gameAccount.Server = model.Server ?? gameAccount.Server;
                    gameAccount.Earring = model.Earring ?? gameAccount.Earring;
                    gameAccount.UpdatedDate = _now;

                    if (model.Files != null && model.Files.Any())
                    {
                        var fileUploads = await UploadFiles(gameAccount.Id, model.Files);
                        if (fileUploads.Any())
                        {
                            gameAccount.Image = string.Join(";", fileUploads);
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