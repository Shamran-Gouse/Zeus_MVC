namespace Zeus_MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserColumsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ClientName", c => c.String(maxLength: 100));
            AddColumn("dbo.AspNetUsers", "CompanyORHospital", c => c.String(maxLength: 100));
            AddColumn("dbo.AspNetUsers", "Address", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "CompanyORHospital");
            DropColumn("dbo.AspNetUsers", "ClientName");
        }
    }
}
