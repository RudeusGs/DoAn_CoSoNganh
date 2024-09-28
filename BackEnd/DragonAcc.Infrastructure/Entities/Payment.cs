using DragonAcc.Infrastructure.Base;

namespace DragonAcc.Infrastructure.Entities
{
    /* 
     * Giao dịch nạp tiền
     * API Endpoint Design: add, delete, update, getall, getbyid
     */
    public class Payment : EntityBase
    {
        public string? Dentistry { get; set; }
        public string? Amount { get; set; }
        public float? Discount { get; set; }

    }
}
