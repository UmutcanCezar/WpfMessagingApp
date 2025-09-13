using mesajprog1.Command;
using mesajprog1.Model;
using mesajprog1.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace mesajprog1.ViewModel
{
    public class TextBoxViewModel : ViewModelBase
    {
        public readonly MessageService messageService;
        private string _MesssageText;
        public string MessageText
        {
            get => _MesssageText;
            set
            {
                if (_MesssageText != value)
                {
                    _MesssageText = value;
                    OnPropertyChanged(nameof(MessageText));
                }
            }
        }

        public ICommand MessageSendCommand { get; set; }
        public TextBoxViewModel()
        {
            messageService = App.MessageService;
            MessageSendCommand = new RelayCommand(async _=> await SendMessage());


        }
        public async Task SendMessage()
        {
            if (CurrentUser.Currentuser != null && CurrentFriend.Currentfriend.FriendId != 0) await messageService.SendMessageAsync(MessageText);
            MessageText = string.Empty;

        }

    }
}
