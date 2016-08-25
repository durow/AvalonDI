using System;

namespace Ayx.AvalonDI
{
    public interface IContainer
    {
        object Get(Type type, string token = "");
        void Wire<T>(string token = "") where T:class;
    }
}
