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
    public class LoginService
    {
        private readonly MainViewModel mainViewModel;
        private readonly HttpClient _httpClient;
        public LoginService(MainViewModel _mainViewModel)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5298/") };
            mainViewModel = _mainViewModel;

        }
        public async Task<bool> Login(string Email, string Password)
        {
            var loginData = new LoginModel { Email = Email, Password = Password };
            var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");
            try
            {
                var response = await _httpClient.PostAsync("api/user/login", content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    // Başarısız giriş
                    System.Diagnostics.Debug.WriteLine("Giriş başarısız: " + response.StatusCode);
                    return false;
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Login hatası: " + ex.Message);
                return false;

            }
        }
        public async Task<User> GetUserDataAsync(string EmailAddress)
        {
            var encodedEmail = WebUtility.UrlEncode(EmailAddress); // <== burada encode ediyoruz

            var response = await _httpClient.GetAsync($"api/User/{encodedEmail}");

            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();


            var userDtos = JsonSerializer.Deserialize<List<UserDto>>(json);

            // Eğer liste boş değilse, ilk kullanıcıyı al:
            var firstUserDto = userDtos.FirstOrDefault();
            if (firstUserDto != null)
            {
                var user = new User
                {
                    Id = firstUserDto.id,
                    Username = firstUserDto.username,
                    Password = firstUserDto.password,
                    EmailAddress = firstUserDto.emailAddress,
                    ProfilePictureUrl = firstUserDto.profilePictureUrl
                };

                return user;
            }
            else return null;
        }
    }
}
