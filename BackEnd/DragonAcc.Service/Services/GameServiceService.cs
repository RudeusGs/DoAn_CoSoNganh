using DragonAcc.Infrastructure.Entities;
using DragonAcc.Infrastructure;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.GameService;
using DragonAcc.Service.Common.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DragonAcc.Service.Services
{
    public class GameServiceService : BaseService, IGameServiceService
    {

        private readonly IFtpDirectoryService _ftpDirectoryService;

        public GameServiceService(DataContext dataContext, IUserService userService, IFtpDirectoryService ftpDirectoryService) : base(dataContext, userService)
        {
            _ftpDirectoryService = ftpDirectoryService;
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

        public async Task<ApiResult> Add(GameService model)
        {
            return null;
        }

        public async Task<ApiResult> Delete(int id)
        {
            var gameService = await _dataContext.GameServices.FirstOrDefaultAsync(x => x.Id == id);
            if (gameService != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    _dataContext.GameServices.Remove(gameService);
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
            return new ApiResult() { Message = "Không tìm thấy dịch vụ này." };
        }

        public async Task<ApiResult> GetAll()
        {
            var result = await _dataContext.GameServices.ToListAsync();
            return new(result);
        }

        public async Task<ApiResult> GetById(int id)
        {
            var result = await _dataContext.GameServices.FirstOrDefaultAsync(x => x.Id == id);
            return new(result);
        }

        public async Task<ApiResult> Add(AddGameServiceModel model)
        {
            var existingGameService = await _dataContext.GameServices.FirstOrDefaultAsync(x => x.ServiceName == model.ServiceName);
            if (existingGameService == null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    var gameService = new GameService
                    {
                        Server = model.Server,
                        ServiceName = model.ServiceName,
                        Description = model.Description,
                        Price = model.Price,
                        CreatedDate = _now
                    };

                    _dataContext.GameServices.Add(gameService);
                    await _dataContext.SaveChangesAsync();

                    if (model.Files != null && model.Files.Any())
                    {
                        var fileUploads = await UploadFiles(gameService.Id, model.Files);
                        if (fileUploads.Any())
                        {
                            gameService.Image = string.Join(";", fileUploads); 
                        }
                    }

                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();

                    return new(gameService);
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    throw new Exception(ex.Message);
                }
            }

            return new() { Message = "Danh mục game này đã tồn tại." };
        }

        public async Task<ApiResult> Update(UpdateGameServiceModel model)
        {
            var gameService = await _dataContext.GameServices.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (gameService != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    var existingService = await _dataContext.GameServices
                        .FirstOrDefaultAsync(x => x.ServiceName == model.ServiceName && x.Id != gameService.Id);

                    if (existingService != null)
                    {
                        return new() { Message = "Tên dịch vụ đã tồn tại." };
                    }

                    gameService.Server = model.Server ?? gameService.Server;
                    gameService.ServiceName = model.ServiceName ?? gameService.ServiceName;
                    gameService.Description = model.Description ?? gameService.Description;
                    gameService.Price = model.Price ?? gameService.Price;
                    gameService.UpdatedDate = _now;

                    if (model.Files != null && model.Files.Any())
                    {
                        var fileUploads = await UploadFiles(gameService.Id, model.Files);
                        if (fileUploads.Any())
                        {
                            gameService.Image = string.Join(";", fileUploads); 
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

            return new() { Message = "Dịch vụ game này không tồn tại." };
        }

        public async Task<ApiResult> GetAll2()
        {
            var result = await _dataContext.GameServices
                              .OrderBy(gameService => gameService.DeleteDate.HasValue ? 1 : 0)
                              .ThenBy(gameService => gameService.DeleteDate)
                              .ToListAsync();
            return new(result);
        }

        public Task<ApiResult> Update(GameService model)
        {
            throw new NotImplementedException();
        }
    }
}
