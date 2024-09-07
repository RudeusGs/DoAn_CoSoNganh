using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure
{
    public interface IDbContextFactory
    {
        public DataContext CreateDataContextInstance();
    }

    public class DbContextFactory : IDbContextFactory
    {
        private readonly DbContextOptions<DataContext> _dbContextOptions;

        public DbContextFactory(IConfiguration configuration)
        {
            _dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseNpgsql(configuration.GetConnectionString("connection"))
                .EnableDetailedErrors()
                .Options;
        }

        public DataContext CreateDataContextInstance()
        {
            return new DataContext(_dbContextOptions);
        }
    }
}

