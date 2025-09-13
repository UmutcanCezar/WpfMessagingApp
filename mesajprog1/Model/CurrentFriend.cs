using mesajprog1.Dto;
using mesajprog1.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mesajprog1.Model
{
    public class CurrentFriend
    {
        private static FriendDto _currentfriend = new FriendDto();
        public static FriendDto Currentfriend
        {
            get => _currentfriend;
            set
            {
                if (_currentfriend != value)
                {

                    _currentfriend = value;

                    PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(CurrentFriend)));
                }
            }
        }

        private static FriendDto _currentfriendreq = new FriendDto();
        public static FriendDto Currentfriendreq
        {
            get => _currentfriendreq;
            set
            {
                if (_currentfriendreq != value)
                {

                    _currentfriendreq = value;

                    PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(CurrentFriend)));
                }
            }
        }
        public static event PropertyChangedEventHandler PropertyChanged;

    }
}
