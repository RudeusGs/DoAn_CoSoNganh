using DragonAcc.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;

namespace DragonAcc.Infrastructure
{
    public class DataContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TopRecharger> TopRechargers { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderHistory> OrderHistorys { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<LuckyWheel> LuckyWheels { get; set; }
        public virtual DbSet<InGameItem> InGameItems { get; set; }
        public virtual DbSet<Gift> Gifts { get; set; }
        public virtual DbSet<GameService> GameServices { get; set; }
        public virtual DbSet<GameAccount> GameAccounts { get; set; }
        public virtual DbSet<DailyCheckIn> DailyCheckIns { get; set; }
        public virtual DbSet<Auction> Auctions { get; set; }
        public virtual DbSet<TransactionDetail > TransactionDetails { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }

    }
}
