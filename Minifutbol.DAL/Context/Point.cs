namespace Minifutbol.DAL.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Point")]
    public partial class Point
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int GamePiont { get; set; }
        public int GameId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public virtual Team Team { get; set; }
        public virtual Game Game { get; set; }
    }
}
