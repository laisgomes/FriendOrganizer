using FriendOrganizer.Model;
using FriendOrganizer.UI.Data.Repositories;
using FriendOrganizer.UI.Event;
using FriendOrganizer.UI.View.Service;
using FriendOrganizer.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FriendOrganizer.UI.ViewModel
{
    internal class FriendDetailViewModel : ViewModelBase, IFriendDetailViewModel
    {
        private IFriendRepository _repository;
        private FriendWrapper _friend;
        private IEventAggregator _eventAggregator;
        private bool _hasChanges;
        private IMessageDialogService _messageDialogService;

        public FriendDetailViewModel(IFriendRepository repository,
            IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _repository = repository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
        }


        public FriendWrapper Friend
        {
            get { return _friend; }
            private set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }

        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }

            }
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        private async void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOkCancelDialog(
                $"Do you really want to delete the friend {Friend.FirstName} {Friend.LastName}?",
                "Question");

            if (result == MessageDialogResult.OK)
            {
                _repository.Remove(Friend.Model);

                await _repository.SaveAsync();

                _eventAggregator.GetEvent<AfterFriendDeletedEvent>().Publish(Friend.Id);
            }


        }
        private async void OnSaveExecute()
        {
            await _repository.SaveAsync();
            HasChanges = _repository.HasChanges();
            _eventAggregator.GetEvent<AfterFriendEvent>().Publish(
                new AfterFriendSaveEventArgs
                {
                    Id = Friend.Id,
                    DisplayMember = $"{Friend.FirstName} {Friend.LastName}"
                });

        }

        private bool OnSaveCanExecute()
        {
            return Friend != null && !Friend.HasErrors && HasChanges;
        }

        private Friend CreateNewFriend()
        {
            var friend = new Friend();
            _repository.Add(friend);
            return friend;

        }

        public async Task LoadAsync(int? friendId)
        {
            var friend = friendId.HasValue
                ? await _repository.GetByIdAsync(friendId.Value)
                : CreateNewFriend();

            Friend = new FriendWrapper(friend);
            Friend.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _repository.HasChanges();
                }
                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            if (Friend.Id == 0)
            {
                Friend.FirstName = "";
            }

        }


    }
}
