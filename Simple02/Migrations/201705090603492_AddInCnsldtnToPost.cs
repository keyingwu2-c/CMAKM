namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInCnsldtnToPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "InCnsldtn", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "InCnsldtn");
        }
    }
}
