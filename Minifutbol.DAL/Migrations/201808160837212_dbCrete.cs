namespace Minifutbol.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbCrete : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Game",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HostTeamId = c.Int(nullable: false),
                        HostTeamGoals = c.Int(),
                        GuestTeamGoals = c.Int(),
                        GuestTeamId = c.Int(nullable: false),
                        GameTime = c.DateTime(nullable: false),
                        RefereeName = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        Team_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Team", t => t.Team_Id)
                .ForeignKey("dbo.Team", t => t.GuestTeamId)
                .ForeignKey("dbo.Team", t => t.HostTeamId, cascadeDelete: true)
                .Index(t => t.HostTeamId)
                .Index(t => t.GuestTeamId)
                .Index(t => t.Team_Id);
            
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        UserName = c.String(),
                        Password = c.String(nullable: false, maxLength: 32, fixedLength: true),
                        Salt = c.String(maxLength: 32, fixedLength: true),
                        MobileNumber = c.String(),
                        PhoneNumber = c.String(),
                        LastLoginDate = c.DateTime(),
                        AccessFailedCount = c.Int(),
                        TeamId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Team", t => t.TeamId,cascadeDelete:false)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.TeamRequest",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        TeamId = c.Int(nullable: false),
                        isApproved = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Team", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.UserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.GameResult",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HostTeamGoals = c.Int(nullable: false),
                        GuestTeamGoals = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Game", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Point",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamId = c.Int(),
                        GamePiont = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Game", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Team", t => t.TeamId)
                .Index(t => t.TeamId)
                .Index(t => t.GameId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Point", "TeamId", "dbo.Team");
            DropForeignKey("dbo.Point", "GameId", "dbo.Game");
            DropForeignKey("dbo.GameResult", "GameId", "dbo.Game");
            DropForeignKey("dbo.Game", "HostTeamId", "dbo.Team");
            DropForeignKey("dbo.Game", "GuestTeamId", "dbo.Team");
            DropForeignKey("dbo.UserClaim", "UserId", "dbo.User");
            DropForeignKey("dbo.TeamRequest", "UserId", "dbo.User");
            DropForeignKey("dbo.TeamRequest", "TeamId", "dbo.Team");
            DropForeignKey("dbo.User", "TeamId", "dbo.Team");
            DropForeignKey("dbo.Game", "Team_Id", "dbo.Team");
            DropIndex("dbo.Point", new[] { "GameId" });
            DropIndex("dbo.Point", new[] { "TeamId" });
            DropIndex("dbo.GameResult", new[] { "GameId" });
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.TeamRequest", new[] { "TeamId" });
            DropIndex("dbo.TeamRequest", new[] { "UserId" });
            DropIndex("dbo.User", new[] { "TeamId" });
            DropIndex("dbo.Game", new[] { "Team_Id" });
            DropIndex("dbo.Game", new[] { "GuestTeamId" });
            DropIndex("dbo.Game", new[] { "HostTeamId" });
            DropTable("dbo.Point");
            DropTable("dbo.GameResult");
            DropTable("dbo.UserClaim");
            DropTable("dbo.TeamRequest");
            DropTable("dbo.User");
            DropTable("dbo.Team");
            DropTable("dbo.Game");
        }
    }
}
