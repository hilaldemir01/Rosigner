using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
//model olarak dusun

namespace Assets.Models
{
    public partial class Measurement 
    {
        public int furniture_height { get; set; }
        public int furniture_width { get; set; }
        public int furniture_length { get; set; }
        public string furniture_name { get; set; }

    }
}
