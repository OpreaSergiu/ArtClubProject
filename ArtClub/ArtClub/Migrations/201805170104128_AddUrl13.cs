namespace ArtClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventGuestsModels", "EventName", c => c.String());
            DropColumn("dbo.EventGuestsModels", "GuestName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EventGuestsModels", "GuestName", c => c.String());
            DropColumn("dbo.EventGuestsModels", "EventName");
        }
    }
}
