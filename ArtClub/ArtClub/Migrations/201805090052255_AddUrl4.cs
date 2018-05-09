namespace ArtClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventsModels", "CreationUser", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventsModels", "CreationUser");
        }
    }
}
