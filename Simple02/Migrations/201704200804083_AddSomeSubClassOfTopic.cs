namespace Simple02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSomeSubClassOfTopic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topics", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Topics", "Discriminator");
        }
    }
}
