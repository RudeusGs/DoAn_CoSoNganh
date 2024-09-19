using DragonAcc.Infrastructure.Base;
using DragonAcc.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DragonAcc.Infrastructure.Extensions
{
    public static class DataExtension
    {
        /// <summary>
        /// Exists the specified data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static IQueryable<T> Exist<T>(this DbSet<T> data) where T : EntityBase
        {
            return data.Where(x => !x.DeleteDate.HasValue);
        }

        /// <summary>
        /// Nots the exist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static IQueryable<T> NotExist<T>(this DbSet<T> data) where T : EntityBase
        {
            return data.Where(x => x.DeleteDate.HasValue);
        }

        /// <summary>
        /// Exists the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static IQueryable<User> Exist(this DbSet<User> data)
        {
            return data.Where(x => !x.DeleteDate.HasValue);
        }
    }
}
