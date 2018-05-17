namespace ArtClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl18 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CostsModels", "UserName", c => c.String());
            DropColumn("dbo.CostsModels", "User");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CostsModels", "User", c => c.String());
            DropColumn("dbo.CostsModels", "UserName");
        }
    }
}
