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
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
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
