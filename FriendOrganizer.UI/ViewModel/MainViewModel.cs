using FriendOrganizer.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using FriendOrganizer.UI.Data;

namespace FriendOrganizer.UI.ViewModel
{
    public partial class MainViewModel:ViewModelBase
    {
        private IFriendDataService _friendDataService;
        private Friend _selectedFriend;
       

        public MainViewModel(IFriendDataService frienDataService)
        {
            Friends = new ObservableCollection<Friend>();
            _friendDataService = frienDataService;
        }
        public ObservableCollection<Friend> Friends { get; set; }
        public void Load()
        {
            var friends = _friendDataService.GetAll();
            Friends.Clear();
            foreach (var friend in friends)
            {
                Friends.Add(friend);
            }
        }

        public Friend SelectedFriend
        {
            get { return _selectedFriend; }
            set
            {
                _selectedFriend = value; 
                OnPropertyChanged();
            }
        }


    }
}
