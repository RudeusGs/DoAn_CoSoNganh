using DragonAcc.Infrastructure.Base;

namespace DragonAcc.Infrastructure.Entities
{
    /*  Admin: tạo ra bảng điểm danh
     *  User sẽ điểm danh để nhận phần thưởng hằng ngày
     *  API Endpoint Design: add, update, delete, getall, getbyid
     */
    public class DailyCheckIn : EntityBase
    {
        public DateTime? CheckInDate { get; set; }
        public int? UserId { get; set; }
        public int? Coin {  get; set; }
        public int? StreakCount { get; set; }
    }
}
