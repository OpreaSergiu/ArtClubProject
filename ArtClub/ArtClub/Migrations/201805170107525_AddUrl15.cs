namespace ArtClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl15 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EventGuestsModels", "EventDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EventGuestsModels", "EventDate", c => c.String());
        }
    }
}
