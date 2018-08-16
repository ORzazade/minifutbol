namespace Minifutbol.DAL.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Game")]
    public partial class Game
    {
        public int Id { get; set; }

        public int HostTeamId { get; set; }
        public int? HostTeamGoals { get; set; }
        public int? GuestTeamGoals { get; set; }
        public int GuestTeamId { get; set; }
        public DateTime GameTime { get; set; }
        public string RefereeName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Description { get; set; }


        [ForeignKey("HostTeamId")]
        public virtual Team HostTeam { get; set; }

        [ForeignKey("GuestTeamId")]
        public virtual Team GuestTeam { get; set; }
      
    }
}
