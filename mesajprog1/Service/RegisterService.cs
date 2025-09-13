using mesajprog1.Dto;
using mesajprog1.Model;
using mesajprog1.View;
using mesajprog1.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace mesajprog1.Service
{
    class RegisterService
    {
        private readonly MainViewModel mainViewModel;
        private readonly HttpClient _httpClient;
        public RegisterService(MainViewModel _mainViewModel)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5298/") };
            mainViewModel = _mainViewModel;

        }

        public async Task Register(string username,string email,string password)
        {
            var RegisterData = new RegisterModel {Username = username, EmailAddress = email, Password = password }; 
            var content = new StringContent(JsonSerializer.Serialize(RegisterData),Encoding.UTF8, "application/json");
            try
            {
                var response = await _httpClient.PostAsync("api/user/register", content);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Kayıt Başarılı!");
                    mainViewModel.CurrentView = new LoginView(mainViewModel);
                }
                else System.Diagnostics.Debug.WriteLine("Giriş başarısız: " + response.StatusCode);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Register hatası: " + ex.Message);

            }
        }
    }
}
