using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using MyPortfolio.Models;
using MyPortfolio.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Policy;

namespace MyPortfolio.ViewModels
{
    internal class LoginViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        MainViewModel mvm;

        string usernameText = string.Empty;
        public string UsernameText 
        { 
            get => usernameText;

            set
            {
                usernameText = value;
                OnPropertyChanged("UsernameText");
            }
        }

        string passwordText = string.Empty;
        public string PasswordText 
        {
            get => passwordText; 
            set
            {
                passwordText = value;
                OnPropertyChanged("PasswordText");
            }
        }

        private PersonRepository personRepository;

        Person personToLogin;

        public LoginViewModel() 
        {
            personRepository = new PersonRepository();
        }

        public ICommand LoginCommand { get; set; } = new LoginButtonCommand();

        public bool Login()
        {
            personToLogin = personRepository.FindPerson(UsernameText);

            if (personToLogin != null && personToLogin.Password == PasswordText)
            {
                //MessageBox.Show($"{personToFind.DisplayName} is logged in");
                return true;
            }
            else
            {
                //MessageBox.Show("Incorrect username or password");
                return false;
            }
        }

        public void TransferPersonToViewModel(MainViewModel mvm)
        {
            mvm.AttachPerson(personToLogin);
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
