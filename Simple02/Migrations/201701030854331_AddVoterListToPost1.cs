namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVoterListToPost1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Post_PID", "dbo.Posts");
            DropIndex("dbo.AspNetUsers", new[] { "Post_PID" });
            CreateTable(
                "dbo.PostApplicationUsers",
                c => new
                    {
                        Post_PID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Post_PID, t.ApplicationUser_Id })
                .ForeignKey("dbo.Posts", t => t.Post_PID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Post_PID)
                .Index(t => t.ApplicationUser_Id);
            
            DropColumn("dbo.AspNetUsers", "Post_PID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Post_PID", c => c.Int());
            DropForeignKey("dbo.PostApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PostApplicationUsers", "Post_PID", "dbo.Posts");
            DropIndex("dbo.PostApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.PostApplicationUsers", new[] { "Post_PID" });
            DropTable("dbo.PostApplicationUsers");
            CreateIndex("dbo.AspNetUsers", "Post_PID");
            AddForeignKey("dbo.AspNetUsers", "Post_PID", "dbo.Posts", "PID");
        }
    }
}
