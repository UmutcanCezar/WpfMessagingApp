using mesajprog1.Dto;
using mesajprog1.Model;
using mesajprog1.View;
using mesajprog1.ViewModel;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;


namespace mesajprog1.Service
{
    class FriendService
    {
        public event Action<FriendDto> OnFriendsUpdate;
        public ObservableCollection<FriendDto> Friends { get; set; } = new();
        private readonly HttpClient _httpClient;
        private HubConnection _connection;
        public FriendService()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5298/") };
            RegisterHubEvents();
            _ = StartHubAsync();

        }
        private void RegisterHubEvents()
        {
            if(CurrentUser.Currentuser!=null)_connection = new HubConnectionBuilder().WithUrl($"http://localhost:5298/chathub?userId={CurrentUser.Currentuser.Id}").WithAutomaticReconnect().Build();
            _connection.On<int,int,string,DateTime>("LastMessage", (senderId, receiverId, message,sentAt) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var friend = Friends.FirstOrDefault(f => f.FriendId == senderId || f.FriendId == receiverId);
                    if (friend != null)
                    {
                        friend.LastMessage = message;
                        friend.LastMessageTime = sentAt;
                        OnFriendsUpdate?.Invoke(friend);

                    }
                });
            });
        }

        private async Task StartHubAsync()
        {
            try
            {
                await _connection.StartAsync();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Hub connection failed: {ex.Message}");
            }
        }
        public async Task<List<FriendDto>> LoadFriends(int userid)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<FriendDto>>($"api/Friend/{userid}");
            }
            catch (HttpRequestException httpEx)
            {
                // Loglama veya hata mesajı
                System.Diagnostics.Debug.WriteLine($"HTTP Error: {httpEx.Message}");
                return new List<FriendDto>(); // veya null dönebilirsin
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"General Error: {ex.Message}");
                return new List<FriendDto>();
            }
        }

        public async Task SendRequest(int senderId,int receiverId)
        {
            try
            {
                var response = await _httpClient.PostAsync($"api/FriendRequest/send-request?senderId={senderId}&receiverId={receiverId}",null);
                if (response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(message);

                }
                else
                {
                    var status = response.StatusCode;
                    var content = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Hata! StatusCode: {status}\nMesaj: {content}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SendRequest Error: {ex.Message}");
                MessageBox.Show("İstek gönderilirken hata oluştu.", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public async Task RejectRequest(int requestid)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/FriendRequest/reject/{requestid}");
                if (response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(message);
                }
                else
                {
                    var status = response.StatusCode;
                    var content = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Hata! StatusCode: {status}\nMesaj: {content}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SendRequest Error: {ex.Message}");
                MessageBox.Show("İstek gönderilirken hata oluştu.", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public async Task AcceptRequest(int requestid)
        {
            try
            {
                var response = await _httpClient.PostAsync($"api/FriendRequest/accept/{requestid}",null);
                if (response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    MessageBox.Show(message);
                }
                else
                {
                    var status = response.StatusCode;
                    var content = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Hata! StatusCode: {status}\nMesaj: {content}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SendRequest Error: {ex.Message}");
                MessageBox.Show("İstek gönderilirken hata oluştu.", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task<List<FriendDto>> LoadRequests(int userid)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<FriendDto>>($"api/friendrequest/pending/{userid}");
            }
            catch (HttpRequestException httpEx)
            {
                System.Diagnostics.Debug.WriteLine($"{httpEx.Message}");
                return new List<FriendDto>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"General Error: {ex.Message}");
                return new List<FriendDto>();
            }
        }
        
        public async Task<List<FriendDto>> LoadNotFriend(int userid)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<FriendDto>>($"api/Friend/notfriends/{userid}");

            }
            catch(HttpRequestException httpEx)
            {
                System.Diagnostics.Debug.WriteLine($"{httpEx.Message}");
                return new List<FriendDto>();
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"General Error: {ex.Message}");
                return new List<FriendDto>();
            }
        }
        
    
    }
        

    }

