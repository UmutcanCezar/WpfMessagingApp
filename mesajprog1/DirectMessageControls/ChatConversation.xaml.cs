using mesajprog1.ViewModel;
using Microsoft.AspNetCore.SignalR.Client;
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

namespace mesajprog1.DirectMessageControls
{
    /// <summary>
    /// ChatConversation.xaml etkileşim mantığı
    /// </summary>
    public partial class ChatConversation : UserControl
    {
        public ChatConversation()
        {
            InitializeComponent(); 
            var viewModel = new ChatViewModel();
            this.DataContext = viewModel;

            viewModel.Conversations.CollectionChanged += (s, e) =>
            {
                // Yeni mesaj geldikçe otomatik aşağı kaydır
                ChatScrollViewer.ScrollToEnd();
            };
        }
    }
}
