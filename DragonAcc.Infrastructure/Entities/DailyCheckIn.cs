using DragonAcc.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Infrastructure.Entities
{
    // Điểm danh hằng ngày
    public class DailyCheckIn : EntityBase
    {
        public DateTime CheckInDate { get; set; }
        public int? UserId { get; set; }
        public int? SellerId { get; set; }
        public string? Reward {  get; set; }
        public int? StreakCount { get; set; }
    }
}
