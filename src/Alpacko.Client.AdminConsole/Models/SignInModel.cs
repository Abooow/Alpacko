using Alpacko.Client.AdminConsole.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alpacko.Client.AdminConsole.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage = "E-mail is required")]
        [Email]
        [ToLower, Trim]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Password]
        public string Password { get; set; }
    }
}