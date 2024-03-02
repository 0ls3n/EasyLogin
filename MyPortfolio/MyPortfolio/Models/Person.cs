using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Models
{
    public class Person
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }

        public Person(string username, string password, string email, string displayName)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.DisplayName = displayName;
        }   
    }
}
