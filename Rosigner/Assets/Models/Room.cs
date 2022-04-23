using System;
using System.Collections.Generic;

namespace Assets.Models
{
    public partial class Room
    {
        public Room()
        {
            Design = new HashSet<Design>();
        }

        public int RoomId { get; set; }
        public double Wall1Length { get; set; }
        public double Wall2Length { get; set; }
        public int RoomStructureId { get; set; }
        public double WallHeight { get; set; }

        public virtual RoomStructure RoomStructure { get; set; }
        public virtual ICollection<Design> Design { get; set; }
    }
}
