using System.ComponentModel.DataAnnotations;

namespace Minifutbol.BL.Models.Core
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "UserName is requeired")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "UserName is requeired")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public bool RememberMe { get; set; } = false;
    }
}
