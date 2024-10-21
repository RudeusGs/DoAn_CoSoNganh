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
                        UserId = _userService.UserId,
                        AccountPassword = model.AccountPassword,
                        Status = "Chưa bán",
                        Server = model.Server,
                        Earring = model.Earring ?? false,
                        Planet = model.Planet,
                        Content = model.Content,
                        Price = model.Price,
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
        public async Task<ApiResult> BuyGameAccount(BuyGameAccountModel model)
        {
            // Kiểm tra tham số GameAccountId
            if (model.GameAccountId == null)
            {
                return new ApiResult { Message = "Game account ID is required." };
            }

            // Lấy thông tin tài khoản game
            var gameAccount = await _dataContext.GameAccounts.FirstOrDefaultAsync(x => x.Id == model.GameAccountId);
            if (gameAccount == null)
            {
                return new ApiResult { Message = "Game account not found." };
            }

            // Kiểm tra trạng thái tài khoản game
            if (gameAccount.Status == "Đã bán")
            {
                return new ApiResult { Message = "Tài khoản này đã bán." };
            }

            // Lấy thông tin người dùng
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == model.UserId);
            if (user == null)
            {
                return new ApiResult { Message = "User not found." };
            }

            // Chuyển đổi Balance và Price từ string sang decimal
            if (!decimal.TryParse(user.Balance, out decimal userBalance))
            {
                return new ApiResult { Message = "Số dư của người dùng không hợp lệ." };
            }
            if (!decimal.TryParse(gameAccount.Price, out decimal accountPrice))
            {
                return new ApiResult { Message = "Giá của tài khoản game không hợp lệ." };
            }

            // Kiểm tra số dư
            if (userBalance < accountPrice)
            {
                return new ApiResult { Message = "Số dư không đủ để mua tài khoản này." };
            }

            // Bắt đầu giao dịch
            using var tran = await _dataContext.Database.BeginTransactionAsync();
            try
            {
                // Trừ số tiền từ số dư của người dùng
                user.Balance = (userBalance - accountPrice).ToString();

                // Tạo bản ghi PurchasedAccount mới
                var purchasedAccount = new PurchasedAccount
                {
                    UserId = model.UserId,
                    AccountName = gameAccount.AccountName,
                    AccountPassword = gameAccount.AccountPassword,
                    CreatedDate = DateTime.UtcNow, // Hoặc sử dụng _now nếu đã được định nghĩa
                };

                _dataContext.PurchasedAccounts.Add(purchasedAccount);

                // Cập nhật trạng thái tài khoản game
                gameAccount.Status = "Đã bán";

                // Lưu các thay đổi vào cơ sở dữ liệu
                await _dataContext.SaveChangesAsync();

                // Hoàn thành giao dịch
                await tran.CommitAsync();

                return new ApiResult { Message = "Mua tài khoản thành công." };
            }
            catch (Exception ex)
            {
                // Rollback giao dịch nếu có lỗi
                await tran.RollbackAsync();
                return new ApiResult { Message = $"Error during purchase: {ex.Message}" };
            }
        }

    }
}