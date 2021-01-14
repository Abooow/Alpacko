using System.ComponentModel.DataAnnotations;

namespace Alpacko.Client.Web.Models
{
    public class RegisterPackageModel
    {
        [Required(ErrorMessage = "This field cannot be empty")]
        [RegularExpression(@"\d*", ErrorMessage="Can only be numbers")]
        [StringLength(10,MinimumLength =10, ErrorMessage ="Must be 10 digits")]
        public string PackageID { get; set; }

    }
}
