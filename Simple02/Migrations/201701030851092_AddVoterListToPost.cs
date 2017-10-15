namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVoterListToPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Post_PID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Post_PID");
            AddForeignKey("dbo.AspNetUsers", "Post_PID", "dbo.Posts", "PID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Post_PID", "dbo.Posts");
            DropIndex("dbo.AspNetUsers", new[] { "Post_PID" });
            DropColumn("dbo.AspNetUsers", "Post_PID");
        }
    }
}
