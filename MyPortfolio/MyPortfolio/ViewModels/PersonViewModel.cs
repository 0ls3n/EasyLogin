using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortfolio.ViewModels
{
    internal class PersonViewModel
    {
        private Person person;

        public string DisplayName { get; set; }

        public PersonViewModel(Person person)
        {
            this.person = person;
            if (this.person is MicrosoftUser mu)
            {
                DisplayName = mu.displayName;
            } else if (this.person is PortfolioPerson pu)
            {
                DisplayName = pu.DisplayName;
            }
        }
    }
}
