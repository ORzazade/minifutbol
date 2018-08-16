using System;

namespace Minifutbol.BL.DTO
{
    public class PointDto
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int GamePiont { get; set; }
        public int GameResultId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Description { get; set; }
    }
}
