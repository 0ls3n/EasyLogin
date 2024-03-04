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

namespace MyPortfolio.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {

        LoginViewModel lvm;
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
            Application.Current.Shutdown();
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            if (lvm.Login())
            {
                MainWindow mainWindow = new MainWindow();
                lvm.TransferPersonToViewModel((MainViewModel)mainWindow.DataContext);
                mainWindow.Show();
                this.Close();
            }
        }

        private void btn_Forgot_Passsword_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterView registerView = new RegisterView();
            registerView.Show();
            this.Close();
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (txtUser.Text != string.Empty && txtPass.Password != string.Empty)
                {
                    if (lvm.Login())
                    {
                        MainWindow mainWindow = new MainWindow();
                        lvm.TransferPersonToViewModel((MainViewModel)mainWindow.DataContext);
                        mainWindow.Show();
                        this.Close();
                    }
                }
            }
        }
    }
}
