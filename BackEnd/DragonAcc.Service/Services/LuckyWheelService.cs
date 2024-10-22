using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.LuckyWheel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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

        private async Task<List<string>> UploadFiles(int? luckywheelid, List<IFormFile>? files)
        {
            var uploadedFilePaths = new List<string>();

            if (files == null || !luckywheelid.HasValue)
            {
                return uploadedFilePaths;
            }

            var luckywheelFolder = $"public/LuckyWheels/{luckywheelid}";

            foreach (var file in files)
            {
                var fileExt = Path.GetExtension(file.FileName);
                var stream = file.OpenReadStream();


                var fileName = $"{luckywheelid}.{uploadedFilePaths.Count + 1}{fileExt}";

                var result = await _ftpDirectoryService.TransferToFtpDirectoryAsync(stream, luckywheelFolder, fileName);

                if (result.Succeed)
                {
                    uploadedFilePaths.Add($"{luckywheelFolder}/{fileName}");
                }
            }

            return uploadedFilePaths;
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

                    if (model.Files != null && model.Files.Any())
                    {
                        var fileUploads = await UploadFiles(newLuckyWheel.Id, model.Files);
                        if (fileUploads.Any())
                        {
                            newLuckyWheel.Image = string.Join(";", fileUploads);
                        }
                    }
                    await _dataContext.SaveChangesAsync();
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

                if (model.Files != null && model.Files.Count > 0)
                {
                    var fileUploads = await UploadFiles(luckyWheel.Id, model.Files);
                    if (fileUploads.Count > 0)
                    {
                        luckyWheel.Image = string.Join(",", fileUploads);
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
        public async Task<ApiResult> SpinWheel()
        {
            var luckyWheels = await _dataContext.LuckyWheels.ToListAsync();

            if (luckyWheels == null || luckyWheels.Count == 0)
            {
                return new() { Message = "Không có sản phẩm nào trong vòng quay!" };
            }
            float totalProbability = luckyWheels.Sum(lw => lw.Probability ?? 0);

            if (totalProbability <= 0)
            {
                return new() { Message = "Tổng xác suất không hợp lệ!" };
            }
            Random rand = new Random();
            float randomNumber = (float)(rand.NextDouble() * totalProbability);

            float cumulativeProbability = 0f;
            foreach (var luckyWheel in luckyWheels)
            {
                cumulativeProbability += luckyWheel.Probability ?? 0;
                if (randomNumber <= cumulativeProbability)
                {
                    return new(luckyWheel);
                }
            }

            return new() { Message = "Không chọn được sản phẩm nào!" };
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
