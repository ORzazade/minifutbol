using System;
using System.ComponentModel.DataAnnotations;

namespace Minifutbol.BL.Models.Game
{
    public class GameCreateModel
    {

        public int HostTeamId { get; set; }
        public int? GuestTeamId { get; set; }
        public int? HostTeamGoals { get; set; }
        public int? GuestTeamGoals { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime GameTime { get; set; }
        public string RefereeName { get; set; }
        public string Description { get; set; }

    }
}
