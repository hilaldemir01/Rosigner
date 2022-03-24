using System.Collections;
using UnityEngine;

namespace Assets.Models
{
    public class Wall 
    {
        public float wall_height { get; set; }
        public float wall_width { get; set; }

        public Wall(float wall_height, float wall_width)
        {
            this.wall_height = wall_height;
            this.wall_width = wall_width;
        }
    }
}