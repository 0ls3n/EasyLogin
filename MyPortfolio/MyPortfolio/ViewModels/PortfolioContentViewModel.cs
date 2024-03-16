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
    class PortfolioContentViewModel : INotifyPropertyChanged
    {
        private string labelText;
        public string LabelText 
        {
            get => labelText; 
            set
            {
                labelText = value;
                OnPropertyChanged("LabelText");
            }
        }

        public PortfolioContentViewModel()
        {

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void ActivePersonDisplayName(Person person)
        {
            if (person is MicrosoftUser mu)
            {
                LabelText = mu.displayName;
            } else if (person is PortfolioPerson pu)
            {
                LabelText = pu.DisplayName;
            }
        }

        public void AddNewPortfolio(Person person,  PersonRepository personRepository)
        {
            PortfolioRepository portfolioRepository = new PortfolioRepository(personRepository);
            portfolioRepository.CreateNewPortfolio("Lorem Ipsum Dolor Sit Amet", person);
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
