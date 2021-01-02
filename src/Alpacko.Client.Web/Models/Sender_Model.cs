using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alpacko.Client.Web.Models
{
    public class Sender_Model
    {
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(26, ErrorMessage = "First name is too long")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(26, ErrorMessage = "Last name is too long")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(30, ErrorMessage = "Address is too long")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Zip Code is required")]
        [MaxLength(9, ErrorMessage = "Zip Code is too long")]
        public int ZipCode { get; set; }

        [Required(ErrorMessage = "City is required")]
        [MaxLength(30, ErrorMessage = "City is too long")]
        public string City { get; set; }

        [Required(ErrorMessage = "E-mail is required")]
        [MaxLength(254, ErrorMessage = "E-mail is too long")]
        [EmailAddress(ErrorMessage = "Not a valid e-mail address")]
        public string Email { get; set; }
    }
}