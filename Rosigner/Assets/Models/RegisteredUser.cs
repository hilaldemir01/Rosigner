using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Assets.Models
{
    [JsonObject,Serializable]
    public partial class RegisteredUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public string Hash { get; set; }
    }
}
