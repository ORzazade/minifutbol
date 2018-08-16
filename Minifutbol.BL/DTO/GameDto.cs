using System;

namespace Minifutbol.BL.DTO
{
    public class GameDto
    {
        public int Id { get; set; }
        public int HostTeamId { get; set; }
        public int GuestTeamId { get; set; }
        public DateTime GameTime { get; set; }
        public string RefereeName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Description { get; set; }

    }
}
