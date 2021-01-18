using Alpacko.Client.AdminConsole.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Alpacko.Client.AdminConsole.Models
{
    public class PostOfficeModel
    {
        [Skip]
        public int Id { get; set; }

        [Required]
        [UpperCaseFirst]
        public string Name { get; set; }

        [Required]
        [ToTitleCase, Trim]
        public string Address { get; set; }

        [Required]
        [RegularExpression(@"\d{3}[ |-]*\d{2}", ErrorMessage = "Not a valid zip code")]
        [Replace(" ", "")]
        [Replace("-", "")]
        public string ZipCode { get; set; }

        [Required]
        [ToTitleCase, Trim]
        public string City { get; set; }

        [Required]
        [Email]
        [ToLower, Trim]
        public string Email { get; set; }

        [Skip]
        public DateTime CreatedDate { get; set; }

        [Skip]
        public DateTime? ModifiedDate { get; set; }

        [Skip]
        public DateTime? DeletedDate { get; set; }
    }
}