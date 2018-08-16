using System;
using System.Collections.Generic;
using Minifutbol.BL.DTO;

namespace Minifutbol.BL.Models.User
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        
        public string MobileNumber { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int? AccessFailedCount { get; set; }

        public int? TeamId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual TeamDto Team { get; set; }

        public virtual ICollection<UserClaimDto> UserClaims { get; set; }

        public virtual ICollection<TeamRequestDto> TeamRequests { get; set; }
    }

    public class UserLogin
    {
        public string Secret { get; set; }
        public string Username { get; set; }
        public UserDetails User { get; set; }

    }
    public class UserDetails
    {
        public string Mail { get; set; }
        public string Department { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
  
}
