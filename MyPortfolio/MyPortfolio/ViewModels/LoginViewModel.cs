using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortfolio.Models;

namespace MyPortfolio.ViewModels
{
    internal class LoginViewModel
    {

        private PersonRepository personRepository;

        public LoginViewModel() 
        {
            personRepository = new PersonRepository();
        }
    }
}
