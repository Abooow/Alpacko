using System;
using System.Collections.Generic;

namespace Alpacko.Client.AdminConsole.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class EmailAttribute : Attribute
    {
        public string ErrorMessage { get; set; }

        public EmailAttribute()
        {
            ErrorMessage = "Not a valid e-mail address";
        }
    }
}