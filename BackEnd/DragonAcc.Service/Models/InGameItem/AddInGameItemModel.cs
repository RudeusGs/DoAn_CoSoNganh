namespace DragonAcc.Service.Models.InGameItem
{
    public class AddInGameItemModel
    {
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public string? ItemPrice { get; set; }
        public int? StarQ { get; set; }
        public string? Server { get; set; }
        public int? Quantity { get; set; }
    }
}
