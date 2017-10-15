namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DsnBgtToAnswer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "dsnbgntime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Answers", "dsnbgntime");
        }
    }
}
