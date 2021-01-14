using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Alpacko.API.Models
{
    public partial class PostOffice
    {
        public PostOffice()
        {
            RegisteredPackage = new HashSet<RegisteredPackage>();
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual ICollection<RegisteredPackage> RegisteredPackage { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
