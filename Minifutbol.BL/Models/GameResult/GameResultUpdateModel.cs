using System;

namespace Minifutbol.BL.Models.GameResult
{
    public class GameResultUpdateModel
    {
        public int Id { get; set; }
        public int HostTeamGoals { get; set; }
        public int GuestTeamGoals { get; set; }
        public int GameId { get; set; }
        public string Description { get; set; }

    }
}
