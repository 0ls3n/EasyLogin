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

        string usernameText;
        public string UsernameText { get => "Logged in as: " + usernameText; set { usernameText = value; } }

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

        PortfolioContent PortfolioContentView { get; set; }
        PortfolioContentViewModel PortfolioContentVM { get; set; }

        LoginViewModel LoginViewModel; 

        PersonRepository personRepository;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainViewModel()
        {
            personRepository = new PersonRepository();
            
            PortfolioContentView = new PortfolioContent();
            PortfolioContentVM = (PortfolioContentViewModel)PortfolioContentView.DataContext;
            ContentControl = PortfolioContentView;
            GetPersonLoggedIn();

            //PortfolioContentVM.ActivePersonDisplayName(activePerson);
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
                        PortfolioContentVM.ActivePersonDisplayName(activePerson);
                        return;
                    }

                    //MessageBox.Show(accountId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            } else
            {
                
            }
        }

        public void RetrievePerson(Person personToRetrieve)
        {
            activePerson = personToRetrieve;
            PortfolioContentVM.ActivePersonDisplayName(activePerson);
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
