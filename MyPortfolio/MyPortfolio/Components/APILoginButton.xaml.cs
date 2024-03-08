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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyPortfolio.Components
{
    /// <summary>
    /// Interaction logic for APILoginButton.xaml
    /// </summary>
    public partial class APILoginButton : UserControl
    {

        public string ImageSource
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PasswordProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("ImageSource", typeof(string), typeof(APILoginButton),
                new PropertyMetadata(string.Empty));

        public APILoginButton()
        {
            InitializeComponent();
        }
    }
}
