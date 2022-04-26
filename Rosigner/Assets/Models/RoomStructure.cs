using System;
using System.Collections.Generic;

namespace Assets.Models
{
    public partial class RoomStructure
    {
        public int RoomStructureID { get; set; }
        public int FurnitureTypeID { get; set; }
        public string SelectedWall { get; set; }
        public double StrructureLength  { get; set; }
        public double RedDotDistance { get; set; }
        public double GroundDistance { get; set; }
        public double StrructureWidth { get; set; }
        public int WallID { get; set; }

    }
}
