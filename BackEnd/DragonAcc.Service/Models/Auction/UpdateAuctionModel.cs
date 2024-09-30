using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Service.Models.Auction
{
    public class UpdateAuctionModel
    {
        public int? Id { get; set; }
        public string? Prize { get; set; }
        public string? AucionName { get; set; }
        public List<IFormFile>? Files { get; set; }
        public string? Image { get; set; }
        public string? StartPrice { get; set; }
        public DateTime? StartDateTime { get; set; }
        public string? TimeAuction { get; set; }
    }
}
