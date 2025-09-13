using mesajprog1.Command;
using mesajprog1.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace mesajprog1.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private UserControl _currentView;
        public UserControl CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public ICommand CloseCommand { get; }
        public ICommand MinimizeCommand { get; }
        public ICommand WindowStateCommand { get; }
        public MainViewModel()
        {
            WindowStateCommand = new RelayCommand(ExecuteWindowStateCommand);
            MinimizeCommand = new RelayCommand(ExecuteMinimizeCommand);
            CloseCommand = new RelayCommand(ExecuteCloseCommand);
            CurrentView = new LoginView(this);
        }

        private void ExecuteBorderMouseDown(object obj)
        {
        }

        private void ExecuteWindowStateCommand(object obj)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized) Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void ExecuteMinimizeCommand(object obj)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void ExecuteCloseCommand(object obj)
        {

            Application.Current.Shutdown();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
