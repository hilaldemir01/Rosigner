using System;
using System.Collections.Generic;

namespace Assets.Models
{
    public partial class FurnitureType
    {
        public FurnitureType()
        {
            Furniture = new HashSet<Furniture>();
            RoomStructure = new HashSet<RoomStructure>();
        }

        public int FurnitureTypeId { get; set; }
        public string FurnitureType1 { get; set; }

        public virtual ICollection<Furniture> Furniture { get; set; }
        public virtual ICollection<RoomStructure> RoomStructure { get; set; }
    }
}
