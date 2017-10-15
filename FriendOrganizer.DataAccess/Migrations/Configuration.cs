using FriendOrganizer.Model;

namespace FriendOrganizer.DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<FriendOrganizer.DataAccess.FriendOrganizerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FriendOrganizer.DataAccess.FriendOrganizerDbContext context)
        {
            context.Friends.AddOrUpdate(
                f => f.FirstName,
             new Friend { FirstName = "Alessandra", LastName = "Brito", Email = "alebrito@gmail.com" },
             new Friend { FirstName = "Juliana", LastName = "Lima" },
             new Friend { FirstName = "Katyanne", LastName = "Pantona" },
             new Friend { FirstName = "Lara Luisa", LastName = "Gomes", Email = "lara@gmail.com" }
                );

            context.ProgrammingLanguages.AddOrUpdate(
                pl => pl.Name,
                new ProgrammingLanguage { Name = "C#" },
                new ProgrammingLanguage { Name = "TypeScript" },
                new ProgrammingLanguage { Name = "F#" },
                new ProgrammingLanguage { Name = "Swift" },
                new ProgrammingLanguage { Name = "Java" });
        }
    }
}
