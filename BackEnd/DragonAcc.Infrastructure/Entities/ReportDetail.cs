using DragonAcc.Infrastructure.Base;

namespace DragonAcc.Infrastructure.Entities
{
    public class ReportDetail : EntityBase
    {
        public int? PurchasedAccountId { get; set; }
        public string? Problem { get; set; }
        public string? Status { get; set; }
        public int? UserId { get; set; }
        public string? SolveTheProblem { get; set; }
    }
}
