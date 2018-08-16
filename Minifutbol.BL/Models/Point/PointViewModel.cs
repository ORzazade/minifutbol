using System;
using System.Collections.Generic;
using Minifutbol.BL.DTO;

namespace Minifutbol.BL.Models.Point
{
    public class PointViewModel
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int GamePiont { get; set; }
        public string TeamName { get; set; }
        public int GameId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public virtual TeamDto Team { get; set; }
        public virtual GameDto Game { get; set; }
    }
}
