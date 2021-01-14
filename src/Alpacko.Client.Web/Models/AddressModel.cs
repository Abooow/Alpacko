using System.ComponentModel.DataAnnotations;

namespace Alpacko.Client.Web.Pages
{
    public class AddressModel
    {
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(26, ErrorMessage = "First name is too long")]
        public string SenderFirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(26, ErrorMessage = "Last name is too long")]
        public string SenderLastName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(30, ErrorMessage = "Address is too long")]
        public string SenderAddress { get; set; }

        [Required(ErrorMessage = "Zip Code is required")]
        [MaxLength(9, ErrorMessage = "Zip Code is too long")]
        [RegularExpression(@"\d{3}[ |-]*\d{2}", ErrorMessage = "Not a valid zip code")]
        public string SenderZipCode { get; set; }

        [Required(ErrorMessage = "City is required")]
        [MaxLength(30, ErrorMessage = "City is too long")]
        public string SenderCity { get; set; }

        [Required(ErrorMessage = "E-mail is required")]
        [MaxLength(254, ErrorMessage = "E-mail is too long")]
        [EmailAddress(ErrorMessage = "Not a valid e-mail address")]
        public string SenderEmail { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [MaxLength(26, ErrorMessage = "First name is too long")]
        public string ReceiverFirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(26, ErrorMessage = "Last name is too long")]
        public string ReceiverLastName { get; set; }

        public string ReceiverCo { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(30, ErrorMessage = "Address is too long")]
        public string ReceiverAddress { get; set; }

        [Required(ErrorMessage = "Zip Code is required")]
        [MaxLength(9, ErrorMessage = "Zip Code is too long")]
        [RegularExpression(@"\d{3}[ |-]*\d{2}", ErrorMessage = "Not a valid zip code")]
        public string ReceiverZipCode { get; set; }

        [Required(ErrorMessage = "City is required")]
        [MaxLength(30, ErrorMessage = "City is too long")]
        public string ReceiverCity { get; set; }

        public string ReceiverCountry { get; set; }

        [MaxLength(254, ErrorMessage = "E-mail is too long")]
        [EmailAddress(ErrorMessage = "Not a valid e-mail address")]
        public string ReceiverEmail { get; set; }

        [RegularExpression(@"^[+]?[(]?\d{3}[)]?[ -]{0,3}\d{0,3}[ -]{0,3}\d{0,2}[ -]{0,3}\d{0,2}$", ErrorMessage = "Not a valid phone number")]
        public string ReceiverPhoneNumber { get; set; }
    }
}