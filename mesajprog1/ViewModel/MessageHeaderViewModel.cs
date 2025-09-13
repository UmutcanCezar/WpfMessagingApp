using mesajprog1.Dto;
using mesajprog1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mesajprog1.ViewModel
{
    public class MessageHeaderViewModel : ViewModelBase
    {
        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }
        private string _profilePictureUrl;
        public string ProfilePictureUrl
        {
            get => _profilePictureUrl;
            set
            {
                if (_profilePictureUrl != value)
                {
                    _profilePictureUrl = value;
                    OnPropertyChanged(nameof(ProfilePictureUrl));
                }
            }
        }

        public MessageHeaderViewModel()
        {
          


            CurrentFriend.PropertyChanged += (s, e) =>
            {
                Username = CurrentFriend.Currentfriend.Username;
                if (CurrentFriend.Currentfriend.ProfilePictureUrl == null) ProfilePictureUrl = "C:\\Users\\Umutcan\\Desktop\\mesajprog\\mesajprog1\\mesajprog1\\assets\\profilphoto.png";
                else ProfilePictureUrl = CurrentFriend.Currentfriend.ProfilePictureUrl;
            };
        }

    }
}
