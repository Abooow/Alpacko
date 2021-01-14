﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpacko.API.Models
{
    public class SignInResultModel
    {
        public bool Successful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }
}
