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

        public void ActivePersonDisplayName(Person activePerson)
        {
            if (activePerson is MicrosoftUser mu)
            {
                LabelText = mu.displayName;
            } else if (activePerson is PortfolioPerson pu)
            {
                LabelText = pu.DisplayName;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
