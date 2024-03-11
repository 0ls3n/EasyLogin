using Microsoft.Identity.Client;
using MyPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyPortfolio.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        string graphAPIEndpoint = "https://graph.microsoft.com/v1.0/me";

        LoginViewModel lvm;

        private string[] scopes = new string[] { "user.read" };

        public LoginView()
        {
            InitializeComponent();

            lvm = new LoginViewModel();
            this.DataContext = lvm;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                WindowState = WindowState.Normal;
                DragMove();
            }
        }

        private void btn_Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            App.ShutAppDown();
        }

        private async void btn_Login_Microsoft_Click(object sender, RoutedEventArgs e)
        {
            AuthenticationResult authResult = null;
            var app = App.PublicClientApp;

            IAccount firstAccount = null; // (await app.GetAccountsAsync()).FirstOrDefault();

            if (firstAccount == null)
            {
                firstAccount = null;//PublicClientApplication.OperatingSystemAccount; // Hvis den ikke kan finde bruger i cache, prøver den at finde den gennem operativ systemets bruger
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
                    authResult = await app.AcquireTokenInteractive(scopes) // Åbner et nyt vindue fra Microsoft Identity API, hvor brugeren kan logge ind fra
                        .WithAccount(firstAccount)
                        .WithParentActivityOrWindow(new WindowInteropHelper(this).Handle)
                        .WithPrompt(Prompt.SelectAccount)
                        .ExecuteAsync();
                }
                catch (MsalException msalex)
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
                // To revieve the id from an IAccount -> string id = authResult.Account.HomeAccountId.ObjectId.Substring(19).Remove(4, 1).ToString();
                lvm.ReadUserFromJSON(await GetProfile(graphAPIEndpoint, authResult.AccessToken));
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }

        private async Task<string> GetProfile(string url, string accessToken)
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            System.Net.Http.HttpResponseMessage response;
            try
            {
                System.Net.Http.HttpRequestMessage request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, url);

                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                response = await httpClient.SendAsync(request);
                string content = await response.Content.ReadAsStringAsync();
                return content;
            } catch (Exception Ex)
            {
                return Ex.ToString();
            }
        }

        private void btn_Forgot_Passsword_Click(object sender, RoutedEventArgs e)
        {

        }

        

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Return)
            //{
            //    if (txtUser.Text != string.Empty && txtPass.Password != string.Empty)
            //    {
            //        if (lvm.Login())
            //        {
            //            MainWindow mainWindow = new MainWindow();
            //            lvm.TransferPersonToViewModel((MainViewModel)mainWindow.DataContext);
            //            mainWindow.Show();
            //            this.Close();
            //        } else
            //        {
            //            this.txtMessage.Text = "Wrong password or username!";
            //        }
            //    }
            //}
        }

        private void btn_Standard_Login_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Register_New_Account_Click(object sender, RoutedEventArgs e)
        {
            RegisterView registerView = new RegisterView();
            registerView.Show();
            this.Close();
        }
    }
}
