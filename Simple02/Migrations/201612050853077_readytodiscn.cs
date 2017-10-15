namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class readytodiscn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "readyForDiscn", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "readyForDiscn");
        }
    }
}
