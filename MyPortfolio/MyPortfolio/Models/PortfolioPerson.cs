using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.Models
{
    public class PortfolioPerson : Person
    {
        public string DisplayName { get; set; }
        public string Mail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public PortfolioPerson(string displayName, string mail, string username, string password)
        {
            this.DisplayName = displayName;
            this.Mail = mail;
            this.Username = username;
            this.Password = password;
        }
    }
}
