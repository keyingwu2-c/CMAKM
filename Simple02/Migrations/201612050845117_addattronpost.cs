namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addattronpost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "type", c => c.String());
            AddColumn("dbo.Posts", "postingTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "postingTime");
            DropColumn("dbo.Posts", "type");
        }
    }
}
