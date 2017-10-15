namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeeIfDifferent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostApplicationUsers", "Post_PID", "dbo.Posts");
            DropForeignKey("dbo.PostApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PostApplicationUsers", new[] { "Post_PID" });
            DropIndex("dbo.PostApplicationUsers", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.AspNetUsers", "Post_PID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Post_PID");
            AddForeignKey("dbo.AspNetUsers", "Post_PID", "dbo.Posts", "PID");
            DropTable("dbo.PostApplicationUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PostApplicationUsers",
                c => new
                    {
                        Post_PID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Post_PID, t.ApplicationUser_Id });
            
            DropForeignKey("dbo.AspNetUsers", "Post_PID", "dbo.Posts");
            DropIndex("dbo.AspNetUsers", new[] { "Post_PID" });
            DropColumn("dbo.AspNetUsers", "Post_PID");
            CreateIndex("dbo.PostApplicationUsers", "ApplicationUser_Id");
            CreateIndex("dbo.PostApplicationUsers", "Post_PID");
            AddForeignKey("dbo.PostApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PostApplicationUsers", "Post_PID", "dbo.Posts", "PID", cascadeDelete: true);
        }
    }
}
