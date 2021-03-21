using System;
using System.Collections.Generic;

namespace Alpacko.API.Models
{
    public partial class User
    {
        public User()
        {
            SentPackage = new HashSet<SentPackage>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int? PostOfficeId { get; set; }
        public int UserRoleId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual PostOffice PostOffice { get; set; }
        public virtual UserRole UserRole { get; set; }
        public virtual ICollection<SentPackage> SentPackage { get; set; }
    }
}
