using System;
using System.Collections.Generic;

namespace Alpacko.Client.AdminConsole.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class ReplaceAttribute : Attribute
    {
        public string OldValue { get; }
        public string NewValue { get; }

        public ReplaceAttribute(string oldValue, string newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}