namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQtypeToEnquiry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enquiries", "Qtype", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Enquiries", "Qtype");
        }
    }
}
