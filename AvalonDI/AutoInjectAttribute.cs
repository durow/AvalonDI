using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ayx.AvalonDI
{
    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Constructor, AllowMultiple = false)]
    public sealed class AutoInjectAttribute:Attribute
    {
        public string Token { get; private set; }
        public AutoInjectAttribute(string token="")
        {
            Token = token;
        }
    }
}
