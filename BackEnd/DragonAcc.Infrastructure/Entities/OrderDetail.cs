﻿using DragonAcc.Infrastructure.Base;

namespace DragonAcc.Infrastructure.Entities
{
    /* 
     * Chi tiết đơn hàng
     * API Endpoint Design: add, update, delete, getall, getbyid
     */
    public class OrderDetail : EntityBase
    { 
        public int? OrderId { get; set; }
        public int? GameAccountId { get; set; }
        public int? InGameItemId { get; set; }
        public int? AuctionId { get; set; }
        public int? LuckyWheelId { get; set; }
        public int? Quantity { get; set; }
        public string? UnitPrice { get; set; }
        public string? TotalPrice { get; set; }

    }
}
