using DragonAcc.Infrastructure.Base;

namespace DragonAcc.Infrastructure.Entities
{
    public class PurchasedAccount : EntityBase
    {
        public string? AccountName { get; set; }
        public string? AccountPassword { get; set; }
        public int? UserId { get; set; }
        public string? Report {  get; set; }
    }
}
