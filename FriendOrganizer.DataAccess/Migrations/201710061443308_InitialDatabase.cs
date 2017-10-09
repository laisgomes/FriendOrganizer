namespace FriendOrganizer.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Friends", newName: "Friend");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Friend", newName: "Friends");
        }
    }
}
