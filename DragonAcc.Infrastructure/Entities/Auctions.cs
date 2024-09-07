using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    public class Auctions : EntityBase
    {
        public InGameItems? _InGameItems { get; set; }
        public GameAccounts? _GameAccounts { get; set; }
        public string? StartPrice { get; set; }
        public string? CurrentPrice { get; set; }
        public DateTime? StartDateTime { get; set; }
        public TimeSpan? TimeAuction { get; set; }
        public int? UserId { get; set; }
    }
}
