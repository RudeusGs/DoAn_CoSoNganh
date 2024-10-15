using DragonAcc.Infrastructure.Base;

namespace DragonAcc.Infrastructure.Entities
{
    public class InGameItem : EntityBase
    {
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public string? ItemPrice { get; set; }
        public string? Image { get; set; }
        public int? StarQ { get; set; }
        public string? Server {  get; set; }
        public int? Quantity { get; set; }
    }
}
