﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Service.Models.Auction
{
    public class UpdateAuctionModel
    {
        public int? InGameItemId { get; set; }
        public int? GameAccountId { get; set; }
        public string? AucionName { get; set; }
        public string? StartPrice { get; set; }
        public DateTime? StartDateTime { get; set; }
        public string? TimeAuction { get; set; }
    }
}