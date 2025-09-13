using mesajprog1.Dto;
using mesajprog1.Model;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace mesajprog1.Service
{
    public class MessageService
    {
        public ObservableCollection<MessageModel> Conversations { get; } = new();

        private HubConnection _connection;
        private readonly HttpClient _httpClient;
        public MessageService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5298/") };
        }

        public async Task LoadMessagesAsync()
        {
            var url = $"http://localhost:5298/api/message/{CurrentUser.Currentuser.Id}/{CurrentFriend.Currentfriend.FriendId}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var messages = JsonConvert.DeserializeObject<List<MessageModel>>(json);

                Conversations.Clear();
                foreach (var m in messages)
                {
                    Conversations.Add(m);
                }
            }
        }
        public async Task InitSignalRAsync()
        {if(CurrentUser.Currentuser!= null)
            _connection = new HubConnectionBuilder()
            .WithUrl($"http://localhost:5298/chathub?userId={CurrentUser.Currentuser.Id}")
            .WithAutomaticReconnect()
                .Build();

            _connection.On<int, int, string, DateTime>("ReceiveMessage", (senderID, receiverID, content, sentAt) =>
            {
                if (senderID == CurrentFriend.Currentfriend.FriendId || receiverID == CurrentFriend.Currentfriend.FriendId)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Conversations.Add(new MessageModel
                        {
                            SenderID = senderID,
                            ReceiverID = receiverID,
                            Content = content,
                            SentAt = sentAt
                        });
                    });
                }
            });
           

            await _connection.StartAsync();
        }

        public async Task SendMessageAsync(string NewMessage)
        {
            if (string.IsNullOrWhiteSpace(NewMessage)) return;

            if (_connection == null || _connection.State != HubConnectionState.Connected)
            {
                // Bağlantı yoksa yeniden başlat
               await InitSignalRAsync();
            }

            if(CurrentUser.Currentuser != null && CurrentFriend.Currentfriend.FriendId != 0) await _connection.InvokeAsync("SendMessage", CurrentUser.Currentuser.Id, CurrentFriend.Currentfriend.FriendId, NewMessage);

        }
    }
}
