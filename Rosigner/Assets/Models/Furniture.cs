using System;
using System.Collections.Generic;

namespace Assets.Models
{
    public partial class Furniture
    {
        public int FurnitureID { get; set; }
        public int FurnitureTypeID { get; set; }
        public int Xdimension { get; set; }
        public int Ydimension { get; set; }
        public int Zdimension { get; set; }
        public int RoomID { get; set; }

    }
}
