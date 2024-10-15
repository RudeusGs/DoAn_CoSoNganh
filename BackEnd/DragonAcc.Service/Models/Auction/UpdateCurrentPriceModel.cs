namespace DragonAcc.Service.Models.Auction
{
    public class UpdateCurrentPriceModel
    {
        public int? Id { get; set; }
        public string? CurrentPrice { get; set; }
        public int? Winner { get; set; }
    }
}