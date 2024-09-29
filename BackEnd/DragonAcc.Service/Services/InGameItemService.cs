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
        private async Task<string?> UploadFile(int? inGameItemId, IFormFile? file)
        {
            if (file == null || !inGameItemId.HasValue)
            {
                return string.Empty;
            }
            var fileExt = Path.GetExtension(file.FileName);
            var stream = file.OpenReadStream();
            var result = await _ftpDirectoryService.TransferToFtpDirectoryAsync(stream, $"public/InGameItem", $"{inGameItemId}{fileExt}");
            if (result.Succeed)
            {
                return $"InGameItem/{inGameItemId}{fileExt}";
            }
            return string.Empty;
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

                    if (model.File != null)
                    {
                        var fileUpload = await UploadFile(InGameItem.Id, model.File);
                        if (!string.IsNullOrEmpty(fileUpload))
                        {
                            InGameItem.Image = fileUpload;
                            await _dataContext.SaveChangesAsync();
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
                    if (model.File != null)
                    {
                        var fileUpload = await UploadFile(inGameItem.Id, model.File);
                        if (!string.IsNullOrEmpty(fileUpload))
                        {
                            inGameItem.Image = fileUpload;
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
