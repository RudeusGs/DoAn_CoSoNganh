using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    // Đấu giá
    public class Auction : EntityBase
    {
        public int? InGameItemId { get; set; }
        public int? GameAccountId { get; set; }
        public string? StartPrice { get; set; }
        public string? CurrentPrice { get; set; }
        public DateTime? StartDateTime { get; set; }
        public TimeSpan? TimeAuction { get; set; }
        public int? UserId { get; set; }
    }
}
