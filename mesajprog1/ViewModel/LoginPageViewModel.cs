using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using mesajprog1.Command;
using mesajprog1.Model;
using mesajprog1.Service;
using mesajprog1.View;
using Microsoft.Win32;

namespace mesajprog1.ViewModel
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly LoginService _LoginService;
        private string _Email;
        private bool _showPassword;
        public string Email
        {
            get => _Email;
            set
            {
                _Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        private string _Password;
        public string Password
        {
            get => _Password;
            set
            {
                _Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public bool ShowPassword
        {
            get => _showPassword;
            set
            {
                _showPassword = value;
                OnPropertyChanged();
            }
        }

        private readonly MainViewModel _mainViewModel;

        public ICommand LoginCommand { get; }
        public ICommand LoginRegisterCommand { get; }
        public LoginPageViewModel(MainViewModel mainViewModel)
        {
            _LoginService = new LoginService(mainViewModel);
            _mainViewModel = mainViewModel;
            LoginCommand = new RelayCommand(async _ => await ExecuteLogin(),_ => CanExecuteLogin());
            LoginRegisterCommand = new RelayCommand(_ => _mainViewModel.CurrentView = new RegisterView(_mainViewModel));
        }




        private bool CanExecuteLogin()
        {
            return !((string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password)) || ValidateEmail(Email) ) ;
        }
private async Task ExecuteLogin()
{
            bool loginSuccess = await _LoginService.Login(Email, Password);
            if (loginSuccess)
    {
        await LoadDataAsync(); // await eklemelisin
                               // Giriş başarılı ve CurrentUser set edilmiş olur

                _mainViewModel.CurrentView = new DirectMessage();
            }
    else
    {
        MessageBox.Show("Login failed. Check credentials.");
    }}

        public async Task LoadDataAsync()
        {
            try
            {
                var data = await _LoginService.GetUserDataAsync(Email);
                CurrentUser.Currentuser = data;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}
