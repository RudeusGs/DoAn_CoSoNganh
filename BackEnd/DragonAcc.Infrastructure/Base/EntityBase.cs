﻿namespace DragonAcc.Infrastructure.Base
{
    public abstract class EntityBase
    {
        public int? Id { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }
        public virtual DateTime? DeleteDate { get; set; }
    }
}
