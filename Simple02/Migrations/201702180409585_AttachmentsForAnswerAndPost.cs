namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttachmentsForAnswerAndPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FilePaths", "answer_AID", c => c.Int());
            AddColumn("dbo.FilePaths", "post_PID", c => c.Int());
            CreateIndex("dbo.FilePaths", "answer_AID");
            CreateIndex("dbo.FilePaths", "post_PID");
            AddForeignKey("dbo.FilePaths", "answer_AID", "dbo.Answers", "AID");
            AddForeignKey("dbo.FilePaths", "post_PID", "dbo.Posts", "PID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FilePaths", "post_PID", "dbo.Posts");
            DropForeignKey("dbo.FilePaths", "answer_AID", "dbo.Answers");
            DropIndex("dbo.FilePaths", new[] { "post_PID" });
            DropIndex("dbo.FilePaths", new[] { "answer_AID" });
            DropColumn("dbo.FilePaths", "post_PID");
            DropColumn("dbo.FilePaths", "answer_AID");
        }
    }
}
