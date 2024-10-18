using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.DailyCheckIn;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Service.Services
{
    public class DailyCheckInService : BaseService, IDailyCheckInService
    {
        public DailyCheckInService(DataContext dataContext, IUserService userService) : base(dataContext, userService)
        {
        }

        public async Task<ApiResult> Add(AddDailyCheckInModel model)
        {
            var result = await _dataContext.DailyCheckIns.FirstOrDefaultAsync(x => x.CheckInDate == model.CheckInDate);
            if (result == null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    var addCheckIn = new DailyCheckIn()
                    {
                        CheckInDate = model.CheckInDate,
                        Coin = model.Coin,
                        CreatedDate = _now
                    };
                    _dataContext.DailyCheckIns.Add(addCheckIn);
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new(addCheckIn);
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    throw new Exception(ex.Message);
                }
            }
            return new() { Message = "Món đồ này đã có" };
        }

        public async Task<ApiResult> Add2(CheckInModel model)
        {
            // Tìm thông tin người dùng
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == model.UserId);

            if (user != null)
            {
                // Tìm bản ghi điểm danh gần nhất (nếu có)
                var lastCheckIn = await _dataContext.DailyCheckIns
                                      .Where(dc => dc.UserId == user.Id)
                                      .OrderByDescending(dc => dc.CheckInDate)
                                      .FirstOrDefaultAsync();

                using var trans = _dataContext.Database.BeginTransaction();
                try
                {
                    // Ngày hiện tại
                    var currentDate = _now;

                    // Kiểm tra nếu chưa từng điểm danh
                    if (lastCheckIn == null)
                    {
                        // Người dùng chưa từng điểm danh => Tạo mới bản ghi
                        var newCheckIn = new DailyCheckIn
                        {
                            UserId = _userService.UserId,
                            Coin = model.Coin,
                            CheckInDate = currentDate,
                            StreakCount = 1 // Bắt đầu streak mới
                        };

                        _dataContext.DailyCheckIns.Add(newCheckIn);
                    }
                    else
                    {
                        // Người dùng đã từng điểm danh => Tính toán streak
                        var currentWeek = ISOWeek.GetWeekOfYear(currentDate);
                        var lastCheckInWeek = ISOWeek.GetWeekOfYear((DateTime)lastCheckIn.CheckInDate);

                        if (currentWeek == lastCheckInWeek)
                        {
                            // Cùng tuần => Tăng StreakCount
                            lastCheckIn.StreakCount += 1;
                            lastCheckIn.Coin += 1 + model.Coin;
                        }
                        else
                        {
                            // Khác tuần => Reset StreakCount
                            lastCheckIn.StreakCount = 1;
                            lastCheckIn.Coin += model.Coin;
                        }

                        // Cập nhật ngày điểm danh
                        lastCheckIn.CheckInDate = currentDate;

                        // Cập nhật thông tin điểm danh
                        _dataContext.DailyCheckIns.Update(lastCheckIn);
                    }

                    // Lưu thay đổi vào cơ sở dữ liệu
                    await _dataContext.SaveChangesAsync();

                    // Commit transaction
                    await trans.CommitAsync();

                    return new ApiResult { Message = "Checkin thành công" };
                }
                catch (Exception ex)
                {
                    // Rollback transaction nếu có lỗi xảy ra
                    await trans.RollbackAsync();
                    return new ApiResult {  Message = ex.Message };
                }
            }

            return new ApiResult { Message = "Không tìm thấy thông tin người dùng." };
        }



        public async Task<ApiResult> Delete(int id)
        {
            var checkIn = await _dataContext.DailyCheckIns.FirstOrDefaultAsync(x => x.Id == id);
            if (checkIn != null)
            {
                var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    checkIn.DeleteDate = _now;
                    _dataContext.DailyCheckIns.Remove(checkIn);
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
            return new ApiResult() { Message = "Không tìm thấy phần điểm danh này!" };
        }

        public async Task<ApiResult> GetAll()
        {
            var result = await _dataContext.DailyCheckIns.ToListAsync();
            return new(result);
        }

        public async Task<ApiResult> GetById(int id)
        {
            var result = await _dataContext.DailyCheckIns.FirstOrDefaultAsync(x => x.Id == id);
            return new(result);
        }

        public async Task<ApiResult> Update(UpdateDailyCheckInModel model)
        {
            var dailyCheckIn = await _dataContext.DailyCheckIns.FirstOrDefaultAsync(x => x.Id == model.Id || 
                                                                                        x.CheckInDate == model.CheckInDate);
            if(dailyCheckIn != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    dailyCheckIn.CheckInDate = model.CheckInDate ?? dailyCheckIn.CheckInDate;
                    dailyCheckIn.Coin = model.Coin ?? dailyCheckIn.Coin;
                    dailyCheckIn.UserId = model.UserId ?? dailyCheckIn.UserId;
                    dailyCheckIn.StreakCount = model.StreakCount ?? dailyCheckIn.StreakCount;
                    dailyCheckIn.UpdatedDate = _now;
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();

                    return new() { Message = "Cập nhật thành công!" };
                }
                catch(Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }
            return new() { Message = "Cập Không nhật thành công!" };
        }

        public Task<ApiResult> Update(DailyCheckIn model)
        {
            throw new NotImplementedException();
        }
        public Task<ApiResult> Add(DailyCheckIn model)
        {
            throw new NotImplementedException();
        }
    }
}
