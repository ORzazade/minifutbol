namespace Minifutbol.DAL.Context
{
    using System;
    using System.Collections;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;

    public partial class MinifutbolContext : DbContext
    {
        public MinifutbolContext()
            : base("name=MinifutbolContext")
        {
        }

        //public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserClaim> UserClaim { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<GameResult> GameResults { get; set; }
        public virtual DbSet<Point> Point { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<TeamRequest> TeamRequest { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.Salt)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .HasOptional<Team>(s => s.Team)
                .WithMany(s=>s.Users)
                .HasForeignKey(e=>e.TeamId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Game>()
                .HasRequired<Team>(e => e.HostTeam)
                .WithMany()

                .WillCascadeOnDelete();

            modelBuilder.Entity<Game>()
               .HasRequired<Team>(e => e.GuestTeam)
               .WithMany()
               .WillCascadeOnDelete(false);
            var at = new MinifutbolContext();
            //var aot = at.Database.SqlQuery<List<Team>>("select * from Team").ToList(); 

        }

        
    }
}
