using System;
using System.Collections.Generic;

namespace Assets.Models
{
    public partial class UserDesign
    {
        public int UserDesignId { get; set; }
        public int UserId { get; set; }
        public int DesignId { get; set; }

        public virtual Design Design { get; set; }
        public virtual RegisteredUser User { get; set; }
    }
}
