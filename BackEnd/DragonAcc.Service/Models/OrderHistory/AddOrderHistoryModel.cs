using DragonAcc.Service.Models.OrderDetail;

namespace DragonAcc.Service.Models.OrderHistory
{
    public class AddOrderHistoryModel
    {
        public int? UserId { get; set; }
        public List<OrderDetailModel>? OrderDetails { get; set; }
    }
}
