using System;
using System.ComponentModel;

namespace mesajprog1.Dto
{
    public class FriendDto : INotifyPropertyChanged
    {
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

        private string? _lastMessage;
        public string? LastMessage
        {
            get => _lastMessage;
            set
            {
                if (_lastMessage != value)
                {
                    _lastMessage = value;
                    OnPropertyChanged(nameof(LastMessage));
                }
            }
        }

        private DateTime? _LastMessageTime;
        public DateTime? LastMessageTime
        {
            get => _LastMessageTime;
            set
            {
                if (_LastMessageTime != value)
                {
                    _LastMessageTime = value;
                    OnPropertyChanged(nameof(LastMessageTime));
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



        private int? _requestId;
        public int? RequestId
        {
            get => _requestId;
            set
            {
                if (_requestId != value)
                {
                    _requestId = value;
                    OnPropertyChanged(nameof(RequestId));
                }
            }
        }
        // --------------------------

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
