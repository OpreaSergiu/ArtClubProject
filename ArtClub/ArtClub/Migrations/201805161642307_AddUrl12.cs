namespace ArtClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventsModels", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventsModels", "CreationDate");
        }
    }
}
