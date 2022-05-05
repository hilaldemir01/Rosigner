using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class Wall
    {
        public int WallID { get; set; }
        public string WallName { get; set; }
        public float WallLength { get; set; }
        public float WallHeight { get; set; }
        public int RoomID { get; set; }
    }
}
