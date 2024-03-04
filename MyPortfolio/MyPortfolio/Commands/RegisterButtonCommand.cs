using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MyPortfolio.Models;
using MyPortfolio.ViewModels;

namespace MyPortfolio.Commands
{
    internal class RegisterButtonCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            bool isActive = false;
            if (parameter is RegisterViewModel rvm)
            {
                isActive = rvm.UsernameText != string.Empty &&
                           rvm.PasswordText != string.Empty &&
                           rvm.EmailText != string.Empty &&
                           rvm.DisplayNameText != string.Empty;
            }

            return isActive;
        }

        public void Execute(object? parameter)
        {
            if (parameter is RegisterViewModel rvm)
            {
                rvm.RegisterNewPerson();
            }
        }
    }
}
