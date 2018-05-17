namespace ArtClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventsModels", "LocationName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventsModels", "LocationName");
        }
    }
}
