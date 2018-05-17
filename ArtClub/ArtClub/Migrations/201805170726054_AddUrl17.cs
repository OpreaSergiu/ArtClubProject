namespace ArtClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl17 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentsModels", "UserName", c => c.String());
            DropColumn("dbo.PaymentsModels", "User");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaymentsModels", "User", c => c.String());
            DropColumn("dbo.PaymentsModels", "UserName");
        }
    }
}
