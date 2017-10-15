namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrivateToEqryComment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Private", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Private");
        }
    }
}
