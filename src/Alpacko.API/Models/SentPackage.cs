using System;
using System.Collections.Generic;

namespace Alpacko.API.Models
{
    public partial class SentPackage
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string PackageId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Package Package { get; set; }
        public virtual User User { get; set; }
    }
}
