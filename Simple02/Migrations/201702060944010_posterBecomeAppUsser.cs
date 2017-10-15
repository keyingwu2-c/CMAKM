namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class posterBecomeAppUsser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostApplicationUsers", "Post_PID", "dbo.Posts");
            DropForeignKey("dbo.PostApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PostApplicationUsers", new[] { "Post_PID" });
            DropIndex("dbo.PostApplicationUsers", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.AspNetUsers", "Post_PID", c => c.Int());
            AddColumn("dbo.Posts", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "Post_PID");
            CreateIndex("dbo.Posts", "ApplicationUser_Id");
            AddForeignKey("dbo.AspNetUsers", "Post_PID", "dbo.Posts", "PID");
            AddForeignKey("dbo.Posts", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
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

            DropForeignKey("dbo.Posts", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Post_PID", "dbo.Posts");
            DropIndex("dbo.Posts", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Post_PID" });
            DropColumn("dbo.Posts", "ApplicationUser_Id");
            DropColumn("dbo.AspNetUsers", "Post_PID");
            CreateIndex("dbo.PostApplicationUsers", "ApplicationUser_Id");
            CreateIndex("dbo.PostApplicationUsers", "Post_PID");
            AddForeignKey("dbo.PostApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PostApplicationUsers", "Post_PID", "dbo.Posts", "PID", cascadeDelete: true);
        }
    }
}
