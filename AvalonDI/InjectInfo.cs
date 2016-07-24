/*
 * Author:durow
 * Description:Record the inject information
*/
using System;

namespace Ayx.AvalonDI
{
    public class InjectInfo
    {
        internal object instance;

        public Type From { get; internal set; }
        public Type To { get; internal set; }
        public string Token { get; internal set; }
        public Func<object> CreateFunction { get; internal set; }
        public InjectType InjectType { get; internal set; }
    }
}
