using FriendOrganizer.Model;

namespace FriendOrganizer.UI.Wrapper
{
    public class FriendPhoneNumberWapper : ModelWrapper<FriendPhoneNumber>
    {
        public FriendPhoneNumberWapper(FriendPhoneNumber model) : base(model)
        {

        }

        public string Number
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}
