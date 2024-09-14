using DragonAcc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Service.Services
{
    public class ExpiredGiftCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _interval = TimeSpan.FromSeconds(1);

        public ExpiredGiftCleanupService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(_interval, stoppingToken);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

                    var expiredGifts = await dbContext.Gifts
                        .Where(x => x.Expiry.HasValue && x.Expiry.Value <= DateTime.UtcNow)
                        .ToListAsync();

                    if (expiredGifts.Any())
                    {
                        dbContext.Gifts.RemoveRange(expiredGifts);
                        await dbContext.SaveChangesAsync();
                    }
                }
            }
        }
    }

}
