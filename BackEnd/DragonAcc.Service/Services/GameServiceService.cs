using DragonAcc.Infrastructure.Entities;
using DragonAcc.Infrastructure;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models.AccountGame;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.GameService;
using DragonAcc.Service.Common.IServices;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net.NetworkInformation;
using System.Numerics;
namespace DragonAcc.Service.Services
{
    public class GameServiceService : BaseService, IGameServiceService
    {

        private readonly IFtpDirectoryService _ftpDirectoryService;

        public GameServiceService(DataContext dataContext, IUserService userService, IFtpDirectoryService ftpDirectoryService) : base(dataContext, userService)
        {
            _ftpDirectoryService = ftpDirectoryService;
        }

        private async Task<string?> UploadFile(int? gameServiceId, IFormFile? file)
        {
            if (file == null || !gameServiceId.HasValue)
            {
                return string.Empty;
            }
            var fileExt = Path.GetExtension(file.FileName);
            var stream = file.OpenReadStream();
            var result = await _ftpDirectoryService.TransferToFtpDirectoryAsync(stream, $"public/GameServices", $"{gameServiceId}{fileExt}");
            if (result.Succeed)
            {
                return $"GameServices/{gameServiceId}{fileExt}";
            }
            return string.Empty;
        }
        public async Task<ApiResult> Add(GameService model)
        {
            return null;
        }

        public async Task<ApiResult> Delete(int id)
        {
            var gameacservice = await _dataContext.GameServices.FirstOrDefaultAsync(x => x.Id == id);
            if (gameacservice != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    _dataContext.GameServices.Remove(gameacservice);
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
            return new ApiResult() { Message = "Không tìm thấy danh mục này." };
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

        public async Task<ApiResult> Update(GameService model)
        {
            return null;
        }

        public async Task<ApiResult> GetAll2()
        {
            var result = await _dataContext.GameServices
                              .OrderBy(gameservice => gameservice.DeleteDate.HasValue ? 1 : 0)
                              .ThenBy(gameservice => gameservice.DeleteDate)
                              .ToListAsync();
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
                    if (model.File != null)
                    {
                        var fileUpload = await UploadFile(gameService.Id, model.File);
                        if (!string.IsNullOrEmpty(fileUpload))
                        {
                            gameService.Image = fileUpload;
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
            return new() { Message = "Danh mục game này đã tồn tại" };
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
                        return new() { Message = "Tên dịch vụ đã tồn tại" };
                    }
                    gameService.Server = model.Server ?? gameService.Server;
                    gameService.ServiceName = model.ServiceName ?? gameService.ServiceName;
                    gameService.Description = model.Description ?? gameService.Description;
                    gameService.Price = model.Price ?? gameService.Price;
                    gameService.UpdatedDate = _now;
                    if (model.File != null)
                    {
                        var fileUpload = await UploadFile(gameService.Id, model.File);
                        if (!string.IsNullOrEmpty(fileUpload))
                        {
                            gameService.Image = fileUpload;
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
            return new() { Message = "Dịch vụ game này không tồn tại!" };
        }


    }
}
