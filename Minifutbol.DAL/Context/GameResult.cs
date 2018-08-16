namespace Minifutbol.DAL.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GameResult")]
    public partial class GameResult
    {
        public int Id { get; set; }
        public int HostTeamGoals { get; set; }
        public int GuestTeamGoals { get; set; }
        public int GameId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Description { get; set; }

        public virtual Game Game { get; set; }
    }
}
