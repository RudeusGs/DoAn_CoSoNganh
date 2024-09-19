using DragonAcc.Infrastructure.Base;

namespace DragonAcc.Infrastructure.Entities
{
    // Đấu giá
    public class Auction : EntityBase
    {
        public int? InGameItemId { get; set; }
        public int? GameAccountId { get; set; }
        public string? AuctionName {  get; set; }
        public string? StartPrice { get; set; }
        public string? CurrentPrice { get; set; }
        public DateTime? StartDateTime { get; set; }
        public string? TimeAuction { get; set; }
        public int? UserId { get; set; }
    }
}
