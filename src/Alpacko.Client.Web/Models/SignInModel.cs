using System.ComponentModel.DataAnnotations;

namespace Alpacko.Client.Web.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage = "E-mail is required")]
        [EmailAddress(ErrorMessage = "Not a valid e-mail address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public bool StaySignedIn { get; set; }
    }
}