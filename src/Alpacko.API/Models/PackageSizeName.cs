using System;
using System.Collections.Generic;

namespace Alpacko.API.Models
{
    public partial class PackageSizeName
    {
        public PackageSizeName()
        {
            PackageDetail = new HashSet<PackageDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual ICollection<PackageDetail> PackageDetail { get; set; }
    }
}
