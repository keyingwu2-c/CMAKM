namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostRelyRelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "RlyWhich_PID", c => c.Int());
            CreateIndex("dbo.Posts", "RlyWhich_PID");
            AddForeignKey("dbo.Posts", "RlyWhich_PID", "dbo.Posts", "PID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "RlyWhich_PID", "dbo.Posts");
            DropIndex("dbo.Posts", new[] { "RlyWhich_PID" });
            DropColumn("dbo.Posts", "RlyWhich_PID");
        }
    }
}
