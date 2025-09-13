using mesajprog1.ViewModel;
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

namespace mesajprog1.View
{
    /// <summary>
    /// LoginView.xaml etkileşim mantığı
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView(MainViewModel mainViewModel)
        {
            InitializeComponent();
            this.DataContext = new LoginPageViewModel(mainViewModel);
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

    }
}
