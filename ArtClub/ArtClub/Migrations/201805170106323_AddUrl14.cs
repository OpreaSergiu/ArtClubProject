namespace ArtClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventGuestsModels", "EventDate", c => c.String());
            AddColumn("dbo.EventGuestsModels", "EventLocation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventGuestsModels", "EventLocation");
            DropColumn("dbo.EventGuestsModels", "EventDate");
        }
    }
}
