namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteUAUPrelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Post_PID", "dbo.Posts");
            DropForeignKey("dbo.Posts", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Answer_AID", "dbo.Answers");
            DropIndex("dbo.Answers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Post_PID" });
            DropIndex("dbo.AspNetUsers", new[] { "Answer_AID" });
            DropIndex("dbo.Posts", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Answers", "ApplicationUser_Id");
            DropColumn("dbo.AspNetUsers", "Post_PID");
            DropColumn("dbo.AspNetUsers", "Answer_AID");
            DropColumn("dbo.Posts", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Answer_AID", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Post_PID", c => c.Int());
            AddColumn("dbo.Answers", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Posts", "ApplicationUser_Id");
            CreateIndex("dbo.AspNetUsers", "Answer_AID");
            CreateIndex("dbo.AspNetUsers", "Post_PID");
            CreateIndex("dbo.Answers", "ApplicationUser_Id");
            AddForeignKey("dbo.AspNetUsers", "Answer_AID", "dbo.Answers", "AID");
            AddForeignKey("dbo.Posts", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "Post_PID", "dbo.Posts", "PID");
            AddForeignKey("dbo.Answers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
