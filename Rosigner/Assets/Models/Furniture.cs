using System;
using System.Collections.Generic;

namespace Assets.Models
{
    public partial class Furniture
    {
        public int FurnitureID { get; set; }
        public int FurnitureTypeID { get; set; }
        public double Xdimension { get; set; }
        public double Ydimension { get; set; }
        public double Zdimension { get; set; }
        public int RoomID { get; set; }

    }
}
