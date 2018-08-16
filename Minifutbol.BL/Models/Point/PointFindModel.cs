using System;

namespace Minifutbol.BL.Models.Point
{
    public class PointFindModel
    {
        public int? Id { get; set; }
        public int? TeamId { get; set; }
        public int? GamePiont { get; set; }
        public int? GameId { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }

    }
}
