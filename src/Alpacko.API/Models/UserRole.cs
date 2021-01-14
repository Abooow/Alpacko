using System;
using System.Collections.Generic;

namespace Alpacko.API.Models
{
    public partial class UserRole
    {
        public UserRole()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
