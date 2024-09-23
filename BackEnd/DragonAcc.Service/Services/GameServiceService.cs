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

        public GameServiceService(DataContext dataContext,Common.IServices.IUserService userService) : base(dataContext, userService)
        {
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
                    gameacservice.DeleteDate = _now;
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
            var result = await _dataContext.GameServices.FirstOrDefaultAsync(x => x.ServiceName == model.ServiceName);
            if(result == null)
            {
                using var tran = _dataContext.Database.BeginTransaction ();
                try
                {
                    var gameService = new GameService()
                    {
                        Server = model.Server,
                        ServiceName = model.ServiceName,
                        Description = model.Description,
                        Price = model.Price,
                        CreatedDate = _now
                    };
                    _dataContext.GameServices.Add(gameService);
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
            return new() { Message = "Danh mục game này đã có" };
        } 
        public async Task<ApiResult> Update(UpdateGameServiceModel model)
        {
            var gameService = await _dataContext.GameServices
                               .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (gameService != null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    if (model.IsDelete == true)
                    {
                        gameService.DeleteDate = _now;
                        await _dataContext.SaveChangesAsync();
                        await tran.CommitAsync();
                        return new();
                    }
                    if (string.IsNullOrEmpty(model.Description)
                        && string.IsNullOrEmpty(model.Price))
                    {
                        return new() { Message = "Không có thông tin nào được cập nhật" };
                    }
                    var existingService = await _dataContext.GameServices
                        .FirstOrDefaultAsync(x => x.ServiceName == model.ServiceName && x.Id != gameService.Id);

                    if (existingService != null)
                    {
                        return new() { Message = "Tên dịch vụ đã tồn tại" };
                    }

                    gameService.Server = model.Server ?? gameService.Server;
                    gameService.Description = model.Description ?? gameService.Description;
                    gameService.Price = model.Price ?? gameService.Price;
                    gameService.UpdatedDate = _now;

                    if (model.IsDelete == false)
                    {
                        gameService.DeleteDate = null;
                    }

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

            return new() { Message = "Dịch vụ game này không tồn tại!" };
        }

    }
}
