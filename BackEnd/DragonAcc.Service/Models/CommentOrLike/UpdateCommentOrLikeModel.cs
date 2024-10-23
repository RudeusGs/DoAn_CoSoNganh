namespace DragonAcc.Service.Models.CommentOrLike
{
    public class UpdateCommentOrLikeModel
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? GameAccountId { get; set; }
        public string Comment { get; set; }
    }
}
