namespace DragonAcc.Service.Models.Payment
{
    public class UpdatePaymentModel
    {
        public int Id { get; set; }
        public string? Dentistry { get; set; }
        public string? Amount { get; set; }
        public float? Discount { get; set; }
    }
}
