using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.LuckyWheel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DragonAcc.Service.Services
{
    public class LuckyWheelService : BaseService, ILuckyWheelService
    {
        private readonly IFtpDirectoryService _ftpDirectoryService;

        public LuckyWheelService(DataContext dataContext, IUserService userService, IFtpDirectoryService ftpDirectoryService)
            : base(dataContext, userService)
        {
            _ftpDirectoryService = ftpDirectoryService;
        }

        private async Task<string?> UploadFile(int? luckyWheelId, IFormFile? file)
        {
            if (file == null || !luckyWheelId.HasValue)
            {
                return string.Empty;
            }
            var fileExt = Path.GetExtension(file.FileName);
            var stream = file.OpenReadStream();
            var result = await _ftpDirectoryService.TransferToFtpDirectoryAsync(stream, $"public/LuckyWheels", $"{luckyWheelId}{fileExt}");
            if (result.Succeed)
            {
                return $"LuckyWheels/{luckyWheelId}{fileExt}";
            }
            return string.Empty;
        }

        public async Task<ApiResult> Add(AddLuckyWheelModel model)
        {
            var luckyWheel = await _dataContext.LuckyWheels.FirstOrDefaultAsync(x => x.Prize == model.Prize);

            if (luckyWheel == null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    var newLuckyWheel = new LuckyWheel
                    {
                        Prize = model.Prize,
                        Probability = model.Probability,
                        CreatedDate = DateTime.Now,
                    };

                    _dataContext.LuckyWheels.Add(newLuckyWheel);
                    await _dataContext.SaveChangesAsync();

                    if (model.File != null)
                    {
                        var fileUpload = await UploadFile(newLuckyWheel.Id, model.File);
                        if (!string.IsNullOrEmpty(fileUpload))
                        {
                            newLuckyWheel.Image = fileUpload;
                            await _dataContext.SaveChangesAsync();
                        }
                    }

                    await tran.CommitAsync();
                    return new(newLuckyWheel);
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }

            return new() { Message = "Món đồ này đã tồn tại!" };
        }

        public async Task<ApiResult> Update(UpdateLuckyWheelModel model)
        {
            var luckyWheel = await _dataContext.LuckyWheels.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (luckyWheel == null)
            {
                return new() { Message = "Không tìm thấy món đồ này!" };
            }

            using var tran = await _dataContext.Database.BeginTransactionAsync();
            try
            {
                luckyWheel.Prize = model.Prize ?? luckyWheel.Prize;
                luckyWheel.Probability = model.Probability ?? luckyWheel.Probability;

                if (model.File != null)
                {
                    var fileUpload = await UploadFile(luckyWheel.Id, model.File);
                    if (!string.IsNullOrEmpty(fileUpload))
                    {
                        luckyWheel.Image = fileUpload;
                    }
                }

                luckyWheel.UpdatedDate = DateTime.Now;

                await _dataContext.SaveChangesAsync();
                await tran.CommitAsync();

                return new(luckyWheel);
            }
            catch (Exception e)
            {
                await tran.RollbackAsync();
                throw new Exception(e.Message);
            }
        }


        public async Task<ApiResult> Delete(int id)
        {
            var luckyWheel = await _dataContext.LuckyWheels.FirstOrDefaultAsync(x => x.Id == id);
            if (luckyWheel != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    _dataContext.LuckyWheels.Remove(luckyWheel);
                    await _dataContext.SaveChangesAsync();
                    await tran.CommitAsync();
                    return new() { Message = "Xóa thành công!" };
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    throw new Exception(e.Message);
                }
            }
            return new() { Message = "Món đồ này không tồn tại!" };
        }

        public async Task<ApiResult> GetAll()
        {
            var result = await _dataContext.LuckyWheels.ToListAsync();
            return new(result);
        }

        public async Task<ApiResult> GetById(int id)
        {
            var result = await _dataContext.LuckyWheels.FirstOrDefaultAsync(x => x.Id == id);
            return new(result);
        }

        public Task<ApiResult> Add(LuckyWheel model)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult> Update(LuckyWheel model)
        {
            throw new NotImplementedException();
        }
    }
}
