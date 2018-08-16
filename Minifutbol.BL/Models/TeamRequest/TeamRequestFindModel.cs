using System;

namespace Minifutbol.BL.Models.TeamRequest
{
    public class TeamRequestFindModel
    {
        public int? Id { get; set; }

        public int? UserId { get; set; }
        public bool? isApproved { get; set; } = false;

        public int? TeamId { get; set; }

        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        public int? PageSize { get; set; }

        public int? PageNumber { get; set; }

    }
}
