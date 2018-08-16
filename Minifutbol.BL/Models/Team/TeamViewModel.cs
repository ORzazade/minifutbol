using System;
using System.Collections.Generic;
using Minifutbol.BL.DTO;

namespace Minifutbol.BL.Models.Team
{
    public class TeamViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string Description { get; set; }

        public virtual ICollection<UserDto> Users { get; set; }
        public virtual ICollection<GameDto> Games { get; set; }
    }
}
