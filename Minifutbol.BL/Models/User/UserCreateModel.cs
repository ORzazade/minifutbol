using System;
using System.ComponentModel.DataAnnotations;

namespace Minifutbol.BL.Models.User
{
    public class UserCreateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string MobileNumber { get; set; }
        [Required]
        [MaxLength(32)]
        public string Password { get; set; }
        public DateTime? LastLoginDate { get; set; } = DateTime.Now;
        public int? AccessFailedCount { get; set; } = 0;
        public int? TeamId { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
