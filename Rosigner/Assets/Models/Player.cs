using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assets.Models
{
    [JsonObject,Serializable]
    public class Player
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public float Score { get; set; }
    }
}
