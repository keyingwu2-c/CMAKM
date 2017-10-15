namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteAVoteInAnswer : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUserVotes", newName: "VoteApplicationUsers");
            RenameTable(name: "dbo.GroupApplicationUsers", newName: "ApplicationUserGroups");
            DropForeignKey("dbo.Answers", "avoting_VID", "dbo.Votes");
            DropIndex("dbo.Answers", new[] { "avoting_VID" });
            DropPrimaryKey("dbo.VoteApplicationUsers");
            DropPrimaryKey("dbo.ApplicationUserGroups");
            AddPrimaryKey("dbo.VoteApplicationUsers", new[] { "Vote_VID", "ApplicationUser_Id" });
            AddPrimaryKey("dbo.ApplicationUserGroups", new[] { "ApplicationUser_Id", "Group_GID" });
            DropColumn("dbo.Answers", "avoting_VID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Answers", "avoting_VID", c => c.Int());
            DropPrimaryKey("dbo.ApplicationUserGroups");
            DropPrimaryKey("dbo.VoteApplicationUsers");
            AddPrimaryKey("dbo.ApplicationUserGroups", new[] { "Group_GID", "ApplicationUser_Id" });
            AddPrimaryKey("dbo.VoteApplicationUsers", new[] { "ApplicationUser_Id", "Vote_VID" });
            CreateIndex("dbo.Answers", "avoting_VID");
            AddForeignKey("dbo.Answers", "avoting_VID", "dbo.Votes", "VID");
            RenameTable(name: "dbo.ApplicationUserGroups", newName: "GroupApplicationUsers");
            RenameTable(name: "dbo.VoteApplicationUsers", newName: "ApplicationUserVotes");
        }
    }
}
