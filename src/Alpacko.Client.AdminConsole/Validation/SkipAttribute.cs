﻿using System;

namespace Alpacko.Client.AdminConsole.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class SkipAttribute : Attribute
    {
        public SkipAttribute()
        {
        }
    }
}