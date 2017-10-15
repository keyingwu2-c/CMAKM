namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class posterdisplay : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "posterDisplay", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "posterDisplay");
        }
    }
}
