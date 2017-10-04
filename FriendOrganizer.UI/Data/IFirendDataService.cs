using System.Collections.Generic;
using FriendOrganizer.Model;

namespace FriendOrganizer.UI.Data
{
    interface IFirendDataService
    {
        IEnumerable<Friend> GetAll();
    }
}