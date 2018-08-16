namespace Minifutbol.BL.Models.TeamRequest
{
    public class TeamRequestUpdateModel
    {
        public int Id { get; set; }

        public int? UserId { get; set; }
        public bool isApproved { get; set; } 

        public int TeamId { get; set; }

    }
}
