using FriendOrganizer.Model;
using System.Collections.Generic;

namespace FriendOrganizer.UI.Data
{
    class FirendDataService : IFirendDataService
    {
        public IEnumerable<Friend> GetAll()
        {
            //TODO: Load data from real database

            yield return new Friend{FirstName= "Alessandra", LastName="Brito"};
            yield return new Friend{FirstName= "Juliana", LastName="Lima"};
            yield return new Friend{FirstName= "Katyanne", LastName="Pantona"};
            yield return new Friend{FirstName= "Lara Luisa", LastName="Gomes"};
            
        }
    }
}
