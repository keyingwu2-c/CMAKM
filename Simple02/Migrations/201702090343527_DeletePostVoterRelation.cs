namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletePostVoterRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Post_PID", "dbo.Posts");
            DropForeignKey("dbo.Posts", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "Post_PID" });
            DropIndex("dbo.Posts", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AspNetUsers", "Post_PID");
            DropColumn("dbo.Posts", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Post_PID", c => c.Int());
            CreateIndex("dbo.Posts", "ApplicationUser_Id");
            CreateIndex("dbo.AspNetUsers", "Post_PID");
            AddForeignKey("dbo.Posts", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "Post_PID", "dbo.Posts", "PID");
        }
    }
}
