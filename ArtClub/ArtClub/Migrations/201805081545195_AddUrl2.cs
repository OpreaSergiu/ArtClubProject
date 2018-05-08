namespace ArtClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReservationsModels", "User", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReservationsModels", "User");
        }
    }
}
