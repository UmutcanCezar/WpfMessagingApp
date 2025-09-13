using mesajprog1.Command;
using mesajprog1.Dto;
using mesajprog1.Model;
using mesajprog1.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace mesajprog1.ViewModel
{
    public class FriendsViewModel : ViewModelBase
    {


        private string _searchtext;
        public string searchtext
        {
            get => _searchtext;
            set
            {
                if (_searchtext != value)
                {
                    _searchtext = value;
                    OnPropertyChanged(nameof(searchtext));
                    FriendsView.Refresh();
                }
            }
        }

        public MessageHeaderViewModel x { get; set; }
        private readonly FriendService __friendservice;
        public ObservableCollection<FriendDto> Friends { get; set; } = new();
        public ICollectionView FriendsView { get; set; }
        public ObservableCollection<FriendDto> Requests { get; set; } = new();
        private readonly FriendState friendState;
        public ICommand SelectFriendCommand { get; set; }
        public ICommand AddCommand {  get; set; }
        public ICommand ChatListCommand { get; set; }
        public ICommand RequestsCommand { get; set; }
        public ICommand HoverFriendCommand { get; set; }
        public ICommand SendRequestCommand { get; set; }
        public ICommand RejectRequestCommand { get; set; }
        public ICommand AcceptRequestCommand { get; set; }
        public FriendsViewModel()
        {
            friendState = (FriendState)Application.Current.Resources["FriendState"];
            friendState.PropertyChanged += FriendState_PropertyChanged;
            friendState.AddOrChat = 0;
            AddCommand = new RelayCommand(_ => friendState.AddOrChat = 1);
            ChatListCommand = new RelayCommand(_ => friendState.AddOrChat = 0);
            SendRequestCommand = new RelayCommand<int>(async friendId=> await SendRequest(friendId));
            HoverFriendCommand = new RelayCommand<FriendDto>(async friend => await HoverSelecet(friend));
            SelectFriendCommand = new RelayCommand<FriendDto>(async friend => await SelectFriend(friend));
            RejectRequestCommand = new RelayCommand<int>(async requestId => await RejectRequest(requestId));
            AcceptRequestCommand = new RelayCommand<int>(async requestId => await AcceptRequest(requestId));
            RequestsCommand = new RelayCommand(_ => friendState.AddOrChat = 2);

                __friendservice = new FriendService();
            Friends = __friendservice.Friends;
           
           _ =  InitializeAsync();
            FriendsView = CollectionViewSource.GetDefaultView(Friends);
            FriendsView.SortDescriptions.Add(new SortDescription(nameof(FriendDto.LastMessageTime), ListSortDirection.Descending));
            FriendsView.Filter = FilterFriend;
            __friendservice.OnFriendsUpdate += friend => FriendsView.Refresh();
        }
        private async void FriendState_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FriendState.AddOrChat))
            {
                await InitializeAsync();
            }
        }
        private bool FilterFriend(object obj)
        {
            if(obj is FriendDto friend)
            {
                if (string.IsNullOrWhiteSpace(searchtext)) return true;
                return friend.Username.Contains(searchtext, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        private async Task HoverSelecet(FriendDto friend)
        {
            if(friendState.AddOrChat != 0)
            {
                if (friend == null) return;
                CurrentFriend.Currentfriendreq = friend;
            }
        }
        private async Task SelectFriend(FriendDto friend)
        {
           if(friendState.AddOrChat == 0)
            {
                if (friend == null) return;
                CurrentFriend.Currentfriend = friend;
                await App.MessageService.LoadMessagesAsync();
            }
           // if (friendState.AddOrChat == 1)
            //{
              //  await __friendservice.SendRequest(CurrentUser.Currentuser.Id,friend.FriendId);
                //await LoadNotFriendsAsync();
           // }
            
        }
        private async Task SendRequest(int friendid)
        {
            if (CurrentUser.Currentuser != null)
            {
                await __friendservice.SendRequest(CurrentUser.Currentuser.Id, friendid);
                await InitializeAsync();
            }
            else return;
        }
        private async Task RejectRequest(int requestid)
        {
            if (CurrentUser.Currentuser != null)
            {
                await __friendservice.RejectRequest(requestid);
                await InitializeAsync();
            }
            else return;
        }
        private async Task AcceptRequest(int requestid)
        {
            if (CurrentUser.Currentuser != null)
            {
                await __friendservice.AcceptRequest(requestid);
                await InitializeAsync();
            }
            else return;
        }
        private async Task InitializeAsync()
        {
            if (friendState.AddOrChat == 1) await LoadNotFriendsAsync();
            else if (friendState.AddOrChat == 0) await LoadFriendsAsync();
            else await LoadRequestAsync();
        }

        private async Task LoadFriendsAsync()
        {
            if(CurrentUser.Currentuser == null)
            {
                return;

            }
            var friendlist = await __friendservice.LoadFriends(CurrentUser.Currentuser.Id);
            if (friendlist != null) {
                Friends.Clear();
                foreach (var friend in friendlist) {
                    if (friend.ProfilePictureUrl == null) friend.ProfilePictureUrl = "C:\\Users\\Umutcan\\Desktop\\mesajprog\\mesajprog1\\mesajprog1\\assets\\2.jpg";

                    Friends.Add(friend);
                
                }
            
            }
        }
        private async Task LoadNotFriendsAsync()
        {
            if(CurrentUser.Currentuser == null)
            {
                return;

            }

            var notfriendlist = await __friendservice.LoadNotFriend(CurrentUser.Currentuser.Id);
            if(notfriendlist != null)
            {
                Friends.Clear();
                foreach(var notfriend in notfriendlist)
                {
                    if(notfriend.ProfilePictureUrl == null) notfriend.ProfilePictureUrl = "C:\\Users\\Umutcan\\Desktop\\mesajprog\\mesajprog1\\mesajprog1\\assets\\3.jpg";
                    Friends.Add(notfriend);
                }
            }

        }
        public async Task LoadRequestAsync()
        {
            if (CurrentUser.Currentuser == null) return;
            var requestlist = await __friendservice.LoadRequests(CurrentUser.Currentuser.Id);

            if(requestlist != null)
            {
                Requests.Clear();
                foreach(var  request in requestlist)
                {
                    if (request.ProfilePictureUrl == null) request.ProfilePictureUrl = "C:\\Users\\Umutcan\\Desktop\\mesajprog\\mesajprog1\\mesajprog1\\assets\\8.jpg";

                    Requests.Add(request);
                }
            }

        }

    }

}
