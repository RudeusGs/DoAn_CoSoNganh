using DragonAcc.Service.Models;

namespace DragonAcc.Service.Common.IServices
{
    public interface IServiceBase<TEntity>
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        Task<ApiResult> GetAll();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        Task<ApiResult> GetById(int id);

        /// <summary>
        /// Adds the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        Task<ApiResult> Add(TEntity model);

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        Task<ApiResult> Update(TEntity model);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        Task<ApiResult> Delete(int id);
    }
}
