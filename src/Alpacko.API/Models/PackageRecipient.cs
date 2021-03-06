﻿using System;
using System.Collections.Generic;

namespace Alpacko.API.Models
{
    public partial class PackageRecipient
    {
        public PackageRecipient()
        {
            Package = new HashSet<Package>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Co { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Package> Package { get; set; }
    }
}
