using System.Collections;
using UnityEngine;

namespace Assets.Models
{
    public class Furniture
    {

        public int furniture_type { get; set; }
        public int furniture_height { get; set; }
        public int furniture_width { get; set; }
        public int furniture_length { get; set; }

        public Furniture(int furniture_type, int furniture_height, int furniture_width, int furniture_length)
        {
            this.furniture_type = furniture_type;
            this.furniture_height = furniture_height;
            this.furniture_width = furniture_width;
            this.furniture_length = furniture_length;
        }
    }
}