namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Teacher2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "User_id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Teachers", "User_id");
            AddForeignKey("dbo.Teachers", "User_id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "User_id", "dbo.AspNetUsers");
            DropIndex("dbo.Teachers", new[] { "User_id" });
            DropColumn("dbo.Teachers", "User_id");
        }
    }
}
