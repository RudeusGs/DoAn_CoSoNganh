using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    public class DailyCheckIns : EntityBase
    {
        public DateTime CheckInDate { get; set; }
        public int? UserId { get; set; }
        public int? SellerId { get; set; }
        public string? Reward {  get; set; }
        public int? StreakCount { get; set; }
    }
}
