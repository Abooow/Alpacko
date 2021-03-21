using System;
using System.Collections.Generic;

namespace Alpacko.API.Models
{
    public partial class RegisteredPackage
    {
        public int Id { get; set; }
        public string PackageId { get; set; }
        public int PostOfficeId { get; set; }
        public DateTime RegisteredDate { get; set; }

        public virtual Package Package { get; set; }
        public virtual PostOffice PostOffice { get; set; }
    }
}
