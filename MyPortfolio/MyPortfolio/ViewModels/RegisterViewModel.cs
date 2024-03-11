using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string username;
        public string Username 
        {
            get => username; 
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        private string displayName;
        public string DisplayName
        {
            get => displayName;
            set
            {
                displayName = value;
                OnPropertyChanged("DisplayName");
            }
        }

        PersonRepository personRepository;

        public RegisterViewModel()
        {
            personRepository = new PersonRepository();
        }

        public void RegisterUser()
        {
            personRepository.CreateNewPortfolioPerson(new PortfolioPerson(DisplayName, Email, Username, Password));
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
