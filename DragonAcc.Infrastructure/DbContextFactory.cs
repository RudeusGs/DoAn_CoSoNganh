using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DragonAcc.Infrastructure
{
    public interface IDbContextFactory
    {
        DataContext CreateDataContextInstance();
    }

    public class DbContextFactory : IDbContextFactory
    {
        private readonly DbContextOptions<DataContext> _dbContextOptions;

        public DbContextFactory(IConfiguration configuration)
        {
            _dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer(configuration.GetConnectionString("connection"))
                .EnableDetailedErrors()
                .Options;
        }

        public DataContext CreateDataContextInstance()
        {
            return new DataContext(_dbContextOptions);
        }
    }
}
