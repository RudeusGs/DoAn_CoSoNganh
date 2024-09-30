using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonAcc.Service.Models.DailyCheckIn
{
    public class UpdateDailyCheckInModel
    {
        public int? Id { get; set; }
        public DateTime? CheckInDate { get; set; }
        public int? UserId { get; set; }
        public int? Coin { get; set; }
        public int? StreakCount { get; set; }
    }
}
