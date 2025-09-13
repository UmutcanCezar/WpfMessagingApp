using mesajprog1.Command;
using mesajprog1.Service;
using mesajprog1.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace mesajprog1.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly MainViewModel _mainViewModel;
        private readonly RegisterService _registerService;
        private string _Username;
        public string Username
        {
            get => _Username;
            set
            {
                if (_Username != value)
                {
                    _Username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }


        private string _Email;
        public string Email
        {
            get => _Email;
            set
            {
                if (_Email != value)
                {
                    _Email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }


        private string _Password;
        public string Password
        {
            get =>  _Password;
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }


        private string _ResetPassword;
        public string ResetPassword
        {
            get => _ResetPassword;
            set
            {
                if (_ResetPassword != value)
                {
                    _ResetPassword = value;
                    OnPropertyChanged(nameof(ResetPassword));
                }
            }
        }




        public ICommand BackToLoginCommand { get; }
        public ICommand RegisterCommand { get; }
        public RegisterViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _registerService = new RegisterService(_mainViewModel);
            BackToLoginCommand = new RelayCommand(_ => _mainViewModel.CurrentView = new LoginView(_mainViewModel));
            RegisterCommand = new RelayCommand(async _ =>await executeRegisterCommand(),_ => CanExecuteRegisterCommand());
        }
        public async Task executeRegisterCommand()
        {
            await _registerService.Register(Username, Email, Password);

        }
        public bool CanExecuteRegisterCommand()
        {
            return !((string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(ResetPassword)) || ValidateEmail(Email));

        }
    }
}
