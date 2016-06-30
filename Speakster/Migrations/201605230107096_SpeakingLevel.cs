namespace Speakster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpeakingLevel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpeakingLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            
            DropTable("dbo.SpeakingLevels");
        }
    }
}
