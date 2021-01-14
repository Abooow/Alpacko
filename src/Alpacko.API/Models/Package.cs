using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Alpacko.API.Models
{
    public partial class Package
    {
        public Package()
        {
            RegisteredPackage = new HashSet<RegisteredPackage>();
            SentPackage = new HashSet<SentPackage>();
        }

        public int Id { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public int PackageDetailId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual PackageDetail PackageDetail { get; set; }
        public virtual PackageRecipient Recipient { get; set; }
        public virtual PackageSender Sender { get; set; }
        public virtual ICollection<RegisteredPackage> RegisteredPackage { get; set; }
        public virtual ICollection<SentPackage> SentPackage { get; set; }
    }
}
