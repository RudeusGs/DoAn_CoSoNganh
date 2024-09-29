using DragonAcc.Service.Models.OrderDetail;
namespace DragonAcc.Service.Models.Order
{
    public class OrderModel
    {
        public int? UserId { get; set; }
        public string? TotalAmount { get; set; }
        public bool? Status { get; set; }
        public List<OrderDetailModel>? OrderDetails { get; set; }
    }
}
