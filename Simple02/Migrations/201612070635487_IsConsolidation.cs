namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsConsolidation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "IsConsolidation", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "IsConsolidation");
        }
    }
}
