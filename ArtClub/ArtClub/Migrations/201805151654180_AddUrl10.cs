namespace ArtClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReservationsModels", "approved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReservationsModels", "approved");
        }
    }
}
