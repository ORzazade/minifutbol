using System;
using System.Collections.Generic;
using Minifutbol.BL.DTO;

namespace Minifutbol.BL.Models.Game
{
    public class GameViewModel
    {
        public int Id { get; set; }

        public int HostTeamId { get; set; }
        public int GuestTeamId { get; set; }
        public int? HostTeamGoals { get; set; }
        public int? GuestTeamGoals { get; set; }
        public DateTime GameTime { get; set; }
        public string RefereeName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Description { get; set; }

        public virtual TeamDto HostTeam { get; set; }

        public virtual TeamDto GuestTeam { get; set; }
    }
}
