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
        private Person activePerson;

        string usernameText;
        public string UsernameText { get => "Logged in as: " + usernameText; set { usernameText = value; } }

        PersonRepository personRepository;

        public MainViewModel()
        {
            personRepository = new PersonRepository();
            GetPersonLoggedIn();
        }

        private async void GetPersonLoggedIn()
        {
            var accounts = await App.PublicClientApp.GetAccountsAsync();
            if (accounts.Any())
            {
                try
                {
                    string accountId = accounts.FirstOrDefault().HomeAccountId.ObjectId.Substring(19).Remove(4, 1);

                    Person personToLogin = personRepository.FindPerson(accountId);

                    if (personToLogin != null)
                    {
                        activePerson = personToLogin;
                        UsernameText = (personToLogin as MicrosoftUser).displayName;
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
    }
}
