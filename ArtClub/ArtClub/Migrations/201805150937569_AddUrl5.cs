namespace ArtClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReservationsModels", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.ReservationsModels", "StartDate");
            DropColumn("dbo.ReservationsModels", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReservationsModels", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ReservationsModels", "StartDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ReservationsModels", "Date");
        }
    }
}
