using System;

namespace Minifutbol.BL.Models.Point
{
    public class PointCreateModel
    {
        public int TeamId { get; set; }
        public int GamePiont { get; set; }
        public int GameId { get; set; }
        public string Description { get; set; }
    }
}
