using DragonAcc.Infrastructure.Base;

namespace DragonAcc.Infrastructure.Entities
{
    public class Order : EntityBase
    {
        public int? UserId { get; set; }
        public string? TotalAmount { get; set; }
        public bool? Status { get; set; }

    }
}
