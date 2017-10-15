namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class autoidforEnquiry : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "ToEqry_EID", "dbo.Enquiries");

            DropIndex("dbo.Posts", new[] { "ToEqry_EID" });
            DropPrimaryKey("dbo.Enquiries");
            DropColumn("dbo.Enquiries", "Enquiry_id");
            DropColumn("dbo.Posts", "ToEqry_EID");
            AddColumn("dbo.Enquiries", "Enquiry_id", c => c.Guid(nullable: false, identity: true));
            AddColumn("dbo.Posts", "ToEqry_EID", c => c.Guid());

            AddPrimaryKey("dbo.Enquiries", "Enquiry_id");

            CreateIndex("dbo.Posts", "ToEqry_EID");

            AddForeignKey("dbo.Posts", "ToEqry_EID", "dbo.Enquiries", "Enquiry_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "ToEqry_EID", "dbo.Enquiries");
            DropIndex("dbo.Posts", new[] { "ToEqry_EID" });
            DropPrimaryKey("dbo.Enquiries");
            DropColumn("dbo.Enquiries", "Enquiry_id");
            //DropColumn("dbo.Posts", "ToEqry_EID");
            AddColumn("dbo.Enquiries", "Enquiry_id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Posts", "ToEqry_EID", c => c.Int());


            AddPrimaryKey("dbo.Enquiries", "Enquiry_id");
            CreateIndex("dbo.Posts", "ToEqry_EID");
            AddForeignKey("dbo.Posts", "ToEqry_EID", "dbo.Enquiries", "Enquiry_id");
        }
    }
}
