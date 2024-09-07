using DragonAcc.Infrastructure.Base;

namespace DragonAcc.Infrastructure.Entities
{
    public class GameAccounts : EntityBase
    {
        public string? AccountName { get; set; }
        public string? AccountPassword { get; set; }
        public int? SellerId { get; set; }
        public int? UserId { get; set; }
        public string? Price { get; set; }
        public string? Status { get; set; }
        public bool? Earring { get; set; }
        public string? Planet {  get; set; }
        public int? Server {  get; set; }


    }
}
