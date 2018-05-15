namespace ArtClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CostsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User = c.String(),
                        EventId = c.Int(nullable: false),
                        EventName = c.String(),
                        Amount = c.Single(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Month = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaymentsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User = c.String(),
                        Amount = c.Single(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Month = c.DateTime(nullable: false),
                        Member = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PaymentsModels");
            DropTable("dbo.CostsModels");
        }
    }
}
