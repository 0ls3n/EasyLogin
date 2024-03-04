using MyPortfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MyPortfolio.Components;

namespace MyPortfolio.Views
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {

        RegisterViewModel rvm;
        public RegisterView()
        {
            InitializeComponent();

            rvm = new RegisterViewModel();
            this.DataContext = rvm;

            this.txtMessage.Text = string.Empty;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btn_Register_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            this.txtMessage.Text = string.Empty;
            if (!txtMail.Text.Contains("@"))
            {
                //MessageBox.Show("Invalid email adress!");
                stringBuilder.AppendLine("Invalid email adress!");
            }
            if (rvm.UserExists(txtUser.Text))
            {
                //MessageBox.Show("User already exists!");
                stringBuilder.AppendLine("User already exists!");
            }

            this.txtMessage.Text += stringBuilder.ToString();

            if (txtMail.Text.Contains("@") && !rvm.UserExists(txtUser.Text))
            {
                this.txtMessage.Text = string.Empty;
                rvm.RegisterButtonCommand.Execute(this.DataContext);
                GoBackToLogin();
            }
        }

        private void btn_Already_Have_Click(object sender, RoutedEventArgs e)
        {
            GoBackToLogin();
        }

        private void GoBackToLogin()
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            this.Close();
        }

        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (rvm.RegisterButtonCommand.CanExecute(this.DataContext))
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    this.txtMessage.Text = string.Empty;
                    if (!txtMail.Text.Contains("@"))
                    {
                        //MessageBox.Show("Invalid email adress!");
                        stringBuilder.AppendLine("Invalid email adress!");
                    }
                    if (rvm.UserExists(txtUser.Text))
                    {
                        //MessageBox.Show("User already exists!");
                        stringBuilder.AppendLine("User already exists!");
                    }

                    this.txtMessage.Text += stringBuilder.ToString();

                    if (txtMail.Text.Contains("@") && !rvm.UserExists(txtUser.Text))
                    {
                        this.txtMessage.Text = string.Empty;
                        rvm.RegisterButtonCommand.Execute(this.DataContext);
                        GoBackToLogin();
                    }
                }
            }
        }
    }
}
