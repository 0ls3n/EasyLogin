using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MyPortfolio.ViewModels;

namespace MyPortfolio.Commands
{
    internal class LoginButtonCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is LoginViewModel lvm)
            {
                lvm.Login();
            }
        }
    }
}
