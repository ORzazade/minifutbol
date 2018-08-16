using System;

namespace Minifutbol.BL.Models.Point
{
    public class PointUpdateModel
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int GameId { get; set; }
        public int GameResultId { get; set; }
        public string Description { get; set; }
    }
}
