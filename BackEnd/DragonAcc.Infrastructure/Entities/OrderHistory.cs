using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    /* 
     * Lịch sử giao dịch
     * API Endpoint Design: add, update, delete, getall, getbyid
     */
    public class OrderHistory : EntityBase
    {
        public int? AuctionId { get; set; }
        public int? GameAccoutId { get; set; }
        public int? GameServiceId { get; set; }
        public int? InGameItemId { get; set; }
        public int? LuckyWheelId { get; set; }
        public int? SellerWithdrawalId { get; set; }
        public int? UserId { get; set; }
    }
}
