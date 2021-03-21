using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpacko.Client.Web.Models
{
    public class SignUpResultModel
    {
        public bool Successful { get; set; }
        public string ErrorMessage { get; set; }
    }
}