using System.Collections;
using UnityEngine;

namespace Assets.Models
{
    public class RegisteredUser : User
    {
        public RegisteredUser(string name, string surname, int gender, string email, string password, int userType ): base(userType)
        {
            this.name = name;
            this.surname = surname;
            this.gender = gender;
            this.email = email;
            this.password = password;
            this.userType = userType;
        }

        public string name { get; set; }
        public string surname { get; set; }
        public int gender { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}