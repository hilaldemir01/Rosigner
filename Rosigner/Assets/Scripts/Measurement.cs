using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;



    public class Measurement 
    {
        public int furniture_height { get; set; }
        public int furniture_width { get; set; }
        public int furniture_length { get; set; }

        public Measurement(int furniture_height, int furniture_width, int furniture_length)
        {
            this.furniture_height = furniture_height;
            this.furniture_width = furniture_width;
            this.furniture_length = furniture_length;
        }
    }
