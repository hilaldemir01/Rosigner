using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Models
{
    public class Room 
    {
        public int room_type { get; set; }
        public List<Wall> number_of_walls { get; set; }

        public Room(int room_type, List<Wall> number_of_walls)
        {
            this.room_type = room_type;
            this.number_of_walls = number_of_walls;
        }
    }
}