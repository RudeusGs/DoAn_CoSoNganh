using DragonAcc.Infrastructure.Base;

namespace DragonAcc.Infrastructure.Entities
{
    public class WithDrawMoney : EntityBase
    {
        public int? UserId { get; set; }
        public string? Amount { get; set; }
        public string? Status { get; set; }
        public string? BankNumber { get; set; }
        public string? BankName { get; set; }

    }
}
