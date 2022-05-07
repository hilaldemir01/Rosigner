using System;
using System.Collections.Generic;

namespace Assets.Models
{
    public partial class RoomStructure
    {
        public int RoomStructureID { get; set; }
        public int FurnitureTypeID { get; set; }
        public float StrructureLength  { get; set; }
        public float RedDotDistance { get; set; }
        public float GroundDistance { get; set; }
        public float StrructureWidth { get; set; }
        public int WallID { get; set; }

    }
}
