using System;
using System.Collections.Generic;
using Minifutbol.BL.DTO;

namespace Minifutbol.BL.Models.TeamRequest
{
    public class TeamRequestViewModel
    {
        public int Id { get; set; }

        public int? UserId { get; set; }
        public bool isApproved { get; set; }

        public int TeamId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public virtual UserDto User { get; set; }
        public virtual TeamDto Team { get; set; }
    }
}
