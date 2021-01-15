using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpacko.API.Models.Results
{
    public class RegisteredPackageResult
    {
        public bool Successful { get; set; }
        public string ErrorMessage { get; set; }
    }
}
