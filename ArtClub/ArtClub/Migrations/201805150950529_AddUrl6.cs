namespace ArtClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReservationsModels", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReservationsModels", "Phone");
        }
    }
}
