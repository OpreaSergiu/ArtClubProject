namespace ArtClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventGuestsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventId = c.Int(nullable: false),
                        GuestName = c.String(),
                        GuestEmail = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EventsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        EventDate = c.DateTime(nullable: false),
                        LocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EventsModels");
            DropTable("dbo.EventGuestsModels");
        }
    }
}
