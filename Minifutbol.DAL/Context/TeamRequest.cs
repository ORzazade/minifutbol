namespace Minifutbol.DAL.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TeamRequest")]
    public partial class TeamRequest
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public int TeamId { get; set; }

        public bool isApproved { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public virtual User User { get; set; }
        public virtual Team Team { get; set; }
    }
}
