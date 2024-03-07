using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using MyPortfolio.ViewModels;

namespace MyPortfolio.Commands
{
    internal class LoginButtonCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove {  CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            //bool isActive = false;

            //if (parameter is LoginViewModel lvm)
            //{
            //    isActive = lvm.UsernameText != string.Empty && lvm.PasswordText != string.Empty;
            //}

            //return isActive;
            return true;
        }

        public void Execute(object? parameter)
        {
            
        }
    }
}
