﻿using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    // Top nạp tiền
    public class TopRecharger : EntityBase
    {
        public int? UserId { get; set; }
        public int? SellerId { get; set; }
        public string? TotalTransaction { get; set; }
    }
}