using DragonAcc.Infrastructure.Base;

namespace DragonAcc.Infrastructure.Entities
{
    public class Recharger : EntityBase
    {
        public string? Dentistry { get; set; }
        public string? Amount { get; set; }
        public string? CardNumber { get; set; }
        public string? CardSeri { get; set; }
        public string? Status { get; set; }
        public int? UserId { get; set; }
        public string? TopRecharge { get; set; }
    }
}
