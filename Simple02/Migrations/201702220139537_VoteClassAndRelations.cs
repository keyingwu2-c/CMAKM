namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VoteClassAndRelations : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUserGroups", newName: "GroupApplicationUsers");
            DropPrimaryKey("dbo.GroupApplicationUsers");
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        VID = c.Int(nullable: false, identity: true),
                        forvotes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VID);
            
            CreateTable(
                "dbo.ApplicationUserVotes",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Vote_VID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Vote_VID })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Votes", t => t.Vote_VID, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Vote_VID);
            
            AddColumn("dbo.Answers", "avoting_VID", c => c.Int());
            AddColumn("dbo.Posts", "pvoting_VID", c => c.Int());
            AddPrimaryKey("dbo.GroupApplicationUsers", new[] { "Group_GID", "ApplicationUser_Id" });
            CreateIndex("dbo.Answers", "avoting_VID");
            CreateIndex("dbo.Posts", "pvoting_VID");
            AddForeignKey("dbo.Posts", "pvoting_VID", "dbo.Votes", "VID");
            AddForeignKey("dbo.Answers", "avoting_VID", "dbo.Votes", "VID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "avoting_VID", "dbo.Votes");
            DropForeignKey("dbo.Posts", "pvoting_VID", "dbo.Votes");
            DropForeignKey("dbo.ApplicationUserVotes", "Vote_VID", "dbo.Votes");
            DropForeignKey("dbo.ApplicationUserVotes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserVotes", new[] { "Vote_VID" });
            DropIndex("dbo.ApplicationUserVotes", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Posts", new[] { "pvoting_VID" });
            DropIndex("dbo.Answers", new[] { "avoting_VID" });
            DropPrimaryKey("dbo.GroupApplicationUsers");
            DropColumn("dbo.Posts", "pvoting_VID");
            DropColumn("dbo.Answers", "avoting_VID");
            DropTable("dbo.ApplicationUserVotes");
            DropTable("dbo.Votes");
            AddPrimaryKey("dbo.GroupApplicationUsers", new[] { "ApplicationUser_Id", "Group_GID" });
            RenameTable(name: "dbo.GroupApplicationUsers", newName: "ApplicationUserGroups");
        }
    }
}
