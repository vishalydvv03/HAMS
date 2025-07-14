
using System.ComponentModel.DataAnnotations;


namespace HAMS.Domain.Models.AuthenticationModel
{
    public class UserLoginModel
    {
        [EmailAddress, Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
