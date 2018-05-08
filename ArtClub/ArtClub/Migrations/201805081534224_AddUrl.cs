namespace ArtClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LocationsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Street = c.String(),
                        Number = c.String(),
                        AddressDetails = c.String(),
                        Description = c.String(),
                        Capacity = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LocationsModels");
        }
    }
}
