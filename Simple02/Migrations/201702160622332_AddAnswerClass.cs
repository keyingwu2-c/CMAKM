namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnswerClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        AID = c.Int(nullable: false, identity: true),
                        content = c.String(),
                        sendTime = c.DateTime(nullable: false),
                        forvotes = c.Int(nullable: false),
                        Provider_Id = c.String(maxLength: 128),
                        ToEqry_EID = c.Guid(),
                    })
                .PrimaryKey(t => t.AID)
                .ForeignKey("dbo.AspNetUsers", t => t.Provider_Id)
                .ForeignKey("dbo.Enquiries", t => t.ToEqry_EID)
                .Index(t => t.Provider_Id)
                .Index(t => t.ToEqry_EID);
            
            AddColumn("dbo.AspNetUsers", "Answer_AID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Answer_AID");
            AddForeignKey("dbo.AspNetUsers", "Answer_AID", "dbo.Answers", "AID");
            DropColumn("dbo.Posts", "IsAnonymous");
            DropColumn("dbo.Posts", "IsConsolidation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "IsConsolidation", c => c.Boolean(nullable: false));
            AddColumn("dbo.Posts", "IsAnonymous", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.AspNetUsers", "Answer_AID", "dbo.Answers");
            DropForeignKey("dbo.Answers", "ToEqry_EID", "dbo.Enquiries");
            DropForeignKey("dbo.Answers", "Provider_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "Answer_AID" });
            DropIndex("dbo.Answers", new[] { "ToEqry_EID" });
            DropIndex("dbo.Answers", new[] { "Provider_Id" });
            DropColumn("dbo.AspNetUsers", "Answer_AID");
            DropTable("dbo.Answers");
        }
    }
}
