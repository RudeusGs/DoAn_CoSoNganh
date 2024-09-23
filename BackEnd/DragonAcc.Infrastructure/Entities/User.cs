using Microsoft.AspNetCore.Identity;

namespace DragonAcc.Infrastructure.Entities
{
    public class User : IdentityUser<int>
    {
        public string? FullName { get; set; }
        public string? Balance { get; set; }
        public int? Coin { get; set; }
        public string? Image { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }
        public virtual DateTime? DeleteDate { get; set; }

    }
}
