using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mesajprog1.Model
{
    public class FriendState : INotifyPropertyChanged
    {
        private byte _addOrChat;
        public byte AddOrChat
        {
            get => _addOrChat;
            set
            {
                if (_addOrChat != value)
                {
                    _addOrChat = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddOrChat)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
