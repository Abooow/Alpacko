﻿using System;
using System.Collections.Generic;

namespace Alpacko.Client.AdminConsole.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ToTitleCaseAttribute : Attribute
    {
        public ToTitleCaseAttribute()
        {
        }
    }
}