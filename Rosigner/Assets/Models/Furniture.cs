using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Assets.Models
{
    [JsonObject,Serializable]
    public partial class Furniture
    {
        public int FurnitureID { get; set; }
        public int FurnitureTypeID { get; set; }
        public float Xdimension { get; set; }
        public float Ydimension { get; set; }
        public float Zdimension { get; set; }
        public int RoomID { get; set; }

    }
}
