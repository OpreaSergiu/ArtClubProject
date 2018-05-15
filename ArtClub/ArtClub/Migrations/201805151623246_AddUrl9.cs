namespace ArtClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApprovedReservationsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Reason = c.String(),
                        LocationReserved = c.Int(nullable: false),
                        User = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApprovedReservationsModels");
        }
    }
}
