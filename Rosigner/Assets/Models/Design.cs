using System;
using System.Collections.Generic;

namespace Assets.Models
{
    public partial class Design
    {
        public Design()
        {
            UserDesign = new HashSet<UserDesign>();
        }

        public int DesignId { get; set; }
        public int FurnitureId { get; set; }
        public int RoomId { get; set; }
        public string DesignImage { get; set; }

        public virtual Furniture Furniture { get; set; }
        public virtual Room Room { get; set; }
        public virtual ICollection<UserDesign> UserDesign { get; set; }
    }
}
