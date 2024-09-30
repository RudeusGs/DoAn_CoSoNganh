using DragonAcc.Infrastructure;
using DragonAcc.Infrastructure.Entities;
using DragonAcc.Service.Common.IServices;
using DragonAcc.Service.Interfaces;
using DragonAcc.Service.Models;
using DragonAcc.Service.Models.InGameItem;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace DragonAcc.Service.Services
{
    public class InGameItemService : BaseService, IIngameItemService
    {
        private readonly IFtpDirectoryService _ftpDirectoryService;

        public InGameItemService(DataContext dataContext, IUserService userService, IFtpDirectoryService ftpDirectoryService)
            : base(dataContext, userService)
        {
            _ftpDirectoryService = ftpDirectoryService;
        }
        private async Task<List<string>> UploadFiles(int? ingameitemId, IList<IFormFile>? files)
        {
            var uploadedFilePaths = new List<string>();

            if (files == null || !ingameitemId.HasValue)
            {
                return uploadedFilePaths;
            }

            var ingameitemFolder = $"public/InGameItems/{ingameitemId}";

            foreach (var file in files)
            {
                var fileExt = Path.GetExtension(file.FileName);
                var stream = file.OpenReadStream();
                var fileName = $"{Guid.NewGuid()}{fileExt}";
                var result = await _ftpDirectoryService.TransferToFtpDirectoryAsync(stream, ingameitemFolder, fileName);

                if (result.Succeed)
                {
                    uploadedFilePaths.Add($"{ingameitemFolder}/{fileName}");
                }
            }

            return uploadedFilePaths;
        }
        public async Task<ApiResult> GetAll()
        {
            var result = await _dataContext.InGameItems.ToListAsync() ;
            return new(result);
        }

        public async Task<ApiResult> GetById(int id)
        {
            var result = await _dataContext.InGameItems.FirstOrDefaultAsync(x => x.Id == id);
            return new(result);
        }

        public async Task<ApiResult> Add(AddInGameItemModel model)
        {
            var result = await _dataContext.InGameItems.FirstOrDefaultAsync(x => x.ItemName == model.ItemName);
            if (result == null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    var InGameItem = new InGameItem()
                    {
                        Server = model.Server,
                        ItemName = model.ItemName,
                        ItemDescription = model.ItemDescription,
                        ItemPrice = model.ItemPrice,
                        StarQ = model.StarQ,
                        Quantity = model.Quantity,
                        CreatedDate = _now

                    };
                    _dataContext.InGameItems.Add(InGameItem);
                    await _dataContext.SaveChangesAsync();

                    if (model.Files != null && model.Files.Count > 0)
                    {
                        var fileUploads = await UploadFiles(InGameItem.Id, model.Files);
                        if (fileUploads.Count > 0)
                        {
                            InGameItem.Image = string.Join(",", fileUploads);
                        }
                    }

                    await tran.CommitAsync();
                    return new(InGameItem);
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    throw new Exception(ex.Message);
                }
            }
            return new() { Message = "Món đồ này đã có" };
        }

        public async Task<ApiResult> Update(UpdateInGameItemModel model)
        {
            var inGameItem = await _dataContext.InGameItems.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (inGameItem != null)
            {
                using var tran = await _dataContext.Database.BeginTransactionAsync();
                try
                {
                    var existingService = await _dataContext.InGameItems
                        .FirstOrDefaultAsync(x => x.ItemName == model.ItemName && x.Id != inGameItem.Id);

                    if (existingService != null)
                    {
                        return new() { Message = "Tên đồ này đã tồn tại" };
                    }
                    inGameItem.ItemName = model.ItemName ?? inGameItem.ItemName;
                    inGameItem.Server = model.Server ?? inGameItem.Server;
                    inGameItem.StarQ = model.StarQ ?? inGameItem.StarQ;
                    inGameItem.Quantity = model.StarQ ?? inGameItem.StarQ;
                    inGameItem.ItemPrice = model.ItemPrice ?? inGameItem.ItemPrice;
                    inGameItem.ItemDescription = model.ItemDescription ?? inGameItem.ItemDescription;
                    inGameItem.UpdatedDate = _now;
                    if (model.Files != null && model.Files.Count > 0)
                    {
                        var fileUploads = await UploadFiles(inGameItem.Id, model.Files);
                        if (fileUploads.Count > 0)
                        {
                            inGameItem.Image = string.Join(",", fileUploads);
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
            return new() { Message = "Món đồ này không tồn tại!" };
        }
        public async Task<ApiResult> Delete(int id)
        {
            var ingameitem = await _dataContext.InGameItems.FirstOrDefaultAsync(x => x.Id == id);
            if (ingameitem != null)
            {
                var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    ingameitem.DeleteDate = _now;
                    _dataContext.InGameItems.Remove(ingameitem);
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
            return new ApiResult() { Message = "Không tìm thấy món đồ này!" };
        }

        public async Task<ApiResult> Remove(int id)
        {
            var ingameitem = _dataContext.InGameItems.FirstOrDefault(x => x.Id == id);
            if (ingameitem != null)
            {
                using var tran = _dataContext.Database.BeginTransaction();
                try
                {
                    ingameitem.DeleteDate = _now;
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
            return new ApiResult() { Message = "Món đồ này không tồn tại" };
        }

        public Task<ApiResult> Add(InGameItem model)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult> Update(InGameItem model)
        {
            throw new NotImplementedException();
        }
    }
}
