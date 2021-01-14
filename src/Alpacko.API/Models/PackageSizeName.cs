using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Alpacko.API.Models
{
    public partial class PackageSizeName
    {
        public PackageSizeName()
        {
            PackageDetail = new HashSet<PackageDetail>();
        }

        public int Id { get; set; }
        public int Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual ICollection<PackageDetail> PackageDetail { get; set; }
    }
}
