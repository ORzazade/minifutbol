using System;

namespace Minifutbol.BL.Models.Game
{
    public class GameUpdateModel
    {
        public int Id { get; set; }

        public int HostTeamId { get; set; }
        public int GuestTeamId { get; set; }
        public int? HostTeamGoals { get; set; }
        public int? GuestTeamGoals { get; set; }
        public DateTime GameTime { get; set; }
        public string RefereeName { get; set; }
        public string Description { get; set; }

    }
}
