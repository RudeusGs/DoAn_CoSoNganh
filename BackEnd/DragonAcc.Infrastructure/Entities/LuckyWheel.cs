using DragonAcc.Infrastructure.Base;

namespace DragonAcc.Infrastructure.Entities
{
    public class LuckyWheel : EntityBase
    {
        public string? Prize { get; set; }
        public float? Probability { get; set; }
        public string? Image { get; set; }
        public int? UserId { get; set; }
    }
}
