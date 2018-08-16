namespace Minifutbol.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pointtaleadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Point",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamId = c.Int(nullable: false),
                        GamePiont = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Game", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Team", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.GameId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Point", "TeamId", "dbo.Team");
            DropForeignKey("dbo.Point", "GameId", "dbo.Game");
            DropIndex("dbo.Point", new[] { "GameId" });
            DropIndex("dbo.Point", new[] { "TeamId" });
            DropTable("dbo.Point");
        }
    }
}
