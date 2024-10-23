using DragonAcc.Infrastructure.Base;

namespace DragonAcc.Infrastructure.Entities
{
    public class CommentOrLike : EntityBase
    {
        public int? UserId { get; set; }
        public int? GameAccountId { get; set; }
        public string Comment {  get; set; }
        public bool Like { get; set; }
        public int CLike { get; set; }
    }
}
