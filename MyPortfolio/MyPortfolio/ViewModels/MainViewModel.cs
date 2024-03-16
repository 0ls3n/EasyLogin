using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MyPortfolio.Models;
using MyPortfolio.Views;

namespace MyPortfolio.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private Person activePerson;
        public PersonViewModel personVM;

        private object contentControl;
        public object ContentControl
        {
            get { return contentControl; }
            set
            {
                contentControl = value;
                OnPropertyChanged("ContentControl");
            }
        }

        PortfolioContentViewModel PortfolioContentVM;

        PersonRepository personRepository;
        PortfolioRepository PortfolioRepository;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainViewModel()
        {
            personRepository = new PersonRepository();
            personVM = new PersonViewModel(activePerson);
            GetPersonLoggedIn();

            PortfolioContentVM = new PortfolioContentViewModel(this);
            
           
            

            PortfolioRepository = new PortfolioRepository(personRepository);
            
            ContentControl = PortfolioContentVM;
            
        }

        private async void GetPersonLoggedIn()
        {
            var accounts = await App.PublicClientApp.GetAccountsAsync();
            if (accounts.Any())
            {
                try
                {
                    string accountId = accounts.FirstOrDefault().HomeAccountId.ObjectId.Substring(19).Remove(4, 1);

                    Person personToLogin = personRepository.FindMicrosoftUser(accountId);

                    if (personToLogin != null)
                    {
                        activePerson = personToLogin;
                        //UsernameText = (activePerson as MicrosoftUser).displayName;
                        //PortfolioContentVM.ActivePersonDisplayName(activePerson);
                        //PortfolioContentVM.AddNewPortfolio(activePerson, personRepository);
                        return;
                    }

                    //MessageBox.Show(accountId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        public void RetrievePerson(Person personToRetrieve)
        {
            activePerson = personToRetrieve;
            //PortfolioContentVM.ActivePersonDisplayName(activePerson);
            //PortfolioContentVM.AddNewPortfolio(activePerson, personRepository);
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
