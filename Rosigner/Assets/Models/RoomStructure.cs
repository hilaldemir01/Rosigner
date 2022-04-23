using System;
using System.Collections.Generic;

namespace Assets.Models
{
    public partial class RoomStructure
    {
        public RoomStructure()
        {
            Room = new HashSet<Room>();
        }

        public int RoomStructureId { get; set; }
        public int FurnitureTypeId { get; set; }
        public string SelectedWall { get; set; }
        public double RoomStructureWidth { get; set; }
        public double DistanceFromRedDot { get; set; }
        public double DistanceFromGround { get; set; }
        public double RoomStructureHeight { get; set; }

        public virtual FurnitureType FurnitureType { get; set; }
        public virtual ICollection<Room> Room { get; set; }
    }
}
