﻿namespace Minifutbol.BL.Models.UserClaim
{
    public class UserClaimUpdateModel
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

    }
}
