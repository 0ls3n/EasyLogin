using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MyPortfolio.Commands;
using MyPortfolio.Models;

namespace MyPortfolio.ViewModels
{
    internal class RegisterViewModel
    {
        private PersonRepository personRepository;

        public string UsernameText { get; set; }
        public string PasswordText { get; set; }
        public string EmailText { get; set; }
        public string DisplayNameText { get; set; }

        public RegisterViewModel()
        {
            personRepository = new PersonRepository();
        }

        public ICommand RegisterButtonCommand { get; set; } = new RegisterButtonCommand();

        public void RegisterNewPerson()
        {
            Person personToCreate = new Person(UsernameText, PasswordText, EmailText, DisplayNameText);

            personRepository.CreateNewPerson(personToCreate);

            MessageBox.Show("Succesfully created a new person!!");
        }


    }
}
