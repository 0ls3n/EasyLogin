using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MyPortfolio.Models;
using MyPortfolio.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using Microsoft.Identity.Client;
using System.Diagnostics;
using System.Windows.Interop;

namespace MyPortfolio.ViewModels
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        string graphAPIEndpoint = "https://graph.microsoft.com/v1.0/me";

        string[] scopes = new string[] { "user.read" };

        public event PropertyChangedEventHandler? PropertyChanged;

        MainViewModel mvm;

        string usernameText = string.Empty;
        public string UsernameText 
        { 
            get => usernameText;

            set
            {
                usernameText = value;
                OnPropertyChanged("UsernameText");
            }
        }

        string passwordText = string.Empty;
        public string PasswordText 
        {
            get => passwordText; 
            set
            {
                passwordText = value;
                OnPropertyChanged("PasswordText");
            }
        }

        private PersonRepository personRepository;

        Person personToLogin;

        public LoginViewModel() 
        {
            personRepository = new PersonRepository();
        }

        public ICommand LoginCommand { get; set; } = new LoginButtonCommand();

        //public bool Login()
        //{
        //    personToLogin = personRepository.FindPerson(UsernameText);

        //    if (personToLogin != null && personToLogin.Password == PasswordText)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public async void Login(Window window)
        {
            AuthenticationResult authResult = null;
            var app = App.PublicClientApp;

            IAccount firstAccount = (await app.GetAccountsAsync()).FirstOrDefault();

            if (firstAccount == null)
            {
                firstAccount = PublicClientApplication.OperatingSystemAccount; // Hvis den ikke kan finde bruger i cache, prøver den at finde den gennem operativ systemets bruger
            }

            try
            {
                authResult = await app.AcquireTokenSilent(scopes, firstAccount).ExecuteAsync(); // Prøver at logge ind gennem cache eller operativ systemets login
            } 
            catch (MsalUiRequiredException ex) // Hvis den ikke kunne logge ind gennem cache eller operativsystemet, kører den koden nedenfor
            {
                Debug.WriteLine(ex);

                try
                {
                    authResult = await app.AcquireTokenInteractive(scopes)
                        .WithAccount(firstAccount)
                        .WithParentActivityOrWindow(new WindowInteropHelper(window).Handle)
                        .WithPrompt(Prompt.SelectAccount)
                        .ExecuteAsync();
                } catch (MsalException msalex)
                {
                    Debug.WriteLine(msalex);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return;
            }

            if (authResult != null)
            {
                MessageBox.Show($"Logged in as: {authResult.Account.Username}");
            }
        }

        public void TransferPersonToViewModel(MainViewModel mvm)
        {
            mvm.AttachPerson(personToLogin);
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
