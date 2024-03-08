using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyPortfolio.Models
{
    public class MicrosoftUser : Person
    {
        public string givenName { get; set; }
        public string surname { get; set; }
        public string mail { get; set; }
        public string displayName { get; set; }
        public string id { get; set; }

        [JsonConstructor]
        public MicrosoftUser(string id, string givenName, string surname, string mail, string displayName)
        {
            this.givenName = givenName;
            this.surname = surname;
            this.mail = mail;
            this.displayName = displayName;
            this.id = id;
        }
    }
}
