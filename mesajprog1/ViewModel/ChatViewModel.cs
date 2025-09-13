using mesajprog1.Command;
using mesajprog1.Model;
using mesajprog1.Service;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace mesajprog1.ViewModel
{
    public class ChatViewModel : ViewModelBase
    {
        private readonly MessageService _messageService;

        public ObservableCollection<MessageModel> Conversations => _messageService.Conversations;


        public ChatViewModel()
        {
            _messageService = App.MessageService;
            CurrentFriend.PropertyChanged += async (s, e) =>
            {
                if (e.PropertyName == nameof(CurrentFriend.Currentfriend))
                {
                    await _messageService.LoadMessagesAsync();
                }
            };

        }

    }
}
