namespace Zeus_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactMessageFiledSlengthsUpdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContactMessages", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ContactMessages", "Subject", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ContactMessages", "Email", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContactMessages", "Email", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.ContactMessages", "Subject", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.ContactMessages", "Name", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
