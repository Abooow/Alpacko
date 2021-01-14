using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Alpacko.API.Models
{
    public partial class RegisteredPackage
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public int PostOfficeId { get; set; }
        public DateTime RegisteredDate { get; set; }

        public virtual Package Package { get; set; }
        public virtual PostOffice PostOffice { get; set; }
    }
}
