namespace Speakster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Teacher : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        CEP = c.String(),
                        Phone = c.String(),
                        Language_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .Index(t => t.Language_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "Language_Id", "dbo.Languages");
            DropIndex("dbo.Teachers", new[] { "Language_Id" });
            DropTable("dbo.Teachers");
        }
    }
}
