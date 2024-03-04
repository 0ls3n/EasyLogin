using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MyPortfolio.Models;

namespace MyPortfolio.ViewModels
{
    internal class MainViewModel
    {
        private Person activePerson;

        string usernameText;
        public string UsernameText { get => usernameText; set { usernameText = activePerson.Username; } }

        public MainViewModel()
        {
            PersonRepository personRepository = new PersonRepository();

        }

        public void AttachPerson(Person person)
        {
            activePerson = person;
            usernameText = activePerson.DisplayName;
        }

        public void DetachPerson() 
        {
            activePerson = null;            
        }
    }
}
