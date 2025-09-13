using mesajprog1.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mesajprog1.Model
{
    public static class CurrentFriendRequest
    {
        private static FriendRequestDto _currentfriendrequest = new FriendRequestDto();
        public static FriendRequestDto currentFriendRequest
        {
            get => _currentfriendrequest;
            set
            {
                if (_currentfriendrequest != value)
                {

                    _currentfriendrequest = value;

                    PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(CurrentFriendRequest)));
                }
            }
        }


        public static event PropertyChangedEventHandler PropertyChanged;
    }
}
