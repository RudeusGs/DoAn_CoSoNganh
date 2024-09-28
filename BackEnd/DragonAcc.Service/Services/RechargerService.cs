using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.Recharger;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DragonAcc.Service.Services
{
    public class RechargerService : BaseService, IRechargerService
    {
        public RechargerService(DataContext dataContext, IUserService userService) : base(dataContext, userService)
        {
        }
        private bool IsValidRecharger(string? dentistry, string? cardNumber, string? cardSeri)
        {
            if (dentistry == null || cardNumber == null || cardSeri == null)
            {
                return false;
            }

            switch (dentistry.ToLower())
            {
                case "viettel":
                    return cardSeri.Length == 11 && cardNumber.Length == 13;

                case "mobiphone":
                    return cardSeri.Length == 15 && cardNumber.Length == 12;

                case "vinaphone":
                    return cardSeri.Length == 14 && cardNumber.Length == 12;

                default:
                    return false;
            }
        }
        public async Task<ApiResult> Add(AddRechargerModel model)
        {
            if (!IsValidRecharger(model.Dentistry, model.CardNumber, model.CardSeri))
            {
                return new()
                {
                    Message = "Thông tin số thẻ hoặc số seri không hợp lệ. Vui lòng kiểm tra lại."
                };
            }

            var recharger = await _dataContext.Rechargers.FirstOrDefaultAsync(x => x.CardNumber == model.CardNumber || x.CardSeri == model.CardSeri);

            if (recharger == null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    var newRecharger = new Recharger
                    {
                        UserId = _userService.UserId,
                        Amount = model.Amount,
                        CardNumber = model.CardNumber,
                        CardSeri = model.CardSeri,
                        Dentistry = model.Dentistry,
                        Status = "Đang duyệt",
                        CreatedDate = _now
                    };

                    _dataContext.Rechargers.Add(newRecharger);
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new(newRecharger);
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }

            return new()
            {
                Message = "Thẻ cào này đã được sử dụng"
            };
        }

        public Task<ApiResult> Add(Recharger model)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult> Delete(int id)
        {
            var recharger = await _dataContext.Rechargers.FirstOrDefaultAsync(x => x.Id == id);
            if (recharger != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    _dataContext.Rechargers.Remove(recharger);
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
            return new ApiResult() { Message = "Không tìm thấy thông tin này." };
        }

        public async Task<ApiResult> GetAll()
        {
            var result = await _dataContext.Rechargers.ToListAsync();
            return new(result);
        }

        public async Task<ApiResult> GetById(int id)
        {
            var result = await _dataContext.Rechargers.FirstOrDefaultAsync(x => x.Id == id);
            return new(result);
        }

        public async Task<ApiResult> Update(UpdateStatusRechargerModel model)
        {
            var recharger = await _dataContext.Rechargers.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (recharger != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    var existingRecharger = await _dataContext.Rechargers
                        .FirstOrDefaultAsync(x => x.Status == model.Status && x.Id != recharger.Id);

                    if (existingRecharger != null)
                    {
                        return new() { Message = "Trạng thái này đã tồn tại cho một recharge khác." };
                    }
                    recharger.Status = model.Status ?? recharger.Status;
                    recharger.UpdatedDate = _now;

                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();

                    return new() { Message = "Cập nhật trạng thái thành công!" };
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }

            return new() { Message = "Recharger này không tồn tại!" };
        }


        public Task<ApiResult> Update(Recharger model)
        {
            throw new NotImplementedException();
        }
    }
}
