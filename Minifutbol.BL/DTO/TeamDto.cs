using System;

namespace Minifutbol.BL.DTO
{
    public class TeamDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string Description { get; set; }

    }
}
