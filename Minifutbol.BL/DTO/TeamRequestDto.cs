using System;

namespace Minifutbol.BL.DTO
{
    public class TeamRequestDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string MobileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int? AccessFailedCount { get; set; }

        public int? TeamId { get; set; }
        public bool IsDeleted { get; set; }
       
    }
}
