using System;
using System.Collections.Generic;
using Minifutbol.BL.DTO;

namespace Minifutbol.BL.Models.GameResult
{
    public class GameResultViewModel
    {
        public int Id { get; set; }
        public int HostTeamGoals { get; set; }
        public int GuestTeamGoals { get; set; }
        public int GameId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Description { get; set; }

        public virtual GameDto Game { get; set; }
    }
}
