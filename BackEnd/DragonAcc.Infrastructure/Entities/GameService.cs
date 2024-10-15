using DragonAcc.Infrastructure.Base;
namespace DragonAcc.Infrastructure.Entities
{
    public class GameService : EntityBase
    {
        public int? Server { get; set; }
        public string? ServiceName { get; set; }
        public string? Image {  get; set; }
        public string? Description { get; set; }
        public string? Price { get; set; }
    }
}
