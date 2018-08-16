using System;

namespace Minifutbol.BL.Models.User
{
    public class UserUpdateModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public int? TeamId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
