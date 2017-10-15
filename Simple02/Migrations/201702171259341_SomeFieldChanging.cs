namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeFieldChanging : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.GroupApplicationUsers", newName: "ApplicationUserGroups");
            DropForeignKey("dbo.Enquiries", "DcnGroup_GID", "dbo.Groups");
            DropIndex("dbo.Enquiries", new[] { "DcnGroup_GID" });
            DropPrimaryKey("dbo.ApplicationUserGroups");
            AddColumn("dbo.Answers", "dcsnStatus", c => c.String());
            AddColumn("dbo.Answers", "DcnGroup_GID", c => c.Int());
            AddColumn("dbo.Posts", "ToAsmt_AID", c => c.Int());
            AddColumn("dbo.Enquiries", "AnswerToCustomer", c => c.String());
            AddPrimaryKey("dbo.ApplicationUserGroups", new[] { "ApplicationUser_Id", "Group_GID" });
            CreateIndex("dbo.Answers", "DcnGroup_GID");
            CreateIndex("dbo.Posts", "ToAsmt_AID");
            AddForeignKey("dbo.Posts", "ToAsmt_AID", "dbo.Answers", "AID");
            AddForeignKey("dbo.Answers", "DcnGroup_GID", "dbo.Groups", "GroupID");
            DropColumn("dbo.Enquiries", "consolidation");
            DropColumn("dbo.Enquiries", "dcsnStatus");
            DropColumn("dbo.Enquiries", "DcnGroup_GID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enquiries", "DcnGroup_GID", c => c.Int());
            AddColumn("dbo.Enquiries", "dcsnStatus", c => c.String());
            AddColumn("dbo.Enquiries", "consolidation", c => c.String());
            DropForeignKey("dbo.Answers", "DcnGroup_GID", "dbo.Groups");
            DropForeignKey("dbo.Posts", "ToAsmt_AID", "dbo.Answers");
            DropIndex("dbo.Posts", new[] { "ToAsmt_AID" });
            DropIndex("dbo.Answers", new[] { "DcnGroup_GID" });
            DropPrimaryKey("dbo.ApplicationUserGroups");
            DropColumn("dbo.Enquiries", "AnswerToCustomer");
            DropColumn("dbo.Posts", "ToAsmt_AID");
            DropColumn("dbo.Answers", "DcnGroup_GID");
            DropColumn("dbo.Answers", "dcsnStatus");
            AddPrimaryKey("dbo.ApplicationUserGroups", new[] { "Group_GID", "ApplicationUser_Id" });
            CreateIndex("dbo.Enquiries", "DcnGroup_GID");
            AddForeignKey("dbo.Enquiries", "DcnGroup_GID", "dbo.Groups", "GroupID");
            RenameTable(name: "dbo.ApplicationUserGroups", newName: "GroupApplicationUsers");
        }
    }
}
