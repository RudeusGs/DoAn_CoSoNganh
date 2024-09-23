using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Common.Services;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.LuckyWheel;
using Microsoft.EntityFrameworkCore;

namespace DragonAcc.Service.Services
{
    public class LuckyWheelService : BaseService, ILuckyWheelService
    {
        public LuckyWheelService(DataContext dataContext, IUserService userService) : base(dataContext, userService)
        {
        }

        public async Task<ApiResult> Add(AddLuckyWheelModel model)
        {
            var luckyWheel = await _dataContext.LuckyWheels.FirstOrDefaultAsync(x => x.Prize == model.Prize);

            if (luckyWheel == null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    var newluckyWheel = new LuckyWheel
                    {
                        Prize = model.Prize,
                        Probability = model.Probability,
                        CreatedDate = DateTime.Now,
                    };
                    _dataContext.LuckyWheels.Add(newluckyWheel);
                    await _dataContext.SaveChangesAsync();                  
                    await tran.CommitAsync();
                    return new(newluckyWheel);
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }

            return new()
            {
                Message = "Món đồ này đã tồn tại!"
            };
        }    

        public async Task<ApiResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult> GetAll()
        {
            throw new NotImplementedException();
        }
        public async Task<ApiResult> Update(UpdateLuckyWheelModel model)
        {
            throw new NotImplementedException();
        }
        public async Task<ApiResult> GetById(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<ApiResult> Add(LuckyWheel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult> Update(LuckyWheel model)
        {
            throw new NotImplementedException();
        }
    }
}
