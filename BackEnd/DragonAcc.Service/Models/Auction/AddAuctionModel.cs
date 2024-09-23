using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Service.Models.Auction
{
    public class AddAuctionModel
    {
        public string? AuctionName { get; set; }
        public string? Prize { get; set; }
        public IFormFile? File { get; set; }
        public string? Image { get; set; }
        public string? StartPrice { get; set; }
        public string? CurrentPrice { get; set; }
        public DateTime? StartDateTime { get; set; }
        public string? TimeAuction { get; set; }
        public string? Status { get; set; }
        public int? Winner { get; set; }
    }
}
