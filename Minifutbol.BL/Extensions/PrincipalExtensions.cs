using Newtonsoft.Json;
using Minifutbol.BL.Models.User;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Minifutbol.BL.Extensions
{
    public static class PrincipalExtensions
    {
        /// <summary>
        /// Returns authenticated user id.
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static int GetUserId(this IPrincipal principal)
        {
            var claimsPrincipal = principal as ClaimsPrincipal;
            return int.Parse(claimsPrincipal != null && claimsPrincipal.HasClaim(s => s.Type.Equals("userId"))
                ? claimsPrincipal.Claims.First(c => c.Type.Equals("userId")).Value
                : "0");
        }
        public static List<string> GetUserRoles(this IPrincipal principal)
        {
            return principal is ClaimsPrincipal claimsPrincipal && claimsPrincipal.HasClaim(s => s.Type.Equals("Roles"))
                ? claimsPrincipal.Claims.Where(c => c.Type.Equals("Roles")).Select(x => x.Value).ToList()
                : new List<string>();
        }
        
    }
}
