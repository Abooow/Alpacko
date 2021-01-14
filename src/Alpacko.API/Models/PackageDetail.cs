using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Alpacko.API.Models
{
    public partial class PackageDetail
    {
        public PackageDetail()
        {
            Package = new HashSet<Package>();
        }

        public int Id { get; set; }
        public int SizeNameId { get; set; }
        public decimal Price { get; set; }
        public double MinLenght { get; set; }
        public double MaxLenght { get; set; }
        public double MinWidth { get; set; }
        public double MaxWidth { get; set; }
        public double? MinHeight { get; set; }
        public double MaxHeight { get; set; }
        public int? MinWeight { get; set; }
        public int MaxWeight { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public virtual PackageSizeName SizeName { get; set; }
        public virtual ICollection<Package> Package { get; set; }
    }
}
