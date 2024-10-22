namespace DragonAcc.Service.Models.AccountGame
{
    public class CommentandLikeModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Comment { get; set; }
        public int? Like { get; set; }
    }
}
