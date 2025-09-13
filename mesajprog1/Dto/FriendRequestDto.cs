using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mesajprog1.Dto
{
    public class FriendRequestDto
    {
        private int _requestid;
        public int Requestid
        {
            get => _requestid;
            set
            {
                if (_requestid != value)
                {
                    _requestid = value;
                    OnPropertyChanged(nameof(Requestid));
                }
            }
        }
        private int _friendId;
        public int FriendId
        {
            get => _friendId;
            set
            {
                if (_friendId != value)
                {
                    _friendId = value;
                    OnPropertyChanged(nameof(FriendId));
                }
            }
        }

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

        private string _emailAddress;
        public string EmailAddress
        {
            get => _emailAddress;
            set
            {
                if (_emailAddress != value)
                {
                    _emailAddress = value;
                    OnPropertyChanged(nameof(EmailAddress));
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

        private DateTime _createdAt;
        public DateTime CreatedAt
        {
            get => _createdAt;
            set
            {
                if (_createdAt != value)
                {
                    _createdAt = value;
                    OnPropertyChanged(nameof(CreatedAt));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
