using DragonAcc.Infrastructure.Base;

namespace DragonAcc.Infrastructure.Entities
{
    public class DailyCheckIn : EntityBase
    {
        public DateTime? CheckInDate { get; set; }
        public int? UserId { get; set; }
        public int? Coin {  get; set; }
        public int? StreakCount { get; set; }
    }
}
