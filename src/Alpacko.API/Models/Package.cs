using System;
using System.Collections.Generic;

namespace Alpacko.API.Models
{
    public partial class Package
    {
        public Package()
        {
            RegisteredPackage = new HashSet<RegisteredPackage>();
            SentPackage = new HashSet<SentPackage>();
        }

        public string Id { get; set; }
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
