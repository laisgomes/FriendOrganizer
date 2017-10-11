using FriendOrganizer.Model;
using System;
using System.Collections.Generic;

namespace FriendOrganizer.UI.Wapper
{
    public class FriendWapper : ModelWapper<Friend>
    {
        public FriendWapper(Friend model) : base(model)
        {
        }

        public int Id
        {
            get { return Model.Id; }
        }

        public string FirstName
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
              
            }
        }

        public string LastName
        {
            get { return GetValue<string>(); }

            set { SetValue(value); }
        }

        public string Email
        {
            get { return GetValue<string>(); }

            set { SetValue(value); }
        }

        /*private void ValidadeProperty(string propertyName)
        {
            ClearErrors(propertyName);
            
        }*/

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(FirstName):
                    if (string.Equals(FirstName, "Robot", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Robosts are not valid friends";
                    }
                    break;
            }
        }
    }
}
