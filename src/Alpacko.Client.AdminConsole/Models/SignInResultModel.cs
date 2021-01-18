using System;
using System.Collections.Generic;

namespace Alpacko.Client.AdminConsole.Models
{
    public class SignInResultModel
    {
        public bool Successful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }
}