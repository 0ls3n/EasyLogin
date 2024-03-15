using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MyPortfolio.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using Microsoft.Identity.Client;
using System.Diagnostics;
using System.Windows.Interop;
using System.Text.Json;
using BCrypt.Net;
using MyPortfolio.Views;

namespace MyPortfolio.ViewModels
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
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

        public void ReadUserFromJSON(string jsonFile)
        {
            MicrosoftUser microsoftPerson = null;
            try
            {
                microsoftPerson = JsonSerializer.Deserialize<MicrosoftUser>(jsonFile);
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            if (microsoftPerson != null)
            {
                if (personRepository.FindMicrosoftUser(microsoftPerson.id) != null)
                {
                    //MessageBox.Show($"Id: {microsoftPerson.id}, DisplayName: {microsoftPerson.displayName}");
                    personToLogin = microsoftPerson;
                } else
                {
                    //MessageBox.Show($"Person with Id: {microsoftPerson.id} does not exist in the current register");
                    personRepository.CreateNewMicrosoftPerson(microsoftPerson);
                    personToLogin = microsoftPerson;
                }
            }
        }

        public bool LoginPortfolioUser()
        {
            bool success = false;
            try
            {
                if (personRepository.FindPortfolioUser(UsernameText) != null 
                    && BCrypt.Net.BCrypt.Verify(PasswordText, personRepository.FindPortfolioUser(UsernameText).Password, false, HashType.SHA512))
                {
                    personToLogin = personRepository.FindPortfolioUser(UsernameText);
                    success = true;
                }
                else
                {
                    MessageBox.Show("Username or password is incorrect");
                    success = false;
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return success;
        }

        public void SendPersonToViewmodel(MainViewModel mvm)
        {
            this.mvm = mvm;
            mvm.RetrievePerson(personToLogin);
        }

        

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
