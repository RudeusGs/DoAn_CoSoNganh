﻿using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    // Lịch sử đơn hàng
    public class OrderHistory : EntityBase
    {
        public int? AuctionId { get; set; }
        public int? GameAccoutId { get; set; }
        public int? GameServiceId { get; set; }
        public int? InGameItemId { get; set; }
        public int? LuckyWheelId { get; set; }
        public int? DepositId { get; set; }
        public int? SellerWithdrawals { get; set; }
    }
}