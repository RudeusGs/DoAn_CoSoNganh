﻿namespace DragonAcc.Service.Models.CommentOrLike
{
    public class AddCommentOrLikeModel
    {
        public int? UserId { get; set; }
        public int? GameAccountId { get; set; }
        public string Comment { get; set; }
    }
}