﻿using DragonAcc.Infrastructure.Base;
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
        public InGameItem? _InGameItems { get; set; }
        public GameAccount? _GameAccounts { get; set; }
        public string? StartPrice { get; set; }
        public string? CurrentPrice { get; set; }
        public DateTime? StartDateTime { get; set; }
        public TimeSpan? TimeAuction { get; set; }
        public int? UserId { get; set; }
    }
}