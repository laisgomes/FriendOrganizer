﻿using FriendOrganizer.UI.Data.Lookups;
using FriendOrganizer.UI.Event;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    class NavigationViewModel :ViewModelBase, INavigationViewModel
    {
        private IFriendLookupDataService _friendLookupService;
        private IEventAggregator _eventAggregator;

        public NavigationViewModel(IFriendLookupDataService friendLookupService, 
            IEventAggregator eventAggregator)
        {
            _friendLookupService = friendLookupService;
            _eventAggregator = eventAggregator;
            Friends = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterFriendEvent>().Subscribe(AfterFriendSaved);
        }

        private void AfterFriendSaved(AfterFriendSaveEventArgs obj)
        {
            var lookupItem = Friends.Single(l => l.Id == obj.Id);
            lookupItem.DisplayMember = obj.DisplayMember;
        }

        public ObservableCollection<NavigationItemViewModel> Friends { get; }

        public async Task LoadAsync()
        {
            var lookup = await _friendLookupService.GetFriendLookupAsync();
            Friends.Clear();
            foreach (var item in lookup)
            {
                Friends.Add(new NavigationItemViewModel(item.Id, item.DisplayMember,_eventAggregator));
            }
        }

       

    }
}
