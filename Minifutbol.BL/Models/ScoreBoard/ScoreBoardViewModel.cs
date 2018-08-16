using System;
using System.Collections.Generic;
using Minifutbol.BL.DTO;

namespace Minifutbol.BL.Models.ScoreBoard
{
    public class ScoreBoardViewModel
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public int Win { get; set; }
        public int Lose { get; set; }
        public int Draw { get; set; }
        public int GamePiont { get; set; }
    }
}
