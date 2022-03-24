using System.Collections;
using UnityEngine;

namespace Assets.Models
{
    public class User 
    {
        public int userType { get; set; }

        public User(int userType)
        {
            this.userType = userType;
        }
    }
}