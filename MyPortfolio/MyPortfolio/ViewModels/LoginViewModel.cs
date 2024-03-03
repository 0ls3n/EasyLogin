using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using MyPortfolio.Models;
using MyPortfolio.Commands;

namespace MyPortfolio.ViewModels
{
    internal class LoginViewModel
    {
        public string UsernameText { get; set; }
        public string PasswordText { get; set; }

        private PersonRepository personRepository;

        public LoginViewModel() 
        {
            personRepository = new PersonRepository();
        }

        public ICommand LoginCommand { get; set; } = new LoginButtonCommand();

        public void Login()
        {
            MessageBox.Show("Loggin in...");
        }
    }
}
