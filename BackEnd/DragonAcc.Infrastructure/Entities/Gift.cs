using DragonAcc.Infrastructure.Base;

namespace DragonAcc.Infrastructure.Entities
{
    public class Gift : EntityBase
    {
        public string? GiftCode { get; set; }
        public float? Percent { get; set; }
        public DateTime? Expiry { get; set; }
    }
}
