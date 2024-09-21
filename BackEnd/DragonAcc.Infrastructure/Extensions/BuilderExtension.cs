using DragonAcc.Infrastructure.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DragonAcc.Infrastructure.Extensions
{
    public static class BuilderExtension
    {
        public static void AddDummyData(this ModelBuilder modelBuilder)
        {
            var roles = new List<IdentityRole<int>>()
            {
                new IdentityRole<int>() { Id = 1, Name = RoleConstants.ADMIN, ConcurrencyStamp = "1", NormalizedName = RoleConstants.ADMIN.ToUpper() },
                new IdentityRole<int>() { Id = 2, Name = RoleConstants.USER, ConcurrencyStamp = "2", NormalizedName = RoleConstants.USER.ToUpper() },        
            };

            modelBuilder.Entity<IdentityRole<int>>()
                .HasData(roles);
        }
    }
}
