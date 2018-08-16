using System;

namespace Minifutbol.BL.DTO
{
    public class GameResultDto
    {
        public int Id { get; set; }
        public int HostTeamGoals { get; set; }
        public int GuestTeamGoals { get; set; }
        public int GameId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Description { get; set; }

    }
}
