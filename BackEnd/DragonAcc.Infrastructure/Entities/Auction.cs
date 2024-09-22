using DragonAcc.Infrastructure.Base;

namespace DragonAcc.Infrastructure.Entities
{
    /*      
     * Admin: tạo phiên đấu giá, sửa phiên đấu giá, và xóa phiên đấu giá
     * User: tham gia đấu giá
     * API Endpoint Design: add, update, delete, getall
     */
    public class Auction : EntityBase
    {
        public string? Prize { get; set; }
        public string? AuctionName {  get; set; }
        public string? StartPrice { get; set; }
        public string? CurrentPrice { get; set; }
        public DateTime? StartDateTime { get; set; }
        public string? Image { get; set; }
        public string? TimeAuction { get; set; }
        public string? Status { get; set; }
        public int? Winner { get; set; }
    }
}
