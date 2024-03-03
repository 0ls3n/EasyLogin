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
        public MainViewModel()
        {
            PersonRepository personRepository = new PersonRepository();

            Person person1 = new Person("Raol58380", "0zpSeKK2If", "Rasmus782@gmail.com", "0ls3n");

            personRepository.CreateNewPerson(person1);
        }
    }
}
