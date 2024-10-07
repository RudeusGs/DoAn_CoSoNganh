using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Service.Services
{
    public class NotificationService : BaseService, INotificationService
    {
        public NotificationService(DataContext dataContext, IUserService userService) : base(dataContext, userService)
        {
        }

        public async Task<ApiResult> Add(AddNotificationModel model)
        {
            if (model.SenderId == null || model.ReceiverId == null)
            {
                return new ApiResult() { Message = "Người gửi và người nhận không được để trống." };
            }

            var notification = new Notification()
            {
                SenderId = _userService.UserId,
                ReceiverId = model.ReceiverId,
                Title = model.Title,
                Message = model.Message,
                CreatedDate = DateTime.Now,
            };

            using var tran = _dataContext.Database.BeginTransaction();
            try
            {
                _dataContext.Notifications.Add(notification);
                await _dataContext.SaveChangesAsync();
                await tran.CommitAsync();
                return new ApiResult(notification);
            }
            catch (Exception e)
            {
                await tran.RollbackAsync();
                throw new Exception(e.Message);
            }
        }

        public async Task<ApiResult> Delete(int id)
        {
            var notificate = await _dataContext.Notifications.FirstOrDefaultAsync(x => x.Id == id);
            if (notificate != null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    notificate.DeleteDate = _now;
                    _dataContext.Notifications.Remove(notificate);
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

            return new ApiResult() { Message = "Notification này không tồn tại" };
        }

        public async Task<ApiResult> GetAll()
        {
            var result = await _dataContext.Notifications.ToListAsync();
            return new(result);
        }

        public async Task<ApiResult> GetById(int id)
        {
            var result = await _dataContext.Notifications.FirstOrDefaultAsync(x => x.Id == id);
            return new(result);
        }

        public async Task<ApiResult> Update(UpdateNotificationModel model)
        {
            var notificate = await _dataContext.Notifications.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (notificate != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    notificate.Title = model.Title ?? notificate.Title;
                    notificate.Message = model.Message ?? notificate.Message;
                    notificate.UpdatedDate = _now;
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
            return new() { Message = "Notification này không tồn tại!" };
        }

        public Task<ApiResult> Update(Notification model)
        {
            throw new NotImplementedException();
        }
        public Task<ApiResult> Add(Notification model)
        {
            throw new NotImplementedException();
        }
    }
}
